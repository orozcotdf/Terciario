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
        public string Turno { get; set; }

        public int[] Lunes { get; set; }
        public int[] Martes { get; set; }
        public int[] Miercoles { get; set; }
        public int[] Jueves { get; set; }
        public int[] Viernes { get; set; }
        public int[] Sabado { get; set; }
        public int[] Domingo { get; set; }

    }

    public class Horarios
    {
        public string Lunes { get; set; }
        public string Martes { get; set; }
        public string Miercoles { get; set; }
        public string Jueves { get; set; }
        public string Viernes { get; set; }
        public string Sabado { get; set; }
        public string Domingo { get; set; }
    }
}