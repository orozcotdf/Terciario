namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaTurnoaCursos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_TURNO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_TURNO");
        }
    }
}
