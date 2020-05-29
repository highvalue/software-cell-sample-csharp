using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleAPI.Contracts;
using SampleAPI.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MachineAPI.Gate.Dapr
{
    [ApiController]
    public class MachinesDaprController : ControllerBase
    {
        private readonly ILogger<MachinesDaprController> _logger;
        private readonly IMachineService _machineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MachinesDaprController"/> class.
        /// </summary>
        /// <param name="machineService">Instance of the IMachineService.</param>
        /// <param name="logger">The logger.</param>
        public MachinesDaprController(ILogger<MachinesDaprController> logger, IMachineService machineService)
        {
            this._logger = logger;
            this._machineService = machineService;
        }

        /// <summary>
        /// GET api returning all items.
        /// </summary>
        /// <returns>code 200 including json.</returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Machine>>> Get()
        {
            return await _machineService.GetMachinesAsync();
        }

        /// <summary>
        /// GET api returning the item specified by id by calling.
        /// </summary>
        /// <param name="id">id of the item.</param>
        /// <returns>Not found status code or code 200 including json.</returns>
        [HttpGet("{id}")]
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
        /// POST api for storing machines.
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
        [HttpPost("Create")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Machine>> Post([FromBody] CreateMachineCommand createCommand)
        {
            var newMachine = await _machineService.AddMachineAsync(createCommand);

            return this.CreatedAtAction(nameof(Post), new { id = newMachine.Id }, newMachine);
        }

        /// <summary>
        /// Patch api for updating a machine.
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
        [HttpPatch("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Machine>> Patch(string id, [FromBody] UpdateMachineCommand machineUpdate)
        {
            // Example of bare bones input validation. Ensure that
            // - route and body reference the same id
            if (machineUpdate.Id != id)
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

        /// <summary>
        /// PUT api for updating a machine.
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
        /// <param name="machine">From Json converted machine object</param>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">If <paramref name="id"/> and <paramref name="update"/> don't match.</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Machine>> Put(string id, [FromBody] UpdateMachineCommand update)
        {
            // Example of bare bones input validation.
            // Ensure that route and body reference the same id
            if (update.Id != id)
            {
                return BadRequest(id);
            }

            var updatedMachine = await _machineService.UpdateMachineAsync(update);

            if (updatedMachine == null)
            {
                return NotFound(id);
            }

            return updatedMachine;
        }
    }
}