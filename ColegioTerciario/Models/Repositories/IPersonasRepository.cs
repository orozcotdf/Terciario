using ColegioTerciario.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.ViewModels;

namespace ColegioTerciario.Models.Repositories
{
    interface IPersonasRepository : IDisposable
    {
        IEnumerable<Persona> GetPersonasByActa(int acta_examen_id);
        IEnumerable<Persona> GetAlumnos();
        IQueryable<SituacionAcademicaPorCiclosViewModel> GetSituacionAcademicaPorCiclos(Persona persona);
        IQueryable<SituacionAcademicaPorMateriasViewModel> GetSituacionAcademicaPorMaterias(Persona persona);
        IQueryable<SituacionFinalesViewModel> GetFinales(Persona persona);
    }
}
