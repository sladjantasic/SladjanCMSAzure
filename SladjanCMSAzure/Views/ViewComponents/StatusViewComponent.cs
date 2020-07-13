using Microsoft.AspNetCore.Mvc;
using SladjanCMSAzure.Data;
using SladjanCMSAzure.Models;
using SladjanCMSAzure.Models.ViewModels;
using SladjanCMSAzure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SladjanCMSAzure.Views.ViewComponents
{
    public class StatusViewComponent : ViewComponent
    {
        private readonly ISqlService sqlService;
        public StatusViewComponent(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int deviceId)
        {
            var device = await sqlService.GetDeviceAsync(deviceId);

            var model = new Status()
            {
                IsConnected = device.IsConnected
            };

            return View(model);
        }
    }
}
