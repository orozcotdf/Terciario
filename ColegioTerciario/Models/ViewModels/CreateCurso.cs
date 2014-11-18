using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels
{
    public class CreateCurso
    {
        public int Sede { get; set; }
        public int Ciclo { get; set; }
        public int Carrera { get; set; }
        public int Año { get; set; }
        public string[] Nros { get; set; }

    }
}