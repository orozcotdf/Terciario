namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MODIPERSON : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Personas", name: "CIUDADES_ID", newName: "PERSONA_NACIMIENTO_CIUDAD_ID_ID");
            RenameColumn(table: "dbo.Personas", name: "PAISES_ID", newName: "PERSONA_NACIMIENTO_PAIS_ID_ID");
            RenameColumn(table: "dbo.Personas", name: "PROVINCIAS_ID", newName: "PERSONA_NACIMIENTO_PROVINCIA_ID_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_CIUDADES_ID", newName: "IX_PERSONA_NACIMIENTO_CIUDAD_ID_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_PAISES_ID", newName: "IX_PERSONA_NACIMIENTO_PAIS_ID_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_PROVINCIAS_ID", newName: "IX_PERSONA_NACIMIENTO_PROVINCIA_ID_ID");
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD", c => c.String());
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_LOCALIDAD_ID");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_LOCALIDAD");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_LOCALIDAD", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_LOCALIDAD_ID", c => c.Int());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID", c => c.Int());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID", c => c.Int());
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD");
            RenameIndex(table: "dbo.Personas", name: "IX_PERSONA_NACIMIENTO_PROVINCIA_ID_ID", newName: "IX_PROVINCIAS_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_PERSONA_NACIMIENTO_PAIS_ID_ID", newName: "IX_PAISES_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_PERSONA_NACIMIENTO_CIUDAD_ID_ID", newName: "IX_CIUDADES_ID");
            RenameColumn(table: "dbo.Personas", name: "PERSONA_NACIMIENTO_PROVINCIA_ID_ID", newName: "PROVINCIAS_ID");
            RenameColumn(table: "dbo.Personas", name: "PERSONA_NACIMIENTO_PAIS_ID_ID", newName: "PAISES_ID");
            RenameColumn(table: "dbo.Personas", name: "PERSONA_NACIMIENTO_CIUDAD_ID_ID", newName: "CIUDADES_ID");
        }
    }
}
