using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.Types;

namespace ColegioTerciario.DAL.Models
{
    [Table("Cursadas")]
    [TrackChanges]
    public class Cursada : EntityBase
    {
        public int ID { get; set; }
        [DefaultValue("Ausente")]
        public string CURSADA_NOTA_P1 { get; set; }
        [DefaultValue("Ausente")]
        public string CURSADA_NOTA_P2 { get; set; }
        [DefaultValue("Ausente")]
        public string CURSADA_NOTA_R1 { get; set; }
        [DefaultValue("Ausente")]
        public string CURSADA_NOTA_R2 { get; set; }
        [DefaultValue(false)]
        public bool CURSADA_P1_REGULAR { get; set; }
        [DefaultValue(false)]
        public bool CURSADA_P2_REGULAR { get; set; }
        public string CURSADA_ESTADO_ACADEMICO { get; set; }
        public string CURSADA_ESTADO_ASISTENCIA { get; set; }
        public string CURSADA_ESTADO_DEFINITIVO { get; set; }
        public Nullable<DateTime> CURSADA_FECHA_VENCIMIENTO { get; set; }
        [Display(Name = "Fecha de Vencimiento")]

        #region Foreign Keys
        public int? CURSADA_ALUMNOS_ID { get; set; }
        public int? CURSADA_MATERIAS_X_CURSOS_ID { get; set; }
        #endregion

        #region Navegacion
        [ForeignKey("CURSADA_ALUMNOS_ID"), InverseProperty("PERSONA_CURSADAS")]
        public Persona CURSADA_ALUMNO { get; set; }
        [ForeignKey("CURSADA_MATERIAS_X_CURSOS_ID")]
        public Materia_x_Curso CURSADA_MATERIA_X_CURSO { get; set; }
        #endregion
    }
}
