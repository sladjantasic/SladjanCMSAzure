using SladjanCMSAzure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SladjanCMSAzure.Services
{
    public interface ICosmosService
    {
        Task<IEnumerable<CosmosDevice>> GetDevicesAsync(string queryString);
        Task<CosmosDevice> GetDeviceAsync(string id);
        Task AddAsync(CosmosDevice device);
        Task UpdateAsync(string id, CosmosDevice device);
        Task RemoveAsync(string id);
    }
}
