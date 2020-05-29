using Microsoft.Extensions.Logging;
using SampleAPI.Contracts;
using SampleAPI.Provider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAPI.Core
{
    public class MachineService : IMachineService
    {
        private readonly IMachinesRepo _repo;
        private readonly ILogger<MachineService> _logger;

        public MachineService(ILogger<MachineService> logger, IMachinesRepo repo)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<Machine> AddMachineAsync(CreateMachineCommand createCommand)
        {
            var newMachine = new Machine()
            {
                Id = Guid.NewGuid().ToString(),
                FabricSerialnumber = createCommand.FabricSerialnumber,
                InternalSeriesName = createCommand.InternalSeriesName,
                MachineName = createCommand.MachineName,
                ManufacturerName = createCommand.ManufacturerName,
                ManufacturingYear = createCommand.ManufacturingYear,
                PlaceOfManufacturing = createCommand.PlaceOfManufacturing,
                Serialnumber = createCommand.Serialnumber,
            };

            return await _repo.AddMachineAsync(newMachine);
        }

        public async Task<Machine> GetMachineAsync(Guid id)
        {
           return await _repo.GetMachineAsync(id);
        }

        public async Task<List<Machine>> GetMachinesAsync()
        {
            return await _repo.GetMachinesAsync();
        }

        public async Task<Machine> UpdateMachineAsync(UpdateMachineCommand updateMachineCommand)
        {
            Machine machine = await GetMachineAsync(Guid.Parse(updateMachineCommand.Id));

            if (machine is null)
            {
                return machine;
            }
            else
            {
                machine.PatchData(updateMachineCommand);

                return await _repo.UpdateMachineAsync(machine);
            }
        }
    }
}
