namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActasCascadeonDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID", "dbo.Actas_Examenes");
            AddForeignKey("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID", "dbo.Actas_Examenes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID", "dbo.Actas_Examenes");
            AddForeignKey("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID", "dbo.Actas_Examenes", "ID");
        }
    }
}
