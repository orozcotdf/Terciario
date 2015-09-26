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
    [Table("Provincias")]
    public class Provincia : EntityBase
    {
    
        public int ID { get; set; }
        public string PROVINCIA_NAME_ASCII { get; set; }
        public string PROVINCIA_SLUG { get; set; }
        [Display(Name="Provincia")]
        public string PROVINCIA_NAME { get; set; }
        public string PROVINCIA_GEONAME_CODE { get; set; }
        public int PROVINCIA_COUNTRY_ID { get; set; }
        public Nullable<int> PROVINCIA_GEONAME_ID { get; set; }
        public string PROVINCIA_ALTERNATE_NAMES { get; set; }
        public string PROVINCIA_DISPLAY_NAME { get; set; }
    
        //public virtual ICollection<Persona> PERSONAS { get; set; }
    }
}
