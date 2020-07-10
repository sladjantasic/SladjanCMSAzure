using AutoMapper;
using SladjanCMSAzure.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SladjanCMSAzure.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Device, DeviceIndex>();
            CreateMap<DeviceCreate, Device>();
            CreateMap<Device, DeviceRemove>();
        }
    }
}
