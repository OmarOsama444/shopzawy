using Modules.Orders.Domain.Elastic;
using Nest;
using Quartz.Xml.JobSchedulingData20;

namespace Modules.Orders.Infrastructure.Elastic;

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
                        .Nested<ProductItemDocument>(x => x
                            .Name(c => c.ProductItemDocuments)
                            .Properties(p => p
                                .Keyword(k => k.Name(d => d.Id))
                                .Number(k => k.Name(d => d.Price).Type(NumberType.Float))
                                .Nested<Variation<string>>(n => n.Name(x => x.StringVariations)
                                    .Properties(np => np
                                        .Keyword(x => x.Name(s => s.SpecId))
                                        .Keyword(x => x.Name(s => s.Value))
                                    )
                                )
                                .Nested<Variation<float>>(n => n.Name(x => x.NumericVariations)
                                    .Properties(np => np
                                        .Keyword(x => x.Name(s => s.SpecId))
                                        .Keyword(x => x.Name(s => s.Value))
                                    )
                                )
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
