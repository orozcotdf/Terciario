using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ColegioTerciario.Models.Types;

namespace ColegioTerciario.DAL.Models
{
    [Table("Carreras")]
    public class Carrera : EntityBase
    {
        public int ID { get; set; }
        [Required]
        public string CARRERA_CODIGO { get; set; }
        public Nullable<DateTime> CARRERA_FECHA_DESDE { get; set; }
        public Nullable<DateTime> CARRERA_FECHA_HASTA { get; set; }
        [Required]
        public string CARRERA_NOMBRE { get; set; }
        public string CARRERA_NOMBRE_CORTO { get; set; }
        public string CARRERA_RESOLUCION_PLAN { get; set; }
        public string CARRERA_TITULO_NOMBRE { get; set; }
        public virtual ICollection<Acta_Examen> ACTAS_EXAMENES { get; set; }
        public virtual ICollection<Materia> MATERIAS { get; set; }
        public override string ToString()
        {
            return this.CARRERA_NOMBRE;
        }
    }
}
