using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ColegioTerciario.DAL.Models;

namespace ColegioTerciario.Models.ViewModels
{
    public class InscripcionesViewModel
    {
        public int? ID { get; set; }
        public string TURNO_EXAMEN_NOMBRE { get; set; }
        public List<Acta_Examen> ACTAS_EXAMENES { get; set; }
        public Turno_Examen Turno { get; set; }
    }

    public class ErrorViewModel
    {
        public int? MESA { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class InscripcionesActaExamenViewModel
    {
        public string MATERIA;
        public int? ID;
        public Boolean ESTADO;
        public string LIBRO;
        public string FOLIO;
        public DateTime? FECHA;
        public string CARRERA;
    }
        
}