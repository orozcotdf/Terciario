namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaTablaMateria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Materias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MATERIA_CODIGO = c.String(),
                        MATERIA_CARRERA_ID = c.Int(nullable: false),
                        MATERIA_ANIO = c.String(),
                        MATERIA_APROBADAS_PARA_CURSAR = c.String(),
                        MATERIA_APROBADAS_PARA_RENDIR = c.String(),
                        MATERIA_CURSADAS_PARA_CURSAR = c.String(),
                        MATERIA_CURSADAS_PARA_RENDIR = c.String(),
                        MATERIA_DURACION = c.Int(nullable: false),
                        MATERIA_HORAS_CATEDRA = c.Int(nullable: false),
                        MATERIA_NOMBRE = c.String(nullable: false),
                        MATERIA_NOMBRE_CORTO = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carreras", t => t.MATERIA_CARRERA_ID, cascadeDelete: true)
                .Index(t => t.MATERIA_CARRERA_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materias", "MATERIA_CARRERA_ID", "dbo.Carreras");
            DropIndex("dbo.Materias", new[] { "MATERIA_CARRERA_ID" });
            DropTable("dbo.Materias");
        }
    }
}
