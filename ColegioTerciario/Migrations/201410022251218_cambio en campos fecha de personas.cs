namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioencamposfechadepersonas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Personas", "PERSONA_NACIMIENTO_FECHA", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Personas", "PERSONA_FECHA_ALTA", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Personas", "PERSONA_FECHA_BAJA", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Personas", "PERSONA_FECHA_BAJA", c => c.DateTime());
            AlterColumn("dbo.Personas", "PERSONA_FECHA_ALTA", c => c.DateTime());
            AlterColumn("dbo.Personas", "PERSONA_NACIMIENTO_FECHA", c => c.DateTime());
        }
    }
}
