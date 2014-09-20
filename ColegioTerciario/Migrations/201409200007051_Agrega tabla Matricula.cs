namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregatablaMatricula : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matriculas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MATRICULA_NOMBRE = c.String(nullable: false),
                        MATRICULA_FECHA = c.DateTime(),
                        MATRICULA_PERSONA_ID = c.Int(nullable: false),
                        MATRICULA_CARRERA_ID = c.Int(nullable: false),
                        MATRICULA_CICLO_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carreras", t => t.MATRICULA_CARRERA_ID, cascadeDelete: true)
                .ForeignKey("dbo.Ciclos", t => t.MATRICULA_CICLO_ID, cascadeDelete: true)
                .ForeignKey("dbo.Personas", t => t.MATRICULA_PERSONA_ID, cascadeDelete: true)
                .Index(t => t.MATRICULA_PERSONA_ID)
                .Index(t => t.MATRICULA_CARRERA_ID)
                .Index(t => t.MATRICULA_CICLO_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matriculas", "MATRICULA_PERSONA_ID", "dbo.Personas");
            DropForeignKey("dbo.Matriculas", "MATRICULA_CICLO_ID", "dbo.Ciclos");
            DropForeignKey("dbo.Matriculas", "MATRICULA_CARRERA_ID", "dbo.Carreras");
            DropIndex("dbo.Matriculas", new[] { "MATRICULA_CICLO_ID" });
            DropIndex("dbo.Matriculas", new[] { "MATRICULA_CARRERA_ID" });
            DropIndex("dbo.Matriculas", new[] { "MATRICULA_PERSONA_ID" });
            DropTable("dbo.Matriculas");
        }
    }
}
