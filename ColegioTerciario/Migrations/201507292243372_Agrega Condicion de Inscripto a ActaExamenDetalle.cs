namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaCondiciondeInscriptoaActaExamenDetalle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_CONDICION_INSCRIPTO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_CONDICION_INSCRIPTO");
        }
    }
}
