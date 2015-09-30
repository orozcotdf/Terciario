using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels.Api
{
    public class CursosPorDocenteViewModel
    {
        public string Anio { get; set; }
        public int? SedeId { get; set; }
        public string CarreraId { get; set; }
        public string CursoNombre { get; set; }
        public string SedeNombre { get; set; }
    }
}