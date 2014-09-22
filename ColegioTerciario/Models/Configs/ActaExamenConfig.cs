using ColegioTerciario.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models
{
    public class ActaExamenConfig : EntityTypeConfiguration<Acta_Examen>
    {
        public ActaExamenConfig()
        {
            HasOptional(a => a.ACTA_EXAMEN_MATERIA)
            .WithMany(m => m.Actas_Examenes)
            .HasForeignKey(a => a.ACTA_EXAMEN_MATERIAS_ID).WillCascadeOnDelete(false);
        }
    }
}