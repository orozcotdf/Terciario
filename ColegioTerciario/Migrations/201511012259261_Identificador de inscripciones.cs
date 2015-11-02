namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Identificadordeinscripciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inscripciones", "INSCRIPCIONES_IDENTIFICADOR", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inscripciones", "INSCRIPCIONES_IDENTIFICADOR");
        }
    }
}
