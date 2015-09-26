using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.Types;
using Newtonsoft.Json;

namespace ColegioTerciario.DAL.Models
{
    [Table("Ciclos")]
    public class Ciclo : EntityBase
    {
        public int ID { get; set; }
        [Required]
        public string CICLO_NOMBRE { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_INICIO { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_FIN { get; set; }
        public string CICLO_ANIO { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_MATRICULA_SEMESTRE1_INICIO { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_MATRICULA_SEMESTRE1_FIN { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_MATRICULA_SEMESTRE2_INICIO { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_MATRICULA_SEMESTRE2_FIN { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_SEMESTRE_1_INICIO { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_SEMESTRE_1_FIN { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_SEMESTRE_2_INICIO { get; set; }
        [Column(TypeName = "Date")]
        public Nullable<DateTime> CICLO_SEMESTRE_2_FIN { get; set; }

        [JsonIgnore]
        public virtual ICollection<Turno_Examen> TURNOS_EXAMENES { get; set; }
    }
}
