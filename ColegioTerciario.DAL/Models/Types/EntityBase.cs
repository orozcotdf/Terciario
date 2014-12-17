using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models.Types
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        public virtual void OnBeforeDelete() { }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
    }

    public class EntityBaseConfiguration : EntityTypeConfiguration<EntityBase>
    {
        public EntityBaseConfiguration()
        {
            Ignore(e => e.IsDeleted);
        }
    }

}