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
    [Table("Paises")]
    public class Pais : EntityBase
    {
        public Pais()
        {
            this.PERSONAS = new HashSet<Persona>();
        }
    
        public int ID { get; set; }
        [Display(Name="Pais")]
        public string PAIS_NAME { get; set; }
        public string PAIS_NAME_ASCII { get; set; }
        public string PAIS_SLUG { get; set; }
        public string PAIS_CODE2 { get; set; }
        public string PAIS_CODE3 { get; set; }
        public string PAIS_CONTINENT { get; set; }
        public string PAIS_TLD { get; set; }
        public Nullable<int> PAIS_GEONAME_ID { get; set; }
        public string PAIS_ALTERNATE_NAMES { get; set; }
        public string PAIS_PHONE { get; set; }
    
        public virtual ICollection<Persona> PERSONAS { get; set; }
    }
}
