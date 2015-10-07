using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ColegioTerciario.Models.Types;

namespace ColegioTerciario.DAL.Models
{
    [Table("Materias_X_Cursos")]
    public class Materia_x_Curso : EntityBase
    {
        public int ID { get; set; }

        #region FOREIGN KEYS
        public int? MATERIA_X_CURSO_CICLOS_ID { get; set; }
        public int? MATERIA_X_CURSO_CARRERAS_ID { get; set; }
        public int? MATERIA_X_CURSO_MATERIAS_ID { get; set; }
        public int? MATERIA_X_CURSO_DOCENTE_ID { get; set; }
        public int? MATERIA_X_CURSO_SEDES_ID { get; set; }

        public int? MATERIA_X_CURSO_CANTIDAD_PARCIALES { get; set; }
        #endregion

        #region Navegacion
        [ForeignKey("MATERIA_X_CURSO_CICLOS_ID"), Display(Name="Ciclo")]
        public Ciclo MATERIA_X_CURSO_CICLO { get; set; }
        [ForeignKey("MATERIA_X_CURSO_CARRERAS_ID"), Display(Name="Carrera")]
        
        public Carrera MATERIA_X_CURSO_CARRERA { get; set; }
        [ForeignKey("MATERIA_X_CURSO_MATERIAS_ID"), Display(Name="Materia")]
        
        public Materia MATERIA_X_CURSO_MATERIA { get; set; }
        [ForeignKey("MATERIA_X_CURSO_DOCENTE_ID"), Display(Name = "Docente")]
        public Persona MATERIA_X_CURSO_DOCENTE { get; set; }
        [ForeignKey("MATERIA_X_CURSO_SEDES_ID"), Display(Name="Sede")]
        public virtual Sede MATERIA_X_CURSO_SEDE { get; set; }
        public virtual ICollection<Horario_Cursada> Horarios_Cursadas { get; set; }

        public virtual string CICLO_ANIO {
            get { return this.MATERIA_X_CURSO_CICLO.CICLO_ANIO; }
        }
        #endregion

        public string MATERIA_X_CURSO_CURSO_NOMBRE { get; set; }
        public bool MATERIA_X_CURSO_ES_PROMOCIONAL { get; set; }
        [Display(Name="Fecha Parcial 1")]
        public Nullable<DateTime> MATERIA_X_CURSO_P1_FECHA { get; set; }
        [Display(Name = "Fecha Parcial 2")]
        public Nullable<DateTime> MATERIA_X_CURSO_P2_FECHA { get; set; }
        [Display(Name = "Fecha Recuperatorio 1")]
        public Nullable<DateTime> MATERIA_X_CURSO_R1_FECHA { get; set; }
        [Display(Name = "Fecha Recuperatorio 2")]
        public Nullable<DateTime> MATERIA_X_CURSO_R2_FECHA { get; set; }

        public string MATERIA_X_CURSO_TURNO { get; set; }

        public string MATERIA_X_CURSO_HORARIOS_LUNES { get; set; }
        public string MATERIA_X_CURSO_HORARIOS_MARTES { get; set; }
        public string MATERIA_X_CURSO_HORARIOS_MIERCOLES { get; set; }
        public string MATERIA_X_CURSO_HORARIOS_JUEVES { get; set; }
        public string MATERIA_X_CURSO_HORARIOS_VIERNES { get; set; }
        public string MATERIA_X_CURSO_HORARIOS_SABADO { get; set; }
        public string MATERIA_X_CURSO_HORARIOS_DOMINGO { get; set; }
        public bool MATERIA_X_CURSO_DEFINITIVO_EN_LIBRO { get; set; }
    }
}
