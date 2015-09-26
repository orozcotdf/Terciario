using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioTerciario.Models.Types;
using Newtonsoft.Json;

namespace ColegioTerciario.DAL.Models
{
    [Table("Materias")]
    public class Materia : EntityBase
    {
        public int ID { get; set; }
        public string MATERIA_CODIGO { get; set; }
        public int? MATERIA_CARRERAS_ID { get; set; }
        public string MATERIA_ANIO { get; set; }
        public string MATERIA_APROBADAS_PARA_CURSAR { get; set; }
        public string MATERIA_APROBADAS_PARA_RENDIR { get; set; }
        public string MATERIA_CURSADAS_PARA_CURSAR { get; set; }
        public string MATERIA_CURSADAS_PARA_RENDIR { get; set; }
        public int MATERIA_DURACION { get; set; } // 0 - Anual, 1 - Primer Cuatrimestre, 2 - Segundo Cuatrimestre
        public int MATERIA_HORAS_CATEDRA { get; set; }
        [Required]
        public string MATERIA_NOMBRE { get; set; }
        public string MATERIA_NOMBRE_CORTO { get; set; }

        [ForeignKey("MATERIA_CARRERAS_ID")]
        [JsonIgnore]
        public Carrera MATERIA_CARRERA { get; set; }

        public virtual ICollection<Acta_Examen> Actas_Examenes { get; set; }

        public const int Anual = 0;
        public const int PrimerCuatrimestre = 1;
        public const int SegundoCuatrimestre = 2;

    }
}
