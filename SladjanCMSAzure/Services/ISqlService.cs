using SladjanCMSAzure.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SladjanCMSAzure.Services
{
    public interface ISqlService
    {
        Task AddAsync(Device device);
        IQueryable<Device> GetAllDevices();
        Task<Device> GetDeviceAsync(int id);
        Task RemoveAsync(Device device);
        Task UpdateAsync(Device device);
    }
}