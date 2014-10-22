using ColegioTerciario.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.Repositories
{
    public class PersonasRepository : IPersonasRepository
    {
        private ColegioTerciarioContext dbContext = new ColegioTerciarioContext();
        private bool disposed = false;

        public IEnumerable<Persona> GetPersonasByActa(int acta_examen_id)
        {
            List<Persona> alumnos = new List<Persona>();
            
            var actas_detalles = dbContext.Actas_Examenes_Detalles
               .Include("ACTA_EXAMEN_DETALLE_ALUMNO")
               .Where(c => c.ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID == acta_examen_id)
               .OrderBy(a => a.ACTA_EXAMEN_DETALLE_ALUMNO.PERSONA_APELLIDO)
               .ToList();

            foreach (Acta_Examen_Detalle detalle in actas_detalles)
            {
                alumnos.Add(detalle.ACTA_EXAMEN_DETALLE_ALUMNO);
            }
            return alumnos;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dbContext.Dispose();
                }
            }
            this.disposed = true;
        }


        public IEnumerable<Persona> GetAlumnos()
        {
            return dbContext.Personas.OrderBy(a => a.PERSONA_APELLIDO).ToList();
        }
    }
}