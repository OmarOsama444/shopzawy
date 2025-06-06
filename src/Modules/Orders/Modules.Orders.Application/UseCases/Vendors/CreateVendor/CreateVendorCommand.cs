using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.CreateVendor;

public record CreateVendorCommand(
    string vendorName,
    string email,
    string phoneNumber,
    string address,
    string logoUrl,
    string shippingZoneName,
    string description,
    bool? active,
    string countryCode) : ICommand<Guid>;
