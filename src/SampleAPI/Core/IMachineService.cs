using SampleAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAPI.Core
{
   public interface IMachineService
    {
        /// <summary>
        /// Stores a single machine.
        /// </summary>
        /// <param name="machine">Single machine.</param>
        /// <returns>null or the new machine.</returns>
        Task<Machine> AddMachineAsync(CreateMachineCommand machine);

        /// <summary>
        /// Updates a single machine.
        /// </summary>       
        /// <param name="newMachine">Data for the update.</param>        
        /// <returns> updated machine or null, in case the machine does not exist</returns>
        Task<Machine> UpdateMachineAsync(UpdateMachineCommand newMachine);

        /// <summary>
        /// Loads and returns a specific machine.
        /// </summary>
        /// <param name="id">Id of the record set.</param>
        /// <returns>Task with the machine or null.</returns>
        Task<Machine> GetMachineAsync(Guid id);

        /// <summary>
        /// Loads and returns all machines which are available.
        /// </summary>
        /// <returns>List of all machines found or an empty list.</returns>
        Task<List<Machine>> GetMachinesAsync();
    }
}
