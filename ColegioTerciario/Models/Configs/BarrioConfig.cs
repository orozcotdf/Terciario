using ColegioTerciario.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.Configs
{
    public class BarrioConfig : EntityTypeConfiguration<Barrio>
    {
        public BarrioConfig()
        {
            HasOptional(b => b.BARRIO_CIUDAD)
                .WithMany(c => c.BARRIOS)
                .HasForeignKey(b => b.BARRIO_CIUDAD_ID)
                .WillCascadeOnDelete(true);
        }
    }
}