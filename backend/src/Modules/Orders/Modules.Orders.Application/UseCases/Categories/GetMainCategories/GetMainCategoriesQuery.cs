using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.GetMainCategories;

public record GetMainCategoriesQuery(Language lang_code) : IQuery<ICollection<TranslatedCategoryResponseDto>>;
