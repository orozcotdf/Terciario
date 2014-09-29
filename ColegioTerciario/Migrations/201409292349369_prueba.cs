namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prueba : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carreras", "CARRERA_CODIGO", c => c.String(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_NOMBRE", c => c.String());
            AlterColumn("dbo.Personas", "PERSONA_APELLIDO", c => c.String());
            AlterColumn("dbo.Personas", "PERSONA_DOCUMENTO_NUMERO", c => c.String());
            AlterColumn("dbo.Personas", "PERSONA_EMAIL", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Personas", "PERSONA_EMAIL", c => c.String(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_DOCUMENTO_NUMERO", c => c.String(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_APELLIDO", c => c.String(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_NOMBRE", c => c.String(nullable: false));
            AlterColumn("dbo.Carreras", "CARRERA_CODIGO", c => c.String());
        }
    }
}
