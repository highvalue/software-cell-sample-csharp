using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Contracts
{
    /// <summary>
    /// This is a machine.
    /// </summary>
    public class Machine
    {
        /// <summary>
        /// Gets or sets id of the machine.
        /// </summary>        
        [Required]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        [Required]
        [JsonProperty(PropertyName = "machineName")]
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets year of the manufacturing.
        /// </summary>
        [Required]
        [JsonProperty(PropertyName = "manufacturingYear")]
        public DateTime ManufacturingYear { get; set; }

        /// <summary>
        /// Gets or sets serial number of the machine.
        /// </summary>
        [Required]
        [JsonProperty(PropertyName = "serialNumber")]
        public string Serialnumber { get; set; }

        /// <summary>
        /// Gets or sets fabric internal serial number.
        /// </summary>        
        [JsonProperty(PropertyName = "fabricSerialNumber")]
        public string FabricSerialnumber { get; set; }

        /// <summary>
        /// Gets or sets internal name of the series.
        /// </summary>
        [JsonProperty(PropertyName = "internalSeriesName")]
        public string InternalSeriesName { get; set; }

        /// <summary>
        /// Gets or sets name of the manufacturer.
        /// </summary>
        [Required]
        [JsonProperty(PropertyName = "manufacturerName")]
        public string ManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets place of the manufacturing.
        /// </summary>
        [JsonProperty(PropertyName = "placeOfManufacturing")]
        public string PlaceOfManufacturing { get; set; }
    }
}