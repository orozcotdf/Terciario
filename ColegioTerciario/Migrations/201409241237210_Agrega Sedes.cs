namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaSedes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sedes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SEDE_NOMBRE = c.String(nullable: false),
                        SEDE_DIRECCION = c.String(),
                        SEDE_TELEFONO = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_SEDES_ID", c => c.Int());
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CURSO_NOMBRE", c => c.String());
            CreateIndex("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_SEDES_ID");
            AddForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_SEDES_ID", "dbo.Sedes", "ID");
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CURSO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CURSO", c => c.String());
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_SEDES_ID", "dbo.Sedes");
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_SEDES_ID" });
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CURSO_NOMBRE");
            DropColumn("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_SEDES_ID");
            DropTable("dbo.Sedes");
        }
    }
}
