using ColegioTerciario.Models.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace ColegioTerciario.DAL.Models
{
    [Table("Equivalencias_Detalles")]
    public class Equivalencia_Detalle : EntityBase
    {
        static readonly int TOTAL = 0;
        static readonly int PARCIAL = 1;
        static readonly int DENEGADA = 2;

        public int ID { get; set; }

        public int EQUIVALENCIA_DETALLE_TIPO { get; set; }
        public String EQUIVALENCIA_COMENTARIO { get; set; }

        #region Foreign Keys
        public int? EQUIVALENCIA_ID { get; set; }
        public int? EQUIVALENCIA_DETALLE_MATERIA_ID { get; set; }
        public int? EQUIVALENCIA_DETALLE_PROFESOR_ID { get; set; }
        #endregion Foreign Keys

        #region Navegacion
        [ForeignKey("EQUIVALENCIA_ID"),InverseProperty("EQUIVALENCIA_DETALLES")]
        [JsonIgnore]
        public virtual Equivalencia EQUIVALENCIA { get; set; }
        [ForeignKey("EQUIVALENCIA_DETALLE_MATERIA_ID")]
        [JsonIgnore]
        public virtual Materia EQUIVALENCIA_DETALLE_MATERIA { get; set; }
        [ForeignKey("EQUIVALENCIA_DETALLE_PROFESOR_ID")]
        [JsonIgnore]
        public virtual Persona EQUIVALENCIA_DETALLE_PROFESOR { get; set; }
        #endregion Navegacion

        
        public string EQUIVALENCIA_DETALLE_TIPO_NOMBRE {
            get
            {
                if (EQUIVALENCIA_DETALLE_TIPO == 0)
                {
                    return "Total";
                }
                if (EQUIVALENCIA_DETALLE_TIPO == 1)
                {
                    return "Parcial";
                }
                if (EQUIVALENCIA_DETALLE_TIPO == 2)
                {
                    return "Denegada";
                }
                return "";
            }
        }

    }
}
