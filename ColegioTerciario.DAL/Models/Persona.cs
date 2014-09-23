using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColegioTerciario.DAL.Models
{
    [Table("Personas")]
    public class Persona
    {
        public int ID { get; set; }
        [Display(Name = "Codigo")]
        public string PERSONA_CODIGO { get; set; }
        [Display(Name = "Usuario")]
        public string PERSONA_USUARIO { get; set; }
        [Display(Name = "Clave")]
        public string PERSONA_CLAVE { get; set; }
        [Display(Name="Nombre")]
        [Required]
        public string PERSONA_NOMBRE { get; set; }
        [Display(Name = "Apellido")]
        [Required]
        public string PERSONA_APELLIDO { get; set; }
        [Display(Name = "Tipo de Documento")]
        public string PERSONA_DOCUMENTO_TIPO { get; set; }
        [Display(Name = "Documento Nro")]
        [Required]
        public string PERSONA_DOCUMENTO_NUMERO { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        public Nullable<System.DateTime> PERSONA_NACIMIENTO_FECHA { get; set; }
        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "El Email no es valido")]
        public string PERSONA_EMAIL { get; set; }
        [Display(Name = "Domicilio")]
        public string PERSONA_DOMICILIO { get; set; }
        [Display(Name = "Telefono")]
        public string PERSONA_TELEFONO { get; set; }
        [Display(Name = "Sexo")]
        public string PERSONA_SEXO { get; set; }
        [Display(Name = "Fecha de Alta")]
        public Nullable<System.DateTime> PERSONA_FECHA_ALTA { get; set; }
        [Display(Name = "Fecha de Baja")]
        public Nullable<System.DateTime> PERSONA_FECHA_BAJA { get; set; }
        [Display(Name = "Titulo Secundario")]
        public string PERSONA_TITULO_SECUNDARIO { get; set; }
        [Display(Name = "Observacion")]
        public string PERSONA_OBSERVACION { get; set; }
        [Display(Name = "Foto")]
        public string PERSONA_FOTO { get; set; }
        [Display(Name = "CUIL")]
        public string PERSONA_CUIL { get; set; }
        [Display(Name = "Es Alumno")]        
        public bool? PERSONA_ES_ALUMNO { get; set; }
        [Display(Name = "Es Docente")]
        public bool? PERSONA_ES_DOCENTE { get; set; }
        [Display(Name = "Es NO Docente")]
        public bool? PERSONA_ES_NODOCENTE { get; set; }

        [Display(Name = "Pais de Nacimiento")]
        public int? PERSONA_NACIMIENTO_PAIS_ID { get; set; }
        [Display(Name = "Provincia de Nacimiento")]
        public int? PERSONA_NACIMIENTO_PROVINCIA_ID { get; set; }
        [Display(Name = "Ciudad de Nacimiento",Prompt="Seleccione una Ciudad")]
        public int? PERSONA_NACIMIENTO_CIUDAD_ID { get; set; }

        [ForeignKey("PERSONA_NACIMIENTO_PAIS_ID")]        
        public Pais PERSONA_NACIMIENTO_PAIS { get; set; }

        [ForeignKey("PERSONA_NACIMIENTO_PROVINCIA_ID")]
        public Provincia PERSONA_NACIMIENTO_PROVINCIA { get; set; }

        [ForeignKey("PERSONA_NACIMIENTO_CIUDAD_ID")]
        public Ciudad PERSONA_NACIMIENTO_CIUDAD { get; set; }
        
        //public virtual Barrio PERSONA_BARRIO { get; set; }

        public virtual ICollection<Acta_Examen> ACTAS_PRECIDIDAS { get; set; }
        public virtual ICollection<Acta_Examen> ACTAS_VOCAL1 { get; set; }
        public virtual ICollection<Acta_Examen> ACTAS_VOCAL2 { get; set; }
        public virtual ICollection<Acta_Examen_Detalle> ACTAS_EXAMENES_DETALLES { get; set; }

        /* TODO: AGREGAR BARRIO_ID Y NOMBRE_PARA_MOSTRAR */
    }
}
