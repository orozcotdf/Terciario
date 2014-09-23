using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    [Table("Actas_Examenes")]
    public class Acta_Examen
    {
        public int ID { get; set; }

        public Nullable<DateTime> ACTA_EXAMEN_FECHA { get; set; }
        public int ACTA_EXAMEN_INSCRIPTOS { get; set; }
        public int ACTA_EXAMEN_APROBADOS { get; set; }
        public int ACTA_EXAMEN_REPROBADOS { get; set; }
        public int ACTA_EXAMEN_AUSENTES { get; set; }
        public string ACTA_EXAMEN_LIBRO { get; set; }
        public string ACTA_EXAMEN_FOLIO { get; set; }

        #region Foreign Keys
        public int? ACTA_EXAMEN_TURNOS_EXAMENES_ID { get; set; }
        public int? ACTA_EXAMEN_CARRERAS_ID { get; set; }
        public int? ACTA_EXAMEN_MATERIAS_ID { get; set; }
        public int? ACTA_EXAMEN_PRESIDENTE_ID { get; set; }
        public int? ACTA_EXAMEN_VOCAL1_ID { get; set; }
        public int? ACTA_EXAMEN_VOCAL2_ID { get; set; }

        #endregion


        #region Navegacion

        public virtual Turno_Examen ACTA_EXAMEN_TURNO_EXAMEN { get; set; }
        public virtual Materia ACTA_EXAMEN_MATERIA { get; set; }
        public virtual Carrera ACTA_EXAMEN_CARRERA { get; set; }
        public virtual Persona ACTA_EXAMEN_PRESIDENTE { get; set; }
        public virtual Persona ACTA_EXAMEN_VOCAL1 { get; set; }
        public virtual Persona ACTA_EXAMEN_VOCAL2 { get; set; }

        public virtual ICollection<Acta_Examen_Detalle> ACTAS_EXAMENES_DETALLES { get; set; }
        #endregion


    }
}
