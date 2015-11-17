using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColegioTerciario.Controllers.Api
{
    public class AlumnoEnCursadaViewModel
    {
        public string Alumno { get; set; }
        public string Nota { get; set; }
        public int CursadaId { get; set; }
        public bool Libre { get; set; }
        public bool Regular { get; set; }
        public string Documento { get; set; }
    }
}
