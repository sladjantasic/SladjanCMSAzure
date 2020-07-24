using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SladjanCMSAzure.Models;
using SladjanCMSAzure.Models.ViewModels;
using SladjanCMSAzure.Services;

namespace SladjanCMSAzure.Controllers
{
    public class CosmosDevicesController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICosmosService cosmosService;

        public CosmosDevicesController(IMapper mapper, ICosmosService cosmosService)
        {
            this.mapper = mapper;
            this.cosmosService = cosmosService;
        }


        // GET: CosmosDevicesController
        public async Task<ActionResult<IEnumerable<CosmosDeviceIndex>>> Index()
        {
            var viewModel = await cosmosService.GetDevicesAsync("SELECT * FROM c");

            return View(mapper.Map<IEnumerable<CosmosDeviceIndex>>(viewModel));
        }

        // GET: CosmosDevicesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CosmosDevicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceCreate viewModel)
        {
            var model = mapper.Map<CosmosDevice>(viewModel);
            //model.UserId = userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                model.IsConnected = false;
                model.Id = Guid.NewGuid().ToString();
                await cosmosService.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }


        // GET: CosmosDevicesController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var device = await cosmosService.GetDeviceAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            await cosmosService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
