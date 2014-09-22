using ColegioTerciario.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.Configs
{
    public class ActaExamenConfig : EntityTypeConfiguration<Acta_Examen>
    {
        public ActaExamenConfig()
        {
            HasOptional(a => a.ACTA_EXAMEN_TURNO_EXAMEN)
            .WithMany(t => t.ACTAS_EXAMENES)
            .HasForeignKey(a => a.ACTA_EXAMEN_TURNOS_EXAMENES_ID).WillCascadeOnDelete(false);

            HasOptional(a => a.ACTA_EXAMEN_CARRERA)
            .WithMany(t => t.ACTAS_EXAMENES)
            .HasForeignKey(a => a.ACTA_EXAMEN_CARRERAS_ID).WillCascadeOnDelete(false);

            HasOptional(a => a.ACTA_EXAMEN_MATERIA)
            .WithMany(m => m.Actas_Examenes)
            .HasForeignKey(a => a.ACTA_EXAMEN_MATERIAS_ID).WillCascadeOnDelete(false);

            // PRESIDENTE_ID
            HasOptional(a => a.ACTA_EXAMEN_PRESIDENTE)
            .WithMany(m => m.ACTAS_PRECIDIDAS)
            .HasForeignKey(a => a.ACTA_EXAMEN_PRESIDENTE_ID).WillCascadeOnDelete(false);

            // PRESIDENTE_ID
            HasOptional(a => a.ACTA_EXAMEN_VOCAL1)
            .WithMany(m => m.ACTAS_VOCAL1)
            .HasForeignKey(a => a.ACTA_EXAMEN_VOCAL1_ID).WillCascadeOnDelete(false);

            // PRESIDENTE_ID
            HasOptional(a => a.ACTA_EXAMEN_VOCAL2)
            .WithMany(m => m.ACTAS_VOCAL2)
            .HasForeignKey(a => a.ACTA_EXAMEN_VOCAL2_ID).WillCascadeOnDelete(false);
        }
    }
}