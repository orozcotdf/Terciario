﻿using System.Data.Entity.Core.Objects;
using ColegioTerciario.DAL.Interfaces;
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
    [Table("Actas_Examenes")]
    [Serializable]
    public class Acta_Examen : EntityBase, IDescriptible
    {
        public Acta_Examen()
        {
            this.ACTA_EXAMEN_APROBADOS = 0;
            this.ACTA_EXAMEN_AUSENTES = 0;
            this.ACTA_EXAMEN_INSCRIPTOS = 0;
            this.ACTA_EXAMEN_REPROBADOS = 0;
        }

        public override void OnBeforeDelete()
        {
            
        }

        public int ID { get; set; }
        [Column(TypeName = "Date")]
        [Display(Name="Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ACTA_EXAMEN_FECHA { get; set; }
        public int? ACTA_EXAMEN_INSCRIPTOS { get; set; }
        public int? ACTA_EXAMEN_APROBADOS { get; set; }
        public int? ACTA_EXAMEN_REPROBADOS { get; set; }
        public int? ACTA_EXAMEN_AUSENTES { get; set; }
        [Display(Name="Libro")]
        public string ACTA_EXAMEN_LIBRO { get; set; }
        [Display(Name = "Folio")]
        public string ACTA_EXAMEN_FOLIO { get; set; }

        [MaxLength]
        [Display(Name = "Comentarios")]
        public string ACTA_EXAMEN_COMENTARIOS { get; set; }

        #region Foreign Keys
        [Display(Name = "Turno de Examen")]
        public int? ACTA_EXAMEN_TURNOS_EXAMENES_ID { get; set; }
        [Display(Name = "Carrera")]
        public int? ACTA_EXAMEN_CARRERAS_ID { get; set; }
        [Display(Name = "Materia")]
        public int? ACTA_EXAMEN_MATERIAS_ID { get; set; }
        [Display(Name = "Presidente")]
        public int? ACTA_EXAMEN_PRESIDENTE_ID { get; set; }
        [Display(Name = "Primer Vocal")]
        public int? ACTA_EXAMEN_VOCAL1_ID { get; set; }
        [Display(Name = "Segundo Vocal")]
        public int? ACTA_EXAMEN_VOCAL2_ID { get; set; }

        #endregion


        #region Navegacion
        [ForeignKey("ACTA_EXAMEN_TURNOS_EXAMENES_ID")]
        public virtual Turno_Examen ACTA_EXAMEN_TURNO_EXAMEN { get; set; }
        [ForeignKey("ACTA_EXAMEN_MATERIAS_ID")]
        public virtual Materia ACTA_EXAMEN_MATERIA { get; set; }
        [ForeignKey("ACTA_EXAMEN_CARRERAS_ID")]
        public virtual Carrera ACTA_EXAMEN_CARRERA { get; set; }
        [ForeignKey("ACTA_EXAMEN_PRESIDENTE_ID"),InverseProperty("ACTAS_PRECIDIDAS")]
        public virtual Persona ACTA_EXAMEN_PRESIDENTE { get; set; }
        [ForeignKey("ACTA_EXAMEN_VOCAL1_ID"), InverseProperty("ACTAS_VOCAL1")]
        public virtual Persona ACTA_EXAMEN_VOCAL1 { get; set; }
        [ForeignKey("ACTA_EXAMEN_VOCAL2_ID"), InverseProperty("ACTAS_VOCAL2")]
        public virtual Persona ACTA_EXAMEN_VOCAL2 { get; set; }

        public virtual ICollection<Acta_Examen_Detalle> ACTAS_EXAMENES_DETALLES { get; set; }
        #endregion



        public string Describir()
        {
            return "ACTA " + this.ACTA_EXAMEN_TURNO_EXAMEN.TURNO_EXAMEN_NOMBRE_PARA_MOSTRAR;
        }

        public bool Estado(int alumno_id)
        {
            var actas = ACTAS_EXAMENES_DETALLES
                .Where(e => e.ACTA_EXAMEN_DETALLE_ALUMNOS_ID == alumno_id && e.ACTA_EXAMEN_DETALLE_ESTADO == "Aprobado")
                .Select(e => e)
                .ToList();
            return ACTAS_EXAMENES_DETALLES.Any(e => e.ACTA_EXAMEN_DETALLE_ALUMNOS_ID == alumno_id && e.ACTA_EXAMEN_DETALLE_ESTADO == "Aprobado");
        }
    }
}
