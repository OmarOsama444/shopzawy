using Modules.Common.Application.Messaging;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Banners.CreateBanner;

public record CreateBannerCommand(
            string title,
            string description,
            string link,
            BannerPosition position,
            BannerSize size,
            bool active) : ICommand<Guid>;
