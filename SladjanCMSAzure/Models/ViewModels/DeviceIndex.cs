using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SladjanCMSAzure.Models.ViewModels
{
    public class DeviceIndex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Status")]
        public bool IsConnected { get; set; }
    }
}
