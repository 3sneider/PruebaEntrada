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
    public class DoctoresController : ControllerBase
    {
        #region properties
        private readonly ApplicationDbContext context;
        private readonly ILogger<DoctoresController> logger;
        private readonly IMapper mapper;

        private DoctoresController(ApplicationDbContext context, ILogger<DoctoresController> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        #endregion

        #region methods

        /// <summary>
        /// trae un listado de doctores
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Doctores")]
        async public Task<ActionResult<IEnumerable<DoctorDTO>>> Get()
        {
            try
            {
                logger.LogInformation("cargando doctores");
                Ok();
                var doctores =  await context.Doctores.Include(x => x.Pacientes).ToListAsync();
                return mapper.Map<List<DoctorDTO>>(doctores);
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
        /// filtra los doctores por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/Doctor/{id}", Name = "getDoctor")]
        async public Task<ActionResult<DoctorDTO>> Get(int id)
        {
            try
            {
                logger.LogInformation("cargando doctor");
                var doctor = await context.Doctores.Include(x => x.Pacientes).FirstOrDefaultAsync(x => x.id == id);

                if (doctor == null)
                {
                    logger.LogWarning($"id {id} no encontrado");
                    return NotFound();
                }

                return mapper.Map<DoctorDTO>(doctor);
            }
            catch (Exception)
            {
                logger.LogError("error en la ejecucion");
                return BadRequest();
            }
        }

        /// <summary>
        /// crea un nuevo registro de doctores
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        [HttpPost("/CrearDoctor")]
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

        #endregion
    }
}