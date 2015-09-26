using ColegioTerciario.Models.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColegioTerciario.DAL.Models
{
    [Table("Equivalencias")]
    public class Equivalencia : EntityBase
    {
        public int ID { get; set; }
        public Nullable<DateTime> EQUIVALENCIA_FECHA { get; set; }
        public String EQUIVALENCIA_NRO_DISPOSICION { get; set; }
        #region Foreign Keys

        public int? EQUIVALENCIA_ALUMNO_ID { get; set; }
        public int? EQUIVALENCIA_CARRERA_ID { get; set; }

        #endregion Foreign Keys

        #region Navegacion

        [ForeignKey("EQUIVALENCIA_ALUMNO_ID")]
        public virtual Persona EQUIVALENCIA_ALUMNO { get; set; }

        [ForeignKey("EQUIVALENCIA_CARRERA_ID")]
        public virtual Carrera EQUIVALENCIA_CARRERA { get; set; }

        public virtual ICollection<Equivalencia_Detalle> EQUIVALENCIA_DETALLES { get; set; }

        #endregion Navegacion
    }
}