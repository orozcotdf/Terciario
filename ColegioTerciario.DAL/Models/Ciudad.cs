using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    [Table("Ciudades")]
    public class Ciudad
    {
        public Ciudad()
        {
            this.PERSONAS = new HashSet<Persona>();
        }
    
        public int ID { get; set; }
        [Display(Name="Ciudad")]
        public string CIUDAD_NAME { get; set; }
        public string CIUDAD_NAME_ASCII { get; set; }
        public string CIUDAD_SLUG { get; set; }
        public Nullable<int> CIUDAD_GEONAME_ID { get; set; }
        public int CIUDAD_COUNTRY_ID { get; set; }
        public Nullable<decimal> CIUDAD_LATITUDE { get; set; }
        public Nullable<decimal> CIUDAD_LONGITUDE { get; set; }
        public string CIUDAD_SEARCH_NAMES { get; set; }
        public Nullable<int> CIUDAD_REGION_ID { get; set; }
        public string CIUDAD_ALTERNATE_NAMES { get; set; }
        public string CIUDAD_DISPLAY_NAME { get; set; }
        public Nullable<long> CIUDAD_POPULATION { get; set; }
        public string CIUDAD_FEATURE_CODE { get; set; }
    
        public virtual ICollection<Persona> PERSONAS { get; set; }
        public virtual ICollection<Barrio> BARRIOS { get; set; }
    }
}
