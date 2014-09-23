using ColegioTerciario.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.Configs
{
    public class PersonaConfig : EntityTypeConfiguration<Persona>
    {
        public PersonaConfig()
        {
            HasOptional(p => p.PERSONA_NACIMIENTO_CIUDAD)
                .WithMany(c => c.PERSONAS)
                .HasForeignKey(p => p.PERSONA_NACIMIENTO_CIUDAD_ID)
                .WillCascadeOnDelete(false);

            HasOptional(p => p.PERSONA_NACIMIENTO_PROVINCIA)
                .WithMany(c => c.PERSONAS)
                .HasForeignKey(p => p.PERSONA_NACIMIENTO_PROVINCIA_ID)
                .WillCascadeOnDelete(false);

            HasOptional(p => p.PERSONA_NACIMIENTO_PAIS)
                .WithMany(c => c.PERSONAS)
                .HasForeignKey(p => p.PERSONA_NACIMIENTO_PAIS_ID)
                .WillCascadeOnDelete(false);

            HasOptional(p => p.PERSONA_BARRIO)
                .WithMany(b => b.PERSONAS)
                .HasForeignKey(p => p.PERSONA_NACIMIENTO_BARRIO_ID)
                .WillCascadeOnDelete(false);
        }
    }
}