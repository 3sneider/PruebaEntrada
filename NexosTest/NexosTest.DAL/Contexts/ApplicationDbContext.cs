using Microsoft.EntityFrameworkCore;
using NexosTest.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexosTest.DAL.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Doctor> Doctores { get; set; }

        public DbSet<Paciente> Pacientes { get; set; }
    }
}
