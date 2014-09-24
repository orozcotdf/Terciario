using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTerciario.DAL.Models
{
    public class Sede
    {
        public int ID { get; set; }
        [Required]
        public string SEDE_NOMBRE { get; set; }
        public string SEDE_DIRECCION { get; set; }
        public string SEDE_TELEFONO { get; set; }

        public virtual ICollection<Materia_x_Curso> MATERIAS_X_CURSOS { get; set; }
    }
}
