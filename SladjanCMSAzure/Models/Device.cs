using System;
using System.Collections.Generic;

namespace SladjanCMSAzure.Models
{
    public partial class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsConnected { get; set; }
        public string UserId { get; set; }
    }
}
