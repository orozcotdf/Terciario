using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    public class Matricula
    {
        public int ID { get; set; }
        [Required]
        public string MATRICULA_NOMBRE { get; set; }
        public Nullable<DateTime> MATRICULA_FECHA { get; set; }
        public int MATRICULA_PERSONA_ID { get; set; }
        public int MATRICULA_CARRERA_ID { get; set; }
        public int MATRICULA_CICLO_ID { get; set; }

        [ForeignKey("MATRICULA_PERSONA_ID")]
        public Persona MATRICULA_PERSONA { get; set; }
        [ForeignKey("MATRICULA_CARRERA_ID")]
        public Carrera MATRICULA_CARRERA { get; set; }
        [ForeignKey("MATRICULA_CICLO_ID")]
        public Ciclo MATRICULA_CICLO { get; set; }
    }
}
