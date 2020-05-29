using SampleAPI.Contracts;
using System;

namespace SampleAPI.Core
{
    public static class MachineExtensions
    {
        /// <summary>
        /// Maps all data from <paramref name="create"/> to a newly created <paramref name="machine"/>.
        /// </summary>
        /// <param name="machine">the <paramref name="machine"/> to be updated.</param>
        /// <param name="create">the data to be applied to <paramref name="machine"/>.</param>
        public static void MapData(this Machine machine, CreateMachineCommand create)
        {
            machine.FabricSerialnumber = create.FabricSerialnumber;
            machine.InternalSeriesName = create.InternalSeriesName;
            machine.MachineName = create.MachineName;
            machine.ManufacturerName = create.ManufacturerName;
            machine.ManufacturingYear = create.ManufacturingYear;
            machine.PlaceOfManufacturing = create.PlaceOfManufacturing;
            machine.Serialnumber = create.Serialnumber;
        }

        /// <summary>
        /// Updates <paramref name="machine"/> with new data from <paramref name="update"/>. Null values are ignored.
        /// </summary>
        /// <param name="machine">the machine to be updated.</param>
        /// <param name="update">the update data.</param>
        public static void PatchData(this Machine machine, UpdateMachineCommand update)
        {
            WhenNotNull(update.FabricSerialnumber, x => machine.FabricSerialnumber = x);
            WhenNotNull(update.InternalSeriesName, x => machine.InternalSeriesName = x);
            WhenNotNull(update.MachineName, x => machine.MachineName = x);
            WhenNotNull(update.ManufacturerName, x => machine.ManufacturerName = x);
            WhenNotNull(update.PlaceOfManufacturing, x => machine.PlaceOfManufacturing = x);
            WhenNotNull(update.Serialnumber, x => machine.Serialnumber = x);

            if (update.ManufacturingYear.HasValue)
            {
                machine.ManufacturingYear = update.ManufacturingYear.Value;
            }

            // simplified null check with static local function
            static void WhenNotNull<T>(T value, Action<T> mapping)
            {
                if (value is null)
                {
                    return;
                }

                mapping(value);
            }
        }
    }
}
