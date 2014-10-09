namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoDATEenActa_Examen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_FECHA", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_FECHA", c => c.DateTime());
        }
    }
}
