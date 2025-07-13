using Modules.Catalog.Domain.Elastic;
using Nest;

namespace Modules.Catalog.Infrastructure.Elastic;

public static class ElasticSearchIndexIntializer
{
    public static async void InitializeElasticSearchIndex(IElasticClient elasticClient)
    {
        // the document for the product
        var indexName = "products";
        var exists = await elasticClient.Indices.ExistsAsync(indexName);
        if (!exists.Exists)
        {
            var createIndexResponse = await elasticClient.Indices.CreateAsync(indexName, c => c
                .Settings(c =>
                    c.NumberOfShards(1)
                    .NumberOfReplicas(0)
                )
                .Map<ProductDocument>(m => m
                    .Properties(p => p
                        .Keyword(x => x.Name(c => c.Id))
                        .Keyword(x => x.Name(c => c.VendorId))
                        .Keyword(x => x.Name(c => c.BrandId))
                        .Keyword(x => x.Name(c => c.CategoryIds))
                        .Keyword(x => x.Name(c => c.ImageUrls))
                        .Object<LocalizedField>(
                            o => o
                            .Name(d => d.Name)
                            .Properties(op => op
                                .Text(t => t.Name(x => x.En).Analyzer("standard"))
                                .Text(t => t.Name(x => x.Ar).Analyzer("arabic")))
                        )
                        .Object<LocalizedField>(
                            o => o
                            .Name(d => d.ShortDescription)
                            .Properties(lp => lp
                                .Text(t => t.Name(x => x.En).Analyzer("standard"))
                                .Text(t => t.Name(x => x.Ar).Analyzer("arabic")))
                        )
                        .Object<LocalizedField>(
                            o => o
                            .Name(d => d.LongDescription)
                            .Properties(lp => lp
                                .Text(t => t.Name(x => x.En).Analyzer("standard"))
                                .Text(t => t.Name(x => x.Ar).Analyzer("arabic")))
                        )
                        .Keyword(k => k.Name(d => d.Id))
                        .Boolean(x => x.Name(x => x.InStock))
                        .Number(k => k.Name(d => d.Price).Type(NumberType.Float))
                        .Nested<StringVariation>(n => n.Name(x => x.StringVariations)
                            .Properties(np => np
                                .Keyword(x => x.Name(s => s.SpecId))
                                .Keyword(x => x.Name(s => s.Value))
                            )
                        )
                        .Nested<StringVariation>(n => n.Name(x => x.StringVariations)
                            .Properties(np => np
                                .Keyword(x => x.Name(s => s.SpecId))
                                .Keyword(x => x.Name(s => s.Value))
                            )
                        )
                        .Nested<NumericVariation>(n => n.Name(x => x.NumericVariations)
                            .Properties(np => np
                                .Keyword(x => x.Name(s => s.SpecId))
                                .Number(x => x.Name(s => s.Value).Type(NumberType.Float))
                            )
                        )
                    )
                )
            );

            if (!createIndexResponse.IsValid)
            {
                throw new Exception($"Failed to create index: {createIndexResponse.ServerError.Error.Reason}");
            }
        }
    }
}
