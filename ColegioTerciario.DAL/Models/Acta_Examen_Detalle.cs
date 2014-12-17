using System.ComponentModel.DataAnnotations.Schema;
using ColegioTerciario.Models.Types;

namespace ColegioTerciario.DAL.Models
{
    [Table("Actas_Examenes_Detalles")]
    public class Acta_Examen_Detalle : EntityBase
    {
        public int ID { get; set; }
        public string ACTA_EXAMEN_DETALLE_NOTA { get; set; }
        public string ACTA_EXAMEN_DETALLE_ESTADO { get; set; }

        #region Foreign Keys
        public int? ACTA_EXAMEN_DETALLE_ALUMNOS_ID { get; set; }
        public int? ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID { get; set; }
        #endregion

        #region Navegacion
        [ForeignKey("ACTA_EXAMEN_DETALLE_ALUMNOS_ID"), InverseProperty("ACTAS_EXAMENES_DETALLES")]
        public Persona ACTA_EXAMEN_DETALLE_ALUMNO { get; set; }
        [ForeignKey("ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID")]
        public Acta_Examen ACTA_EXAMEN_DETALLE_ACTA_EXAMEN { get; set; }
        #endregion
    }
}
