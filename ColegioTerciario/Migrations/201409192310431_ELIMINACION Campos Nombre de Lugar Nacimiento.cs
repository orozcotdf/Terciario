namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ELIMINACIONCamposNombredeLugarNacimiento : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_NOMBRE");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_NOMBRE");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_NOMBRE");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_NOMBRE", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_NOMBRE", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_NOMBRE", c => c.String());
        }
    }
}
