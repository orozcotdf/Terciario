using ColegioTerciario.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.Configs
{
    public class ActaExamenDetalleConfig : EntityTypeConfiguration<Acta_Examen_Detalle>
    {
        public ActaExamenDetalleConfig()
        {
            HasOptional(a => a.ACTA_EXAMEN_DETALLE_ALUMNO)
            .WithMany(p => p.ACTAS_EXAMENES_DETALLES)
            .HasForeignKey(a => a.ACTA_EXAMEN_DETALLE_ALUMNOS_ID).WillCascadeOnDelete(false);

            HasOptional(a => a.ACTA_EXAMEN_DETALLE_ACTA_EXAMEN)
            .WithMany(p => p.ACTAS_EXAMENES_DETALLES)
            .HasForeignKey(a => a.ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID).WillCascadeOnDelete(false);
        }
    }
}