using ColegioTerciario.Models;
using ColegioTerciario.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.Models.Repositories
{
    class MateriasXCursoRepository : IMateriasXCursoRepository, IDisposable
    {
        private ColegioTerciarioContext dbContext;

        public MateriasXCursoRepository(ColegioTerciarioContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IEnumerable<DAL.Models.Materia_x_Curso> GetMateriasXCursos()
        {
            return this.dbContext.Materias_X_Cursos;
        }

        public List<int> InsertMateriasXCursos(List<DAL.Models.Materia_x_Curso> materias_x_cursos)
        {
            foreach (DAL.Models.Materia_x_Curso materia_x_curso in materias_x_cursos)
            {
                if (ChequeaSiExisteCurso(materia_x_curso)) throw new Exception();
            }



            List<int> ids = new List<int>();
            using (var dbTransact = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DAL.Models.Materia_x_Curso materia_x_curso in materias_x_cursos)
                    {
                        int id = this.InsertMateriaXCurso(materia_x_curso);
                        ids.Add(id);
                    }
                    dbTransact.Commit();
                    return ids;
                }
                catch (Exception)
                {
                    dbTransact.Rollback();
                    return ids;
                }
                
                
            }
        }

        public int InsertMateriaXCurso(DAL.Models.Materia_x_Curso materia_x_curso)
        {
            this.dbContext.Materias_X_Cursos.Add(materia_x_curso);
            this.dbContext.SaveChanges();
            return materia_x_curso.ID;
        }

        private bool disposed = false;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public bool ChequeaSiExisteCurso(DAL.Models.Materia_x_Curso materia_x_curso)
        {
            var temp = dbContext.Materias_X_Cursos.
                Where(m => m.MATERIA_X_CURSO_CURSO_NOMBRE == materia_x_curso.MATERIA_X_CURSO_CURSO_NOMBRE && m.MATERIA_X_CURSO_CICLO.ID == materia_x_curso.MATERIA_X_CURSO_CICLO.ID).Count();
            if (temp > 0)
                return true;
            else
                return false;
        }
    }
}
