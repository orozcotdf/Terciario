using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ColegioTerciario.DAL.Models;

namespace ColegioTerciario.Models.ViewModels
{
    public class SituacionAcademicaPorCiclosViewModel
    {
        public string Curso { get; set; }
        public string Carrera { get; set; }
        public IEnumerable<SituacionAcademicaCursadasViewModel> Cursadas { get; set; }
    }

    public class SituacionAcademicaPorMateriasViewModel
    {
        public string Curso { get; set; }
        public string Carrera { get; set; }
        public IEnumerable<SituacionAcademicaPorMateriasMateriasViewModel> Materias { get; set; }
    }

    public class SituacionAcademicaCursadasViewModel
    {
        public string Ciclo { get; set; }
        public IEnumerable<SituacionAcademicaMateriasViewModel> Materias { get; set; }
    }

    public class SituacionAcademicaMateriasViewModel
    {
        public string MateriaNombre { get; set; }
        public string Estado { get; set; }
        public string Ciclo { get; set; }

        public string P1 { get; set; }
        public string R1 { get; set; }
        public string P2 { get; set; }
        public string R2 { get; set; }
        public DateTime? P1Fecha { get; set; }
        public DateTime? R1Fecha { get; set; }
        public DateTime? P2Fecha { get; set; }
        public DateTime? R2Fecha { get; set; }

        public int MateriaXCursoID { get; set; }
    }


    public class SituacionAcademicaPorMateriasMateriasViewModel
    {
        public string Materia { get; set; }
        public IEnumerable<SituacionAcademicaMateriasViewModel> Cursadas { get; set; }
    }
}