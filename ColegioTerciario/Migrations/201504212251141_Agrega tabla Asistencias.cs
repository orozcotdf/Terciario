namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregatablaAsistencias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asistencias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MATERIA_X_CURSOS_ID = c.Int(),
                        ASISTENCIA_FECHA = c.DateTime(nullable: false),
                        ASISTENCIA_ALUMNO_ID = c.Int(),
                        ASISTENCIA_PRESENTE = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTimeOffset(precision: 7),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Personas", t => t.ASISTENCIA_ALUMNO_ID)
                .ForeignKey("dbo.Materias_X_Cursos", t => t.MATERIA_X_CURSOS_ID)
                .Index(t => t.MATERIA_X_CURSOS_ID)
                .Index(t => t.ASISTENCIA_ALUMNO_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Asistencias", "MATERIA_X_CURSOS_ID", "dbo.Materias_X_Cursos");
            DropForeignKey("dbo.Asistencias", "ASISTENCIA_ALUMNO_ID", "dbo.Personas");
            DropIndex("dbo.Asistencias", new[] { "ASISTENCIA_ALUMNO_ID" });
            DropIndex("dbo.Asistencias", new[] { "MATERIA_X_CURSOS_ID" });
            DropTable("dbo.Asistencias");
        }
    }
}
