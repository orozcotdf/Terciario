namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambiosenconfiguraciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InscripcionesConfigs", "CONFIG_NOMBRE", c => c.String());
            AddColumn("dbo.InscripcionesConfigs", "CONFIG_VALOR", c => c.String());
            DropColumn("dbo.InscripcionesConfigs", "CONFIG_TITULO");
            DropColumn("dbo.InscripcionesConfigs", "CONFIG_NOTIFICACION");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InscripcionesConfigs", "CONFIG_NOTIFICACION", c => c.String());
            AddColumn("dbo.InscripcionesConfigs", "CONFIG_TITULO", c => c.String());
            DropColumn("dbo.InscripcionesConfigs", "CONFIG_VALOR");
            DropColumn("dbo.InscripcionesConfigs", "CONFIG_NOMBRE");
        }
    }
}
