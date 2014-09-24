using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    [Table("Materias_X_Cursos")]
    public class Materia_x_Curso
    {
        public int ID { get; set; }

        #region FOREIGN KEYS
        public int? MATERIA_X_CURSO_CICLOS_ID { get; set; }
        public int? MATERIA_X_CURSO_CARRERAS_ID { get; set; }
        public int? MATERIA_X_CURSO_MATERIAS_ID { get; set; }
        public int? MATERIA_X_CURSO_DOCENTE_ID { get; set; }
        public int? MATERIA_X_CURSO_SEDES_ID { get; set; }
        #endregion

        #region Navegacion
        [ForeignKey("MATERIA_X_CURSO_CICLOS_ID")]
        public Ciclo MATERIA_X_CURSO_CICLO { get; set; }
        [ForeignKey("MATERIA_X_CURSO_CARRERAS_ID")]
        public Carrera MATERIA_X_CURSO_CARRERA { get; set; }
        [ForeignKey("MATERIA_X_CURSO_MATERIAS_ID")]
        public Materia MATERIA_X_CURSO_MATERIA { get; set; }
        [ForeignKey("MATERIA_X_CURSO_DOCENTE_ID")]
        public Persona MATERIA_X_CURSO_DOCENTE { get; set; }
        [ForeignKey("MATERIA_X_CURSO_SEDES_ID")]
        public virtual Sede MATERIA_X_CURSO_SEDE { get; set; }
        public virtual ICollection<Horario_Cursada> Horarios_Cursadas { get; set; }
        #endregion

        public string MATERIA_X_CURSO_CURSO_NOMBRE { get; set; }
        public bool MATERIA_X_CURSO_ES_PROMOCIONAL { get; set; }
        public Nullable<DateTime> MATERIA_X_CURSO_P1_FECHA { get; set; }
        public Nullable<DateTime> MATERIA_X_CURSO_P2_FECHA { get; set; }
        public Nullable<DateTime> MATERIA_X_CURSO_R1_FECHA { get; set; }
        public Nullable<DateTime> MATERIA_X_CURSO_R2_FECHA { get; set; }
    }
}
