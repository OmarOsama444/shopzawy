using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Modules.Catalog.Application.UseCases.Specs.Dtos;

public record SpecOptionResponse(Guid SpecificationId, string Value);
