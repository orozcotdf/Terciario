using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels
{
    public class CursadasViewModel
    {
        public string EstadoDefinitivo { get; set; }
        public string AlumnoApellido { get; set; }

        public string AlumnoNombre { get; set; }

        public string DocumentoNumero { get; set; }

        public int ID { get; set; }

        public string EstadoAcademico { get; set; }

        public string EstadoAsistencia { get; set; }

        public string NotaP1 { get; set; }

        public string NotaR1 { get; set; }

        public string NotaP2 { get; set; }

        public string NotaR2 { get; set; }
        public int AlumnoID { get; set; }
    }
}