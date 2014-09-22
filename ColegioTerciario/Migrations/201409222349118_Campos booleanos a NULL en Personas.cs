namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposbooleanosaNULLenPersonas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Personas", "PERSONA_ES_ALUMNO", c => c.Boolean());
            AlterColumn("dbo.Personas", "PERSONA_ES_DOCENTE", c => c.Boolean());
            AlterColumn("dbo.Personas", "PERSONA_ES_NODOCENTE", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Personas", "PERSONA_ES_NODOCENTE", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_ES_DOCENTE", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Personas", "PERSONA_ES_ALUMNO", c => c.Boolean(nullable: false));
        }
    }
}
