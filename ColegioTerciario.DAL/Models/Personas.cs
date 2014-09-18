using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ColegioTerciario.DAL.Models
{
    public class Personas
    {
        public int ID { get; set; }
        public string PERSONA_CODIGO { get; set; }
        public string PERSONA_USUARIO { get; set; }
        public string PERSONA_CLAVE { get; set; }
        [Display(Name="Nombre")]
        public string PERSONA_NOMBRE { get; set; }
        [Display(Name = "Apellido")]
        public string PERSONA_APELLIDO { get; set; }
        public string PERSONA_DOCUMENTO_TIPO { get; set; }
        public string PERSONA_DOCUMENTO_NUMERO { get; set; }
        public Nullable<System.DateTime> PERSONA_NACIMIENTO_FECHA { get; set; }
        public string PERSONA_EMAIL { get; set; }
        public string PERSONA_DOMICILIO { get; set; }
        public string PERSONA_TELEFONO { get; set; }
        public string PERSONA_SEXO { get; set; }
        public Nullable<System.DateTime> PERSONA_FECHA_ALTA { get; set; }
        public Nullable<System.DateTime> PERSONA_FECHA_BAJA { get; set; }
        public string PERSONA_TITULO_SECUNDARIO { get; set; }
        public string PERSONA_OBSERVACION { get; set; }
        public string PERSONA_FOTO { get; set; }
        public Nullable<int> PERSONA_NACIMIENTO_PAIS_ID { get; set; }
        public virtual Paises PERSONA_NACIMIENTO_PAIS { get; set; }
        public virtual Provincias PERSONA_NACIMIENTO_PROVINCIA { get; set; }
        public virtual Ciudades PERSONA_NACIMIENTO_CIUDAD { get; set; }
        public string PERSONA_NACIMIENTO_PAIS_NOMBRE { get; set; }
        public string PERSONA_NACIMIENTO_PROVINCIA_NOMBRE { get; set; }
        public string PERSONA_NACIMIENTO_CIUDAD_NOMBRE { get; set; }
        public string PERSONA_CUIL { get; set; }
        public Nullable<int> PERSONA_BARRIO { get; set; }
        public Nullable<bool> PERSONA_ES_ALUMNO { get; set; }
        public Nullable<bool> PERSONA_ES_DOCENTE { get; set; }
        public Nullable<bool> PERSONA_ES_NODOCENTE { get; set; }

   
    }
}
