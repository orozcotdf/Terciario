namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Equivalencia_DetalleCambiaDetalle_Tipoainteger : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Equivalencias_Detalles", "EQUIVALENCIA_DETALLE_TIPO", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Equivalencias_Detalles", "EQUIVALENCIA_DETALLE_TIPO", c => c.String());
        }
    }
}
