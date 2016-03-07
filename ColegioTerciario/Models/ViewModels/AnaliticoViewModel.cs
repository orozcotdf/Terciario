using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColegioTerciario.DAL.Models;

namespace ColegioTerciario.Models.ViewModels
{
    public class AnaliticoFinalesViewModel
    {

        public int ActaId { get; set; }

        public DateTime? Fecha { get; set; }

        public string Materia { get; set; }

        public string Nota { get; set; }

        public string Estado { get; set; }

        public string Libro { get; set; }

        public string Folio { get; set; }
    }
    public class ListaAnaliticoViewModel
    {
        public string Anio { get; set; }
        public IEnumerable<AnaliticoFinalesViewModel> Finales { get; set; }
    }

    public class AnaliticoViewModel
    {
        public Persona Persona { get; set; }
        public IEnumerable<ListaAnaliticoViewModel> Analitico { get; set; }
        public string Carrera { get; set; }
    }
}
