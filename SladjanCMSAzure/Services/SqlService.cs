using Microsoft.EntityFrameworkCore;
using SladjanCMSAzure.Data;
using SladjanCMSAzure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SladjanCMSAzure.Services
{
    public class SqlService : ISqlService
    {
        private readonly SQLdbContext context;

        public SqlService(SQLdbContext context)
        {
            this.context = context;
        }

        public IQueryable<Device> GetAllDevices()
        {
            return context.Devices.AsQueryable();
        }

        public async Task<Device> GetDeviceAsync(int id)
        {
            return await context.Devices.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Device device)
        {
            await context.AddAsync(device);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Device device)
        {
            context.Remove(device);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Device device)
        {
            context.Update(device);
            await context.SaveChangesAsync();
        }
    }
}
