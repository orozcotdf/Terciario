namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MODIPersonasUbicacionNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_ID", "dbo.Ciudades");
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID", "dbo.Paises");
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID", "dbo.Provincias");
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_PAIS_ID" });
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_PROVINCIA_ID" });
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_CIUDAD_ID" });
            AlterColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID", c => c.Int());
            AlterColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID", c => c.Int());
            AlterColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_ID", c => c.Int());
            CreateIndex("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID");
            CreateIndex("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID");
            CreateIndex("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_ID");
            AddForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_ID", "dbo.Ciudades", "ID");
            AddForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID", "dbo.Paises", "ID");
            AddForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID", "dbo.Provincias", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID", "dbo.Provincias");
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID", "dbo.Paises");
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_ID", "dbo.Ciudades");
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_CIUDAD_ID" });
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_PROVINCIA_ID" });
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_PAIS_ID" });
            AlterColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_ID");
            CreateIndex("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID");
            CreateIndex("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID");
            AddForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID", "dbo.Provincias", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID", "dbo.Paises", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_ID", "dbo.Ciudades", "ID", cascadeDelete: true);
        }
    }
}
