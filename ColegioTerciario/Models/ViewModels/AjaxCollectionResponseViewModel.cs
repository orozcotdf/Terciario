using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels
{
    public class AjaxCollectionResponseViewModel
    {
        public int CantidadPaginas { get; set; }
        public IQueryable<object> Resultados { get; set; }
        public int CantidadResultados { get; set; }
    }
}