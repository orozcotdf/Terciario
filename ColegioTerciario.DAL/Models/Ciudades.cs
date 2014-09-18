using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    public class Ciudades
    {
        public Ciudades()
        {
            this.PERSONAS = new HashSet<Personas>();
        }
    
        public int ID { get; set; }
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
    
        public virtual ICollection<Personas> PERSONAS { get; set; }
    }
}
