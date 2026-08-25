using CarSeer.Application.Makes.GetMakes;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarSeer.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MakesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(string? search, CancellationToken cancellationToken)
    {
        try
        {
            var makes = await sender.Send(new GetMakesQuery(search), cancellationToken);
            return Ok(makes);
        }
        catch (ValidationException exception)
        {
            return BadRequest(new
            {
                errors = exception.Errors.Select(error => error.ErrorMessage)
            });
        }
    }
}
