using SampleAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAPI.Provider
{
    public interface IMachinesRepo
    {
        /// <summary>
        /// Stores a single machine.
        /// </summary>
        /// <param name="machine">Single machine.</param>
        /// <returns>null or the new machine.</returns>
        Task<Machine> AddMachineAsync(Machine machine);

        /// <summary>
        /// Updates a single machine.
        /// </summary>
        /// <param name="machineID">Id of the machine.</param>
        /// <param name="newMachine">Data for the update.</param>
        /// <param name="includeNull">If set to true null is set as value, else null is not reflected.</param>
        /// <returns>null or the updated machine.</returns>
        Task<Machine> UpdateMachineAsync(Machine newMachine);

        /// <summary>
        /// Loads and returns a specific machine.
        /// </summary>
        /// <param name="id">Id of the record set.</param>
        /// <returns>Task with the machine or null.</returns>
        Task<Machine> GetMachineAsync(Guid id);

        /// <summary>
        /// Loads and returns all machines which are available.
        /// </summary>
        /// <returns>List of all machines found or null.</returns>
        Task<List<Machine>> GetMachinesAsync();
    }
}
