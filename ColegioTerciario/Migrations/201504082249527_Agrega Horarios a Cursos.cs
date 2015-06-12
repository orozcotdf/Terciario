namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaHorariosaCursos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_LUNES", c => c.String());
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_MARTES", c => c.String());
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_MIERCOLES", c => c.String());
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_JUEVES", c => c.String());
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_VIERNES", c => c.String());
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_SABADO", c => c.String());
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_DOMINGO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_DOMINGO");
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_SABADO");
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_VIERNES");
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_JUEVES");
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_MIERCOLES");
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_MARTES");
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_HORARIOS_LUNES");
        }
    }
}
