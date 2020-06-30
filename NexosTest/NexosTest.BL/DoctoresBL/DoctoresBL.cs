using NexosTest.DAL.Contexts;
using NexosTest.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NexosTest.BL.DoctoresBL
{
    public class DoctoresBL 
    {
        private readonly ApplicationDbContext context;

        public DoctoresBL(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return context.Doctores.ToList();
        }

        public Doctor GetDoctorById(int id)
        {
            return context.Doctores.FirstOrDefault(x => x.id == id);
        }


    }
}
