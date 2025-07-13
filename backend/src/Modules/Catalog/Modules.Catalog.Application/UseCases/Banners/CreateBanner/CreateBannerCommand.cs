using Common.Application.Messaging;
using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Application.UseCases.Banners.CreateBanner;

public record CreateBannerCommand(
            string title,
            string description,
            string link,
            BannerPosition position,
            BannerSize size,
            bool active) : ICommand<Guid>;
