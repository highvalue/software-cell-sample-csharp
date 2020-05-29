using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleAPI.Contracts;
using SampleAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Gate
{
    [Route("api/machines")]
    [ApiController]
    public class MachinesRestController : ControllerBase
    {
        private readonly ILogger<MachinesRestController> _logger;

        /// <summary>
        /// CosmosDB service interface for db operations.
        /// </summary>
        private readonly IMachineService _machineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MachinesRestController"/> class.
        /// </summary>
        /// <param name="cosmosDbService">Instance of the CosmosDB service.</param>
        /// <param name="logger">The logger.</param>
        public MachinesRestController(ILogger<MachinesRestController> logger, IMachineService cosmosDbService)
        {
            this._logger = logger;
            this._machineService = cosmosDbService;
        }

        /// <summary>
        /// GET api returning all items by calling .../api/machines.
        /// </summary>
        /// <returns>Not found status code or code 200 including json.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<Machine>>> Get()
        {
            var machines = await _machineService.GetMachinesAsync();

            return machines.Select(x => x).ToList();
        }

        /// <summary>
        /// GET api returning the item specified by id by calling .../api/machines/{id}.
        /// </summary>
        /// <param name="id">id of the item.</param>
        /// <returns>Not found status code or code 200 including json.</returns>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Machine>> Get(string id)
        {
            // Example of bare bones input validation. Ensure that
            // - id is a proper Guid
            if (!Guid.TryParse(id, out var parsedId))
            {
                return BadRequest();
            }

            var machine = await _machineService.GetMachineAsync(parsedId);

            if (machine == null)
            {
                return NotFound(id);
            }

            return machine;
        }

        /// <summary>
        /// POST api for storing machines by calling .../api/machines and the content in the body.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/machines
        ///     {
        ///         "machineName": "Test machine",
        ///         "manufacturingYear": "2020-04-08T20:33:50Z",
        ///         "serialNumber": "123-456-7890",
        ///         "fabricSerialNumber": "12345-12345",
        ///         "internalSeriesName": "Contura 7034",
        ///         "manufacturerName": "Microsoft",
        ///         "placeOfManufacturing": "Redmond"
        ///     }
        ///
        /// </remarks>
        /// <param name="createCommand">The Machine to be created</param>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="500">If the item could not be created because of technical issues.</response>
        /// <returns>The newly created item.</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Machine>> Post([FromBody] CreateMachineCommand createCommand)
        {
            var newMachine = await _machineService.AddMachineAsync(createCommand);

            return this.CreatedAtAction(nameof(Post), new { id = newMachine.Id }, newMachine);
        }

        /// <summary>
        /// Patch api for updating a machine by calling .../api/machines/{id} and the content in the body.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/machines/ec76897b-4f19-40c7-8de6-bd999852daab
        ///     {
        ///         "id": "ec76897b-4f19-40c7-8de6-bd999852daab",
        ///         "machineName": "Test machine1",
        ///         "manufacturingYear": "2021-04-08T20:33:50Z",
        ///         "serialNumber": "123-456",
        ///         "fabricSerialNumber": "12345",
        ///         "internalSeriesName": "Contura 4711",
        ///         "manufacturerName": "Microsoft",
        ///         "placeOfManufacturing": "Redmond"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Id of the item.</param>
        /// <param name="machineUpdate">From Json converted machine object</param>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">If the id is not properly formated or body and route do not match.</response>
        /// /// <response code="404">If no machine with id could be found.</response>
        [HttpPut("{id}", Name = "Patch")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Machine>> Patch(string id, [FromBody] UpdateMachineCommand machineUpdate)
        {
            // Example of bare bones input validation. Ensure that
            // - route and body reference the same id
            // - id is a proper Guid
            if ((machineUpdate.Id != id) || (!Guid.TryParse(machineUpdate.Id, out var _)))
            {
                return BadRequest();
            }

            var updatedMachine = await _machineService.UpdateMachineAsync(machineUpdate);

            if (updatedMachine == null)
            {
                return NotFound();
            }

            return updatedMachine;
        }
    }
}