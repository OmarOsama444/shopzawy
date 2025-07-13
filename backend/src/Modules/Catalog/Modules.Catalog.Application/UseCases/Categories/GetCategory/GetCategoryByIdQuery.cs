using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Catalog.Application.Dtos;

namespace Modules.Catalog.Application.UseCases.Categories.GetCategory;

public record GetCategoryByIdQuery(Guid Id, Language LangCode) : IQuery<CategoryResponeDto>;

