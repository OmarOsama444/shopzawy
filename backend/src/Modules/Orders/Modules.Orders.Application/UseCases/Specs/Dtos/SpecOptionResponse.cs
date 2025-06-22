using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Spec.Dtos;

public record SpecOptionResponse(Guid SpecificationId, string Value);
