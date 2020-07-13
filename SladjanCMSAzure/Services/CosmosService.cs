using Microsoft.Azure.Cosmos;
using SladjanCMSAzure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SladjanCMSAzure.Services
{
    public class CosmosService : ICosmosService
    {
        private Container container;

        public CosmosService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(CosmosDevice device)
        {
            await container.CreateItemAsync<CosmosDevice>(device, new PartitionKey(device.Id));
        }

        public async Task<CosmosDevice> GetDeviceAsync(string id)
        {
            try
            {
                var response = await container.ReadItemAsync<CosmosDevice>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CosmosDevice>> GetDevicesAsync(string queryString)
        {
            var query = container.GetItemQueryIterator<CosmosDevice>(new QueryDefinition(queryString));
            List<CosmosDevice> results = new List<CosmosDevice>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task RemoveAsync(string id)
        {
            await container.DeleteItemAsync<CosmosDevice>(id, new PartitionKey(id));
        }

        public async Task UpdateAsync(string id, CosmosDevice device)
        {
            await container.UpsertItemAsync<CosmosDevice>(device, new PartitionKey(id));
        }
    }
}
