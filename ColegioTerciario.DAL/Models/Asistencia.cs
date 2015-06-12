using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.DAL.Interfaces;
using ColegioTerciario.Models.Types;

namespace ColegioTerciario.DAL.Models
{
    [Table("Asistencias")]
    public class Asistencia : EntityBase, IDescriptible
    {
        public int ID { get; set; }
        public int? MATERIA_X_CURSOS_ID { get; set; }
        public DateTime ASISTENCIA_FECHA { get; set; }
        public int? ASISTENCIA_ALUMNO_ID { get; set; }
        public Boolean ASISTENCIA_PRESENTE { get; set; }

        #region Navegacion

        [ForeignKey("MATERIA_X_CURSOS_ID")]
        public virtual Materia_x_Curso MATERIA_X_CURSO { get; set; }

        [ForeignKey("ASISTENCIA_ALUMNO_ID")]
        public virtual Persona ALUMNO { get; set; }
        #endregion


        public string Describir()
        {
            return "";
        }
    }
}
