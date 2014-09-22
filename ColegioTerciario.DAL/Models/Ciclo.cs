using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    [Table("Ciclos")]
    public class Ciclo
    {
        public int ID { get; set; }
        [Required]
        public string CICLO_NOMBRE { get; set; }
        public Nullable<DateTime> CICLO_INICIO { get; set; }
        public Nullable<DateTime> CICLO_FIN { get; set; }
        public string CICLO_ANIO { get; set; }
        public Nullable<DateTime> CICLO_MATRICULA_INICIO { get; set; }
        public Nullable<DateTime> CICLO_MATRICULA_FIN { get; set; }

        public Nullable<DateTime> CICLO_SEMESTRE_1_INICIO { get; set; }
        public Nullable<DateTime> CICLO_SEMESTRE_1_FIN { get; set; }
        public Nullable<DateTime> CICLO_SEMESTRE_2_INICIO { get; set; }
        public Nullable<DateTime> CICLO_SEMESTRE_2_FIN { get; set; }

        public virtual ICollection<Turno_Examen> TURNOS_EXAMENES { get; set; }
    }
}
