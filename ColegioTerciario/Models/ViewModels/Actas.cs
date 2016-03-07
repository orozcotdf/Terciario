using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels
{
    public class ActasDataTablesViewModel
    {
        public int ID { get; set; }
        public string Fecha { get; set; }
        public string Libro { get; set; }
        public string Folio { get; set; }
        public string Turno { get; set; }
        public string Carrera { get; set; }
        public string Materia { get; set; }
        public string Presidente { get; set; }
        public string Vocal1 { get; set; }
        public string Vocal2 { get; set; }
    }

    

    public class SituacionFinalesViewModel
    {
        public int CarreraID { get; set; }
        public string Persona { get; set; }
        public string Carrera { get; set; }
        public IEnumerable<FinalesViewModel> Finales { get; set; }

    }

    public class FinalesViewModel
    {
        public string Anio { get; set; }
        public IEnumerable<ExamenesFinalesViewModel> Examenes { get; set; }
        
    }

    public class ExamenesFinalesViewModel
    {
        public int ActaId { get; set; }
        public DateTime? Fecha { get; set; }
        public string Materia { get; set; }
        public string Nota { get; set; }
        public string Estado { get; set; }
        public string Anio { get; set; }
        public string Folio { get; set; }
        public string Libro { get; set; }

        public string CodigoMateria { get; set; }
    }
}