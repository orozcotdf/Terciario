using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels
{
    public class AjaxCollectionParamViewModel
    {
        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }

        public string OrdenarPorColumna { get; set; }

        public bool OrdenarAsc { get; set; }
        public string Filtro { get; set; }
    }
}