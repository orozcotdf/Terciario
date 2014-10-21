using ColegioTerciario.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.Models.Repositories
{
    interface IPersonasRepository : IDisposable
    {
        IEnumerable<Persona> GetPersonasByActa(int acta_examen_id);
    }
}
