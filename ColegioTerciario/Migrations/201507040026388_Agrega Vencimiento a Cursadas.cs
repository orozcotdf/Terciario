namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaVencimientoaCursadas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursadas", "CURSADA_FECHA_VENCIMIENTO", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursadas", "CURSADA_FECHA_VENCIMIENTO");
        }
    }
}
