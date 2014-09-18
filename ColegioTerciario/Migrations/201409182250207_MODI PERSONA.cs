namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MODIPERSONA : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Personas", name: "PERSONA_NACIMIENTO_CIUDAD_ID_ID", newName: "PERSONA_NACIMIENTO_CIUDAD_ID");
            RenameColumn(table: "dbo.Personas", name: "PERSONA_NACIMIENTO_PAIS_ID_ID", newName: "PERSONA_NACIMIENTO_PAIS_ID");
            RenameColumn(table: "dbo.Personas", name: "PERSONA_NACIMIENTO_PROVINCIA_ID_ID", newName: "PERSONA_NACIMIENTO_PROVINCIA_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_PERSONA_NACIMIENTO_CIUDAD_ID_ID", newName: "IX_PERSONA_NACIMIENTO_CIUDAD_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_PERSONA_NACIMIENTO_PAIS_ID_ID", newName: "IX_PERSONA_NACIMIENTO_PAIS_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_PERSONA_NACIMIENTO_PROVINCIA_ID_ID", newName: "IX_PERSONA_NACIMIENTO_PROVINCIA_ID");
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_NOMBRE", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_NOMBRE", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_NOMBRE", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_BARRIO", c => c.Int());
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD");
            DropColumn("dbo.Personas", "PERSONA_BARRIO_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Personas", "PERSONA_BARRIO_ID", c => c.Int());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS", c => c.String());
            DropColumn("dbo.Personas", "PERSONA_BARRIO");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_NOMBRE");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_NOMBRE");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_NOMBRE");
            RenameIndex(table: "dbo.Personas", name: "IX_PERSONA_NACIMIENTO_PROVINCIA_ID", newName: "IX_PERSONA_NACIMIENTO_PROVINCIA_ID_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_PERSONA_NACIMIENTO_PAIS_ID", newName: "IX_PERSONA_NACIMIENTO_PAIS_ID_ID");
            RenameIndex(table: "dbo.Personas", name: "IX_PERSONA_NACIMIENTO_CIUDAD_ID", newName: "IX_PERSONA_NACIMIENTO_CIUDAD_ID_ID");
            RenameColumn(table: "dbo.Personas", name: "PERSONA_NACIMIENTO_PROVINCIA_ID", newName: "PERSONA_NACIMIENTO_PROVINCIA_ID_ID");
            RenameColumn(table: "dbo.Personas", name: "PERSONA_NACIMIENTO_PAIS_ID", newName: "PERSONA_NACIMIENTO_PAIS_ID_ID");
            RenameColumn(table: "dbo.Personas", name: "PERSONA_NACIMIENTO_CIUDAD_ID", newName: "PERSONA_NACIMIENTO_CIUDAD_ID_ID");
        }
    }
}
