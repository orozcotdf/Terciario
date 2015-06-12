namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaEstadoDefinitivoaMateria_X_Curso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CANTIDAD_PARCIALES", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CANTIDAD_PARCIALES");
        }
    }
}
