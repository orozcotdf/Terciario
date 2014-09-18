namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modilocalidades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_LOCALIDAD", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_LOCALIDAD");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA");
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_PAIS");
        }
    }
}
