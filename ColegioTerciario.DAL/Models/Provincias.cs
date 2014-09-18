using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    public class Provincias
    {
        public Provincias()
        {
            this.PERSONAS = new HashSet<Personas>();
        }
    
        public int ID { get; set; }
        public string PROVINCIA_NAME_ASCII { get; set; }
        public string PROVINCIA_SLUG { get; set; }
        public string PROVINCIA_NAME { get; set; }
        public string PROVINCIA_GEONAME_CODE { get; set; }
        public int PROVINCIA_COUNTRY_ID { get; set; }
        public Nullable<int> PROVINCIA_GEONAME_ID { get; set; }
        public string PROVINCIA_ALTERNATE_NAMES { get; set; }
        public string PROVINCIA_DISPLAY_NAME { get; set; }
    
        public virtual ICollection<Personas> PERSONAS { get; set; }
    }
}
