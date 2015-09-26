namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaEquivalencias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equivalencias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EQUIVALENCIA_FECHA = c.DateTime(),
                        EQUIVALENCIA_NRO_DISPOSICION = c.String(),
                        EQUIVALENCIA_ALUMNO_ID = c.Int(),
                        EQUIVALENCIA_CARRERA_ID = c.Int(),
                        DeletedAt = c.DateTimeOffset(precision: 7),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Personas", t => t.EQUIVALENCIA_ALUMNO_ID)
                .ForeignKey("dbo.Carreras", t => t.EQUIVALENCIA_CARRERA_ID)
                .Index(t => t.EQUIVALENCIA_ALUMNO_ID)
                .Index(t => t.EQUIVALENCIA_CARRERA_ID);
            
            CreateTable(
                "dbo.Equivalencias_Detalles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EQUIVALENCIA_DETALLE_TIPO = c.String(),
                        EQUIVALENCIA_COMENTARIO = c.String(),
                        EQUIVALENCIA_ID = c.Int(),
                        EQUIVALENCIA_DETALLE_MATERIA_ID = c.Int(),
                        EQUIVALENCIA_DETALLE_PROFESOR_ID = c.Int(),
                        DeletedAt = c.DateTimeOffset(precision: 7),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Equivalencias", t => t.EQUIVALENCIA_ID)
                .ForeignKey("dbo.Materias", t => t.EQUIVALENCIA_DETALLE_MATERIA_ID)
                .ForeignKey("dbo.Personas", t => t.EQUIVALENCIA_DETALLE_PROFESOR_ID)
                .Index(t => t.EQUIVALENCIA_ID)
                .Index(t => t.EQUIVALENCIA_DETALLE_MATERIA_ID)
                .Index(t => t.EQUIVALENCIA_DETALLE_PROFESOR_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equivalencias_Detalles", "EQUIVALENCIA_DETALLE_PROFESOR_ID", "dbo.Personas");
            DropForeignKey("dbo.Equivalencias_Detalles", "EQUIVALENCIA_DETALLE_MATERIA_ID", "dbo.Materias");
            DropForeignKey("dbo.Equivalencias_Detalles", "EQUIVALENCIA_ID", "dbo.Equivalencias");
            DropForeignKey("dbo.Equivalencias", "EQUIVALENCIA_CARRERA_ID", "dbo.Carreras");
            DropForeignKey("dbo.Equivalencias", "EQUIVALENCIA_ALUMNO_ID", "dbo.Personas");
            DropIndex("dbo.Equivalencias_Detalles", new[] { "EQUIVALENCIA_DETALLE_PROFESOR_ID" });
            DropIndex("dbo.Equivalencias_Detalles", new[] { "EQUIVALENCIA_DETALLE_MATERIA_ID" });
            DropIndex("dbo.Equivalencias_Detalles", new[] { "EQUIVALENCIA_ID" });
            DropIndex("dbo.Equivalencias", new[] { "EQUIVALENCIA_CARRERA_ID" });
            DropIndex("dbo.Equivalencias", new[] { "EQUIVALENCIA_ALUMNO_ID" });
            DropTable("dbo.Equivalencias_Detalles");
            DropTable("dbo.Equivalencias");
        }
    }
}
