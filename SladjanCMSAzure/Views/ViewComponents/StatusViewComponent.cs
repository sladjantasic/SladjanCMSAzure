using Microsoft.AspNetCore.Mvc;
using SladjanCMSAzure.Models;
using SladjanCMSAzure.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SladjanCMSAzure.Views.ViewComponents
{
    public class StatusViewComponent : ViewComponent
    {
        private readonly SQLdbContext context;

        public StatusViewComponent(SQLdbContext context)
        {
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int deviceId)
        {
            var device = await context.Devices.FindAsync(deviceId);

            var model = new Status()
            {
                IsConnected = device.IsConnected
            };

            return View(model);
        }
    }
}
