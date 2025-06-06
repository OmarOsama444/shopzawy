using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Spec.Dtos;

public record SpecOptionResponse(Guid Id, Guid SpecificationId, string Value);
