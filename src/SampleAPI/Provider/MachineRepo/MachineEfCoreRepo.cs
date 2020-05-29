using Microsoft.EntityFrameworkCore;
using SampleAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAPI.Provider
{
    public class MachineEfCoreRepo : IMachinesRepo
    {
        private readonly MachineContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineEfCoreRepo"/> class.
        /// </summary>
        /// <param name="context"></param>
        public MachineEfCoreRepo(MachineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Stores a single machine.
        /// </summary>
        /// <param name="machine">Single machine.</param>
        /// <returns>null or the new machine.</returns>
        public async Task<Machine> AddMachineAsync(Machine machine)
        {
            await _context.Machines.AddAsync(machine);
            await _context.SaveChangesAsync();

            return machine;
        }

        /// <summary>
        /// Updates a single machine.
        /// </summary>
        /// <param name="machineID">Id of the machine.</param>
        /// <param name="newMachine">Data for the update.</param>
        /// <param name="includeNull">If set to true null is set as value, else null is not reflected.</param>
        /// <returns>null or the updated machine.</returns>
        public async Task<Machine> UpdateMachineAsync(Machine updatedData)
        {
            _context.Machines.Update(updatedData);
            _context.SaveChanges();

            return updatedData;
        }

        /// <summary>
        /// Loads and returns a specific machine.
        /// </summary>
        /// <param name="id">Id of the record set.</param>
        /// <returns>Task with the machine or null.</returns>
        public async Task<Machine> GetMachineAsync(Guid id)
        {
            return await _context.Machines.FindAsync(id);
        }

        /// <summary>
        /// Loads and returns all machines which are available.
        /// </summary>
        public async Task<List<Machine>> GetMachinesAsync()
        {
            return await _context.Machines.ToListAsync();
        }
    }
}
