namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InscripcioneEnlistadeespera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inscripciones", "INSCRIPCIONES_EN_LISTA_DE_ESPERA", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inscripciones", "INSCRIPCIONES_EN_LISTA_DE_ESPERA");
        }
    }
}
