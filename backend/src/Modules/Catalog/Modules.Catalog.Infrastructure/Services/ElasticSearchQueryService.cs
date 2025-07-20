using Common.Application.InjectionLifeTime;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Services;
using Modules.Catalog.Domain.Elastic;
using Nest;

namespace Modules.Catalog.Infrastructure.Services;


public class ElasticSearchQueryService : IElasticSearchQueryService, ISingleton
{
    private readonly IElasticClient _elasticClient;
    private const string IndexName = "products";

    public ElasticSearchQueryService(IElasticClientFactory elasticClientFactory)
    {
        _elasticClient = elasticClientFactory.CreateElasticClient();
    }

    /// <summary>
    /// Executes a search against the products index using the given parameters.
    /// </summary>
    public async Task<ISearchResponse<ProductDocument>> SearchProductsAsync(
        string? queryText = null,
        string? vendorId = null,
        string? brandId = null,
        string? categoryId = null,
        float? minPrice = null,
        float? maxPrice = null,
        List<VariationFilter<string>>? colorVariationFilters = null,
        List<VariationFilter<string>>? stringVariationFilters = null,
        List<NumericVariationFilter>? numericVariationFilters = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var mustQueries = new List<QueryContainer>();
        var filterQueries = new List<QueryContainer>
        {
            new TermQuery
            {
                Field = "inStock",
                Value = true
            }
        };
        // Full-text search on localized fields
        if (!string.IsNullOrWhiteSpace(queryText))
        {
            mustQueries.Add(new MultiMatchQuery
            {
                Fields = Infer.Fields<ProductDocument>(
                    p => p.Name!.En,
                    p => p.Name!.Ar,
                    p => p.LongDescription!.En,
                    p => p.LongDescription!.Ar),
                Query = queryText,
                Type = TextQueryType.BestFields
            });
        }

        // Exact term filters
        if (!string.IsNullOrWhiteSpace(vendorId))
            filterQueries.Add(new TermQuery { Field = "vendorId", Value = vendorId });

        if (!string.IsNullOrWhiteSpace(brandId))
            filterQueries.Add(new TermQuery { Field = "brandId", Value = brandId });

        if (categoryId is not null)
            filterQueries.Add(new TermQuery { Field = "categoryIds", Value = categoryId! });

        if (minPrice.HasValue || maxPrice.HasValue)
        {
            var range = new NumericRangeQuery { Field = "price" };
            if (minPrice.HasValue)
                range.GreaterThanOrEqualTo = minPrice.Value;
            if (maxPrice.HasValue)
                range.LessThanOrEqualTo = maxPrice.Value;
            filterQueries.Add(range);
        }

        // Nested string variation filters
        if (colorVariationFilters?.Count > 0)
        {
            foreach (var vf in colorVariationFilters)
            {
                filterQueries.Add(new NestedQuery
                {
                    Path = "stringVariations",
                    Query = new BoolQuery
                    {
                        Filter =
                        [
                                new TermQuery { Field = "colorVariations.specId", Value = vf.SpecId },
                                new TermQuery { Field = "colorVariations.value",  Value = vf.Value! }
                        ]
                    }
                });
            }
        }

        // Nested string variation filters
        if (stringVariationFilters?.Count > 0)
        {
            foreach (var vf in stringVariationFilters)
            {
                filterQueries.Add(new NestedQuery
                {
                    Path = "stringVariations",
                    Query = new BoolQuery
                    {
                        Filter =
                        [
                                new TermQuery { Field = "stringVariations.specId", Value = vf.SpecId },
                                new TermQuery { Field = "stringVariations.value",  Value = vf.Value! }
                        ]
                    }
                });
            }
        }

        if (numericVariationFilters?.Count > 0)
        {
            foreach (var vf in numericVariationFilters)
            {
                var numRangeQuery = new NumericRangeQuery();
                numRangeQuery.Field = "numericVariations.value";
                if (vf.Lte.HasValue)
                    numRangeQuery.LessThanOrEqualTo = vf.Lte;
                if (vf.Gte.HasValue)
                    numRangeQuery.GreaterThanOrEqualTo = vf.Gte;
                var nestedQuery = new NestedQuery
                {
                    Path = "numericVariations",
                    Query = new BoolQuery
                    {
                        Filter =
                        [
                                new TermQuery { Field = "numericVariations.specId", Value = vf.SpecId },
                                numRangeQuery
                        ]
                    }
                };
            }
        }

        // Pagination
        var from = (page - 1) * pageSize;

        // Execute the search
        return await _elasticClient.SearchAsync<ProductDocument>(s => s
            .Index(IndexName)
            .From(from)
            .Size(pageSize)
            .Query(q => q
                .Bool(b => b
                    .Must(mustQueries.ToArray())
                    .Filter(filterQueries.ToArray())
                )
            ),
            cancellationToken
        );
    }
}