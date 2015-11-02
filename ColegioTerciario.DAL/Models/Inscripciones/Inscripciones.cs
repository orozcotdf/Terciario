using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.Types;
using Newtonsoft.Json;

namespace ColegioTerciario.DAL.Models.Inscripciones
{
    public class Inscripciones : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int INSCRIPCIONES_IDENTIFICADOR { get; set; }
        public string INSCRIPCIONES_CODIGO { get; set; }
        public string INSCRIPCIONES_USUARIO { get; set; }
        public string INSCRIPCIONES_CLAVE { get; set; }
        [Required]
        public string INSCRIPCIONES_NOMBRE { get; set; }
        [Required]
        public string INSCRIPCIONES_APELLIDO { get; set; }
        public string INSCRIPCIONES_NOMBRE_PARA_MOSTRAR { get; set; }
        public string INSCRIPCIONES_DOCUMENTO_TIPO { get; set; }
        [Required]
        [RegularExpression("[0-9]{7,}", ErrorMessage = "DNI Invalido, no puede contener espacios, puntos o simbolos")]
        public string INSCRIPCIONES_DOCUMENTO_NUMERO { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? INSCRIPCIONES_NACIMIENTO_FECHA { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "El Email no es valido")]
        public string INSCRIPCIONES_EMAIL { get; set; }
        public string INSCRIPCIONES_DOMICILIO { get; set; }
        public string INSCRIPCIONES_TELEFONO { get; set; }
        public string INSCRIPCIONES_SEXO { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? INSCRIPCIONES_FECHA_ALTA { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? INSCRIPCIONES_FECHA_BAJA { get; set; }
        public string INSCRIPCIONES_TITULO_SECUNDARIO { get; set; }
        public string INSCRIPCIONES_OBSERVACION { get; set; }
        public string INSCRIPCIONES_FOTO { get; set; }
        public string INSCRIPCIONES_CUIL { get; set; }
        public bool? INSCRIPCIONES_ES_ALUMNO { get; set; }
        public bool? INSCRIPCIONES_ES_DOCENTE { get; set; }
        public bool? INSCRIPCIONES_ES_NODOCENTE { get; set; }
        public int? INSCRIPCIONES_NACIMIENTO_PAIS_ID { get; set; }
        public int? INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID { get; set; }
        public int? INSCRIPCIONES_NACIMIENTO_CIUDAD_ID { get; set; }
        public int? INSCRIPCIONES_NACIMIENTO_BARRIO_ID { get; set; }
        public int? INSCRIPCIONES_CARRERA_ID { get; set; }
        [ForeignKey("INSCRIPCIONES_NACIMIENTO_PAIS_ID")]
        public virtual Pais INSCRIPCIONES_NACIMIENTO_PAIS { get; set; }
        [ForeignKey("INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID")]
        public virtual Provincia INSCRIPCIONES_NACIMIENTO_PROVINCIA { get; set; }
        [ForeignKey("INSCRIPCIONES_NACIMIENTO_CIUDAD_ID")]
        public virtual Ciudad INSCRIPCIONES_NACIMIENTO_CIUDAD { get; set; }
        [ForeignKey("INSCRIPCIONES_NACIMIENTO_BARRIO_ID")]
        public virtual Barrio INSCRIPCIONES_BARRIO { get; set; }

        [ForeignKey("INSCRIPCIONES_CARRERA_ID")]
        public virtual InscripcionesCarrera INSCRIPCIONES_CARRERA { get; set; }
    }
}
