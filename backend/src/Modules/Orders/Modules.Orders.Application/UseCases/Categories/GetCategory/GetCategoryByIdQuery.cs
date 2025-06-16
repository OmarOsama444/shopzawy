using Modules.Common.Application.Messaging;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Application.UseCases.Categories.Dtos;

namespace Modules.Orders.Application.UseCases.Categories.GetCategory;

public record GetCategoryByIdQuery(Guid Id, Language LangCode) : IQuery<CategoryResponeDto>;

