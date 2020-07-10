using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SladjanCMSAzure.Models.ViewModels
{
    public class DeviceCreate
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
