using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SladjanCMSAzure.Models;
using SladjanCMSAzure.Models.ViewModels;
using SladjanCMSAzure.Services;

namespace SladjanCMSAzure.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IMapper mapper;
        //private readonly UserManager<User> userManager;
        private readonly ISqlService sqlService;

        public DevicesController(IMapper mapper, ISqlService sqlService)
        {
            this.mapper = mapper;
            //this.userManager = userManager;
            this.sqlService = sqlService;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
           
            var viewModel = await mapper.ProjectTo<DeviceIndex>(sqlService.GetAllDevices()).ToListAsync();

            return View(viewModel);
        }



        // GET: Devices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceCreate viewModel)
        {
            var model = mapper.Map<Device>(viewModel);
            //model.UserId = userManager.GetUserId(User);
            model.IsConnected = false;

            if (ModelState.IsValid)
            {
                await sqlService.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }



        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = mapper.Map<DeviceRemove>(await sqlService.GetDeviceAsync((int)id));

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await sqlService.GetDeviceAsync(id);
            await sqlService.RemoveAsync(device);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ToggleDeviceStatus(int id)
        {
            var device = await sqlService.GetDeviceAsync(id);
            device.IsConnected = !device.IsConnected;
            await sqlService.UpdateAsync(device);
            return RedirectToAction(nameof(Index));
        }
    }
}
