namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoDateenTurno_Examen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Turnos_Examenes", "TURNO_EXAMEN_FECHA_INICIO", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Turnos_Examenes", "TURNO_EXAMEN_FECHA_FIN", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Turnos_Examenes", "TURNO_EXAMEN_FECHA_FIN", c => c.DateTime());
            AlterColumn("dbo.Turnos_Examenes", "TURNO_EXAMEN_FECHA_INICIO", c => c.DateTime());
        }
    }
}
