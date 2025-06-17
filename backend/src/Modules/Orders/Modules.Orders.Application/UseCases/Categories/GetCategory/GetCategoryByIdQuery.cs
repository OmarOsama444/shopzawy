using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Application.UseCases.Categories.Dtos;
using Common.Application.Messaging;
using Common.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Categories.GetCategory;

public record GetCategoryByIdQuery(Guid Id, Language LangCode) : IQuery<CategoryResponeDto>;

