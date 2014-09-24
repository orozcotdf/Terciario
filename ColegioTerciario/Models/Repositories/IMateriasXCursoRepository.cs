using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.DAL.Models;

namespace ColegioTerciario.Models.Repositories
{
    public interface IMateriasXCursoRepository : IDisposable
    {
        IEnumerable<Materia_x_Curso> GetMateriasXCursos();
        List<int> InsertMateriasXCursos(List<Materia_x_Curso> materias_x_cursos);
        int InsertMateriaXCurso(Materia_x_Curso materia_x_curso);
    }
}
