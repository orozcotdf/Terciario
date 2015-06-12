namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaEstadoDefinitivoaCursada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursadas", "CURSADA_ESTADO_DEFINITIVO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursadas", "CURSADA_ESTADO_DEFINITIVO");
        }
    }
}
