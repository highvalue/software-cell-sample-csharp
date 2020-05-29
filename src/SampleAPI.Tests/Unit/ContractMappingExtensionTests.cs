using System;
using Xunit;

namespace SampleAPI.Tests
{
    public class ContractMappingExtensionTests
    {
        [Fact]
        public void ToContract_ValidMachine_AllPropertiesMapped()
        {
            // arrange
            var testMachine = new SampleAPI.Contracts.Machine()
            {
                Id = Guid.NewGuid().ToString(),
                FabricSerialnumber = "1234ab",
                InternalSeriesName = "internal1234",
                ManufacturerName = "Microsoft",
                MachineName = "xyz",
                ManufacturingYear = new DateTime(2029, 1, 20),
                PlaceOfManufacturing = "Redmond",
                Serialnumber = "01234",
            };

            // act
            var result = testMachine;

            // assert
            Assert.Equal(testMachine.Id, result.Id);
           // ...
        }
    }
}
