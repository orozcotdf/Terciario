namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PresentoDocumentacionparaInscripcion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inscripciones", "INSCRIPCIONES_PRESENTO_DOCUMENTACION", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inscripciones", "INSCRIPCIONES_PRESENTO_DOCUMENTACION");
        }
    }
}
