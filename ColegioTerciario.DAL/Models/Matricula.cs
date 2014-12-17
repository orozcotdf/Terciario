using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.Types;

namespace ColegioTerciario.DAL.Models
{
    [Table("Matriculas")]
    public class Matricula : EntityBase
    {
        public int ID { get; set; }
        [Required]
        public string MATRICULA_NOMBRE { get; set; }
        public Nullable<DateTime> MATRICULA_FECHA { get; set; }
        public int MATRICULA_PERSONAS_ID { get; set; }
        public int MATRICULA_CARRERAS_ID { get; set; }
        public int MATRICULA_CICLOS_ID { get; set; }

        [ForeignKey("MATRICULA_PERSONAS_ID")]
        public Persona MATRICULA_ALUMNO { get; set; }
        [ForeignKey("MATRICULA_CARRERAS_ID")]
        public Carrera MATRICULA_CARRERA { get; set; }
        [ForeignKey("MATRICULA_CICLOS_ID")]
        public Ciclo MATRICULA_CICLO { get; set; }
    }
}
