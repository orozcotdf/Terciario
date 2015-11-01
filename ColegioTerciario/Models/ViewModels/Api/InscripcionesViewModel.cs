using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.ViewModels.Api
{
    public class InscripcionesViewModel
    {
        [Required]
        public string INSCRIPCIONES_APELLIDO { get; set; }
        [Required]
        public string INSCRIPCIONES_NOMBRE { get; set; }
        public int? INSCRIPCIONES_CARRERA_ID { get; set; }
        public string INSCRIPCIONES_DOCUMENTO_NUMERO { get; set; }
        public string INSCRIPCIONES_DOCUMENTO_TIPO { get; set; }
        public string INSCRIPCIONES_DOMICILIO { get; set; }
        [Required]
        public string INSCRIPCIONES_EMAIL { get; set; }
        public int? INSCRIPCIONES_NACIMIENTO_BARRIO_ID { get; set; }
        public int? INSCRIPCIONES_NACIMIENTO_CIUDAD_ID { get; set; }
        public DateTime? INSCRIPCIONES_NACIMIENTO_FECHA { get; set; }
        public int? INSCRIPCIONES_NACIMIENTO_PAIS_ID { get; set; }
        public int? INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID { get; set; }
        public string INSCRIPCIONES_SEXO { get; set; }
        public string INSCRIPCIONES_TELEFONO { get; set; }
        public string INSCRIPCIONES_TITULO_SECUNDARIO { get; set; }
    }
}