namespace ColegioTerciario.Models.ViewModels.Api
{
    public class CursosPorDocenteViewModel
    {
        public string CICLO_ANIO { get; set; }
        public int? MATERIA_X_CURSO_SEDES_ID { get; set; }
        public string CARRERA_NOMBRE { get; set; }
        public string MATERIA_X_CURSO_CURSO_NOMBRE { get; set; }
        public string SEDE_NOMBRE { get; set; }

        public string MATERIA_NOMBRE { get; set; }

        public int ID { get; set; }

        public int? CANTIDAD_PARCIALES { get; set; }
    }
}