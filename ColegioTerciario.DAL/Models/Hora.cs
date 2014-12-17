using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.Types;

namespace ColegioTerciario.DAL.Models
{
    [Table("Horas")]
    public class Hora : EntityBase
    {
        public int ID { get; set; }
        public Nullable<DateTime> HORA_INICIO { get; set; }
        public Nullable<DateTime> HORA_FIN { get; set; }
        public string HORA_NOMBRE { get; set; }
        public string HORA_TURNO { get; set; }

        public virtual ICollection<Horario_Cursada> Horarios_Cursadas { get; set; }
    }
}
