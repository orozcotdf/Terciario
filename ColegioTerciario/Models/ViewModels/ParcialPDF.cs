using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ColegioTerciario.DAL.Models;

namespace ColegioTerciario.Models.ViewModels
{
    public class ParcialPDF
    {
        public List<Integrante> Integrantes { get; set; }
        public string Nombre { get; set; }
        public string Ciclo { get; set; }
        public string Carrera { get; set; }
        public string Materia { get; set; }
        public string Docente { get; set; }
        public string Sede { get; set; }
        public DateTime? Fecha { get; set; }
        public string Instancia { get; set; }

        public int Inscriptos { get; set; }
        public int Aprobados { get; set; }
        public int Desaprobados { get; set; }
        public int Ausentes { get; set; }
        public int Examinados { get; set; }
    }

    public class Integrante
    {
        public string Calificacion { get; set; }
        public Persona Persona { get; set; }
    }
}