using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.UpdateProduct;

public record UpdateProductCommand(string productName,
    string longDescription,
    string shortDescription,
    string imageUrl,
    WeightUnit weightUnit,
    float weight,
    float price,
    DimensionUnit dimensionUnit,
    float width,
    float length,
    float height,
    ICollection<string> tags) : ICommand<Guid>;

public sealed class UpdateProductCommandHandler() : ICommandHandler<UpdateProductCommand, Guid>
{
    public Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        throw new NotImplementedException();
    }
}
