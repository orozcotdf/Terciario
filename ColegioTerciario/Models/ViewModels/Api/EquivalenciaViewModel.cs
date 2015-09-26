using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels.Api
{
    public class EquivalenciaViewModel
    {
        public int ID { get; set; }
        public System.Nullable<DateTime> EQUIVALENCIA_FECHA { get; set; }
        public String EQUIVALENCIA_NRO_DISPOSICION { get; set; }
        public string EQUIVALENCIA_ALUMNO_NOMBRE { get; set; }
        public string EQUIVALENCIA_CARRERA_NOMBRE { get; set; }
        public int? EQUIVALENCIA_CARRERA_ID { get; set; }
        public int? EQUIVALENCIA_ALUMNO_ID { get; set; }
    }
}