using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    [Table("Turnos_Examenes")]
    public class Turno_Examen // EX Llamados
    { 
        public int ID { get; set; }
        public int TURNO_EXAMEN_NUMERO { get; set; }
        [Required]
        public string TURNO_EXAMEN_NOMBRE { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> TURNO_EXAMEN_FECHA_INICIO { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> TURNO_EXAMEN_FECHA_FIN { get; set; }

        #region Foreign Keys
        public int? TURNO_EXAMEN_CICLOS_ID { get; set; }
        #endregion

        #region Navegacion
        [ForeignKey("TURNO_EXAMEN_CICLOS_ID")]
        public Ciclo TURNO_EXAMEN_CICLO { get; set; }
        public ICollection<Acta_Examen> ACTAS_EXAMENES { get; set; }
        #endregion
    }
}
