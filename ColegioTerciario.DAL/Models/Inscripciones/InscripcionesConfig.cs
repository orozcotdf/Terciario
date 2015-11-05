using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.Types;

namespace ColegioTerciario.DAL.Models.Inscripciones
{
    public class InscripcionesConfig : EntityBase
    {
        public int ID { get; set; }
        public string CONFIG_NOMBRE { get; set; }
        public string CONFIG_VALOR { get; set; }
        public DateTime? CONFIG_VALIDA_DESDE { get; set; }
        public DateTime? CONFIG_VALIDA_HASTA { get; set; }
    }
}
