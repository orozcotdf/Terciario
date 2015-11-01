using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.Types;
using Newtonsoft.Json;

namespace ColegioTerciario.DAL.Models.Inscripciones
{
    public class InscripcionesCarrera : EntityBase
    {
        public int ID { get; set; }
        [Required]
        public string CARRERA_NOMBRE { get; set; }
        public int CARRERA_ORIGINAL_ID { get; set; }
        public DateTime? CARRERA_HABILITADA_DESDE { get; set; }
        public DateTime? CARRERA_HABILITADA_HASTA { get; set; }
        [JsonIgnore]
        public virtual ICollection<Acta_Examen> ACTAS_EXAMENES { get; set; }
        [JsonIgnore]
        public virtual ICollection<Materia> MATERIAS { get; set; }
    }
}
