using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NexosTest.DAL.Contexts;
using NexosTest.Entities.Entities;
using NexosTest.Entities.Models;

namespace NexosTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        #region properties
        private readonly ApplicationDbContext context;
        private readonly ILogger<PacientesController> logger;
        private readonly IMapper mapper;

        public PacientesController(ApplicationDbContext context, ILogger<PacientesController> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }
        #endregion

        #region methods

        /// <summary>
        /// trae los pacientes
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Pacientes")]
        [ResponseCache(Duration = 240)] // No funciona en google chrome, la cabecera llega max-age 0
        async public Task<ActionResult<IEnumerable<PacienteDTO>>> Get()
        {
            try
            {
                logger.LogInformation("cargando pacientes");
                Ok();
                var pacientes = await context.Pacientes.Include(x => x.doctor).ToListAsync();
                return mapper.Map<List<PacienteDTO>>(pacientes);
                // separando la logica { quitar }
                //DoctoresBL doctoresBL = new DoctoresBL(context);
                //IEnumerable<Doctor> respuesta = doctoresBL.GelAllDoctors();
                //return respuesta as ActionResult;
            }
            catch (Exception)
            {
                logger.LogError("error en la ejecucion");
                return BadRequest();
            }

        }

        /// <summary>
        /// filtra los pacientes por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/Paciente/{id}", Name = "getPaciente")]
        async public Task<ActionResult<PacienteDTO>> Get(int id)
        {
            try
            {
                logger.LogInformation("cargando paciente");
                var paciente = await context.Pacientes.Include(x => x.doctor).FirstOrDefaultAsync(x => x.id == id);

                if (paciente == null)
                {
                    logger.LogWarning($"id {id} no encontrado");
                    return NotFound();
                }

                return mapper.Map<PacienteDTO>(paciente);
            }
            catch (Exception)
            {
                logger.LogError("error en la ejecucion");
                return BadRequest();
            }
        }

        /// <summary>
        /// crea un nuevo registro de pacientes
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        [HttpPost("/CrearPaciente")]
        async public Task<ActionResult> Post([FromBody] Doctor doctor)
        {
            try
            {
                logger.LogDebug("validando creacion de datos");
                context.Doctores.Add(doctor);
                await context.SaveChangesAsync();
                return new CreatedAtRouteResult("getDoctor", new { id = doctor.id }, doctor);
            }
            catch (Exception)
            {
                logger.LogError("error en la ejecucion");
                return NotFound();
            }
        }

        /// <summary>
        /// actualia datos de un paciente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paciente"></param>
        /// <returns></returns>
        [HttpPut("/ActualizarPaciente/{id}")]
        async public Task<ActionResult> Put(int id, [FromBody] Paciente paciente)
        {
            try
            {
                if (id != paciente.id)
                {
                    logger.LogWarning($"id {id} no encontrado");
                    return BadRequest();
                }

                context.Entry(paciente).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception)
            {
                logger.LogError("error en la ejecucion");
                return BadRequest();
            }
        }

        /// <summary>
        /// elimina un registro de la tabla pacientes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/EliminarPaciente/{id}")]
        async public Task<ActionResult<PacienteDTO>> Delete(int id)
        {
            try
            {
                var paciente = await context.Pacientes.FirstOrDefaultAsync(x => id == x.id);

                if (paciente == null)
                {
                    return NotFound();
                }

                context.Pacientes.Remove(paciente);
                context.SaveChanges();
                return mapper.Map<PacienteDTO>(paciente);
            }
            catch (Exception)
            {
                logger.LogError("error en la ejecucion");
                return BadRequest();
            }
        }

        #endregion
    }
}