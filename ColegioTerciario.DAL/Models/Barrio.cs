using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    [Table("Barrios")]
    public class Barrio
    {
        public int ID { get; set; }
        [Display(Name="Barrio")]
        public string BARRIO_NOMBRE { get; set; }
        public int? BARRIO_CIUDAD_ID { get; set; }


        [ForeignKey("BARRIO_CIUDAD_ID"),InverseProperty("BARRIOS")]
        public virtual Ciudad BARRIO_CIUDAD { get; set; }
        public virtual ICollection<Persona> PERSONAS { get; set; }
    }
}
