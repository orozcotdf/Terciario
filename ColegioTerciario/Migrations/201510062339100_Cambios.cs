namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Personas", "PERSONA_NOMBRE", c => c.String(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_APELLIDO", c => c.String(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_DOCUMENTO_NUMERO", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Personas", "PERSONA_DOCUMENTO_NUMERO", c => c.String());
            AlterColumn("dbo.Personas", "PERSONA_APELLIDO", c => c.String());
            AlterColumn("dbo.Personas", "PERSONA_NOMBRE", c => c.String());
        }
    }
}
