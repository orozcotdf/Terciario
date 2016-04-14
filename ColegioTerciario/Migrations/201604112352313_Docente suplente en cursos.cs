namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Docentesuplenteencursos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_DOCENTE_SUPLENTE_ID", c => c.Int());
            CreateIndex("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_DOCENTE_SUPLENTE_ID");
            AddForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_DOCENTE_SUPLENTE_ID", "dbo.Personas", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_DOCENTE_SUPLENTE_ID", "dbo.Personas");
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_DOCENTE_SUPLENTE_ID" });
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_DOCENTE_SUPLENTE_ID");
        }
    }
}
