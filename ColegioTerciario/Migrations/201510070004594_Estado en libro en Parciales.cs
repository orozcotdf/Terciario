namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstadoenlibroenParciales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_DEFINITIVO_EN_LIBRO", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_DEFINITIVO_EN_LIBRO");
        }
    }
}
