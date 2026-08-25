using System.Diagnostics;
using CarSeer.Application.Models.GetModels;
using CarSeer.Application.VehicleTypes.GetVehicleTypes;
using CarSeer.Domain.Exceptions;
using CarSeer.Web.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarSeer.Web.Controllers;

public sealed class HomeController(ISender sender) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(
        [Bind(nameof(VehicleLookupViewModel.MakeId), nameof(VehicleLookupViewModel.MakeName), nameof(VehicleLookupViewModel.Year), nameof(VehicleLookupViewModel.VehicleType))]
        VehicleLookupViewModel model,
        CancellationToken cancellationToken)
    {
        if (model.MakeId is null && model.Year is null)
        {
            return View(model);
        }

        model.HasSearched = true;

        if (model.MakeId is null or <= 0)
        {
            model.ErrorMessage = "Select a car make from the suggestions.";
            return View(model);
        }

        if (model.Year is null)
        {
            model.ErrorMessage = "Enter a manufacture year.";
            return View(model);
        }

        try
        {
            var typesTask = sender.Send(new GetVehicleTypesQuery(model.MakeId.Value), cancellationToken);
            var modelsTask = sender.Send(new GetModelsQuery(model.MakeId.Value, model.Year.Value, model.VehicleType),cancellationToken);

            await Task.WhenAll(typesTask, modelsTask);

            model.VehicleTypes = await typesTask;
            model.Models = await modelsTask;
        }
        catch (ValidationException exception)
        {
            model.ErrorMessage = string.Join(" ", exception.Errors.Select(error => error.ErrorMessage));
        }
        catch (VehicleCatalogException exception)
        {
            model.ErrorMessage = exception.Message;
        }

        return View(model);
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
