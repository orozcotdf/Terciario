namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegularidaddeParciales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursadas", "CURSADA_P1_REGULAR", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cursadas", "CURSADA_P2_REGULAR", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursadas", "CURSADA_P2_REGULAR");
            DropColumn("dbo.Cursadas", "CURSADA_P1_REGULAR");
        }
    }
}
