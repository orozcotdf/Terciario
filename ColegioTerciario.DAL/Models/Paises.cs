using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    public class Paises
    {
        public Paises()
        {
            this.PERSONAS = new HashSet<Personas>();
        }
    
        public int ID { get; set; }
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
    
        public virtual ICollection<Personas> PERSONAS { get; set; }
    }
}
