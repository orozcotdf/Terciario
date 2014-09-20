using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ColegioTerciario.DAL.Models
{
    [Table("Carreras")]
    public class Carrera
    {
        public int ID { get; set; }
        public string CARRERA_CODIGO { get; set; }
        public Nullable<DateTime> CARRERA_FECHA_DESDE { get; set; }
        public Nullable<DateTime> CARRERA_FECHA_HASTA { get; set; }
        [Required]
        public string CARRERA_NOMBRE { get; set; }
        public string CARRERA_NOMBRE_CORTO { get; set; }
        public string CARRERA_RESOLUCION_PLAN { get; set; }
    }
}
