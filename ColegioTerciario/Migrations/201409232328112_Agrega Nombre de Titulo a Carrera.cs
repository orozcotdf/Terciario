namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaNombredeTituloaCarrera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carreras", "CARRERA_TITULO_NOMBRE", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carreras", "CARRERA_TITULO_NOMBRE");
        }
    }
}
