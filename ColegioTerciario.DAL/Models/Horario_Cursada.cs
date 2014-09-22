using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    [Table("Horarios_Cursadas")]
    public class Horario_Cursada // TABLA INTERMEDIA ENTRE HORA Y MATERIA_X_CURSO
    {
        public int ID { get; set; }
        public string HORARIO_CURSADA_DIA { get; set; }


        #region Foreign Keys
        public int? HORARIO_CURSADA_MATERIAS_X_CURSOS_ID { get; set; }
        public int? HORARIO_CURSADA_HORAS_ID { get; set; }
        #endregion

        #region Navegacion
        [ForeignKey("HORARIO_CURSADA_MATERIAS_X_CURSOS_ID")]
        public virtual Materia_x_Curso HORARIO_CURSADA_MATERIA_X_CURSO { get; set; }
        [ForeignKey("HORARIO_CURSADA_HORAS_ID")]
        public virtual Hora HORARIO_CURSADA_HORA { get; set; }
        #endregion
    }
}
