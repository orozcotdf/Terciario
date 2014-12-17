using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.Types;

namespace ColegioTerciario.DAL.Models
{
    [Table("Cursadas")]
    public class Cursada : EntityBase
    {
        public int ID { get; set; }
        public string CURSADA_NOTA_P1 { get; set; }
        public string CURSADA_NOTA_P2 { get; set; }
        public string CURSADA_NOTA_R1 { get; set; }
        public string CURSADA_NOTA_R2 { get; set; }

        #region Foreign Keys
        public int? CURSADA_ALUMNOS_ID { get; set; }
        public int? CURSADA_MATERIAS_X_CURSOS_ID { get; set; }
        #endregion

        #region Navegacion
        [ForeignKey("CURSADA_ALUMNOS_ID"), InverseProperty("PERSONA_CURSADAS")]
        public Persona CURSADA_ALUMNO { get; set; }
        [ForeignKey("CURSADA_MATERIAS_X_CURSOS_ID")]
        public Materia_x_Curso CURSADA_MATERIA_X_CURSO { get; set; }
        #endregion
    }
}
