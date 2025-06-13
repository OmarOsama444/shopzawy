using Modules.Common.Application.Messaging;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.GetMainCategories;

public record GetMainCategoriesQuery(Language lang_code) : IQuery<ICollection<TranslatedCategoryResponseDto>>;
