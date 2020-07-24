using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SladjanCMSAzure.Models;
using SladjanCMSAzure.Models.ViewModels;
using SladjanCMSAzure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMSApi.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DevicesController : ControllerBase
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

        // GET: api/<DevicesController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var viewModel = await mapper.ProjectTo<DeviceIndex>(sqlService.GetAllDevices()).ToListAsync();

            return Ok(viewModel);
        }

        // GET api/<DevicesController>/5
        [HttpGet("{Id}", Name = "Device")]
        public async Task<IActionResult> GetAsync(int Id)
        {
            var viewModel = mapper.Map<DeviceIndex>(await sqlService.GetDeviceAsync(Id));
            return Ok(viewModel);
        }

        // POST api/<DevicesController>
        [HttpPost]
        public async Task<ActionResult<DeviceIndex>> Post(DeviceCreate viewModel)
        {
            var model = mapper.Map<Device>(viewModel);
            model.IsConnected = false;
            await sqlService.AddAsync(model);

            var deviceToReturn = mapper.Map<DeviceIndex>(model);
            return CreatedAtRoute("Device", new { Id = deviceToReturn.Id}, deviceToReturn);

        }

        // PUT api/<DevicesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DevicesController>/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var device = await sqlService.GetDeviceAsync(Id);
            await sqlService.RemoveAsync(device);
            return NoContent();
        }
    }
}
