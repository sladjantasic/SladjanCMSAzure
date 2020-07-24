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
    [Route("api/cosmosdevices")]
    [ApiController]
    public class CosmosDevicesController : ControllerBase
    {
        private readonly IMapper mapper;
        //private readonly UserManager<User> userManager;
        private readonly ICosmosService cosmosService;

        public CosmosDevicesController(IMapper mapper, ICosmosService cosmosService)
        {
            this.mapper = mapper;
            //this.userManager = userManager;
            this.cosmosService = cosmosService;
        }

        // GET: api/<DevicesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CosmosDeviceIndex>>> GetAllAsync()
        {
            var viewModel = await cosmosService.GetDevicesAsync("SELECT * FROM c");

            return Ok(mapper.Map<IEnumerable<CosmosDeviceIndex>>(viewModel));
        }

        // GET api/<DevicesController>/5
        [HttpGet("{Id}", Name = "CosmosDevice")]
        public async Task<IActionResult> GetAsync(string Id)
        {
            var viewModel = mapper.Map<CosmosDeviceIndex>(await cosmosService.GetDeviceAsync(Id));
            return Ok(viewModel);
        }

        // POST api/<DevicesController>
        [HttpPost]
        public async Task<ActionResult<CosmosDeviceIndex>> Post(DeviceCreate viewModel)
        {
            var model = mapper.Map<CosmosDevice>(viewModel);
            model.IsConnected = false;
            model.Id = Guid.NewGuid().ToString();
            await cosmosService.AddAsync(model);

            var deviceToReturn = mapper.Map<CosmosDeviceIndex>(model);
            return CreatedAtRoute("CosmosDevice", new { Id = deviceToReturn.Id }, deviceToReturn);

        }

        // PUT api/<DevicesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DevicesController>/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(string Id)
        {
            await cosmosService.RemoveAsync(Id);
            return NoContent();
        }
    }
}
