namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fechadepresentaciondedocumentacionparainscripcion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inscripciones", "INSCRIPCIONES_FECHA_PRESENTO_DOCUMENTACION", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inscripciones", "INSCRIPCIONES_FECHA_PRESENTO_DOCUMENTACION");
        }
    }
}
