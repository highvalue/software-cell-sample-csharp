using Microsoft.Azure.Cosmos;
using SampleAPI.Contracts;
using SampleAPI.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineAPI.Provider
{
    /// <summary>
    /// Class is handling all of the Cosmos DB operations.
    /// </summary>
    public class MachinesCosmosDbRepo : IMachinesRepo
    {
        private readonly Container _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="MachinesCosmosDbRepo"/> class.
        /// </summary>
        /// <param name="dbClient">Cosmos DB client.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="containerName">Name of the container.</param>
        public MachinesCosmosDbRepo(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        /// <summary>
        /// Creates a new machine.
        /// </summary>
        /// <param name="machine">Single machine.</param>
        /// <returns>null or the new machine.</returns>
        public async Task<Machine> AddMachineAsync(Machine machine)
        {
            machine.Id = Guid.NewGuid().ToString();
            Machine newMachine = await _container.CreateItemAsync<Machine>(machine);
            return newMachine;
        }

        /// <summary>
        /// Loads and returns a specific machine.
        /// </summary>
        /// <param name="id">Id of the record set.</param>
        /// <returns>Task with the machine or null.</returns>
        public async Task<Machine> GetMachineAsync(Guid id)
        {
            try
            {
                ItemResponse<Machine> response = await _container.ReadItemAsync<Machine>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        /// <summary>
        /// Loads and returns all machines which are available.
        /// </summary>
        /// <returns>List of all machines found or null.</returns>
        public async Task<List<Machine>> GetMachinesAsync()
        {
            try
            {
                return _container.GetItemLinqQueryable<Machine>(true).ToList();
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        /// <summary>
        /// Updates machine.
        /// </summary>
        /// <param name="machine">Single machine.</param>
        /// <returns>Task with the machine or null.</returns>
        public async Task<Machine> UpdateMachineAsync(Machine update)
        {
            Machine updatedMachine = await _container.UpsertItemAsync(update);
            return updatedMachine;
        }
    }
}
