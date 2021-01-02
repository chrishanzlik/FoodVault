﻿using FoodVault.Api.Storage.Common;
using FoodVault.Application.Mediator;
using FoodVault.Application.Storage.FoodStorages.CreateStorage;
using FoodVault.Application.Storage.FoodStorages.GetStorageOverview;
using FoodVault.Application.Storage.FoodStorages.RemoveStorage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodVault.Api.Storage.FoodStorages
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodStorageController : Controller
    {
        private readonly IMediator _mediator;

        public FoodStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetStorageOverviewAsync()
        {
            var query = new GetStorageOverviewQuery();

            var result = await _mediator.Send(query);

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStorageAsync(CreateStorageRequest request)
        {
            var command = new CreateStorageCommand(request.StorageName, request.Description);

            ICommandResult result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        [HttpDelete("{foodStorageId}")]
        public async Task<IActionResult> RemoveStorageAsync(Guid foodStorageId)
        {
            var command = new RemoveStorageCommand(foodStorageId);

            ICommandResult result = await _mediator.Send(command);

            return result.ToActionResult();
        }
    }
}
