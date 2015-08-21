namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaEstadoaTurnosdeExamenes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Turnos_Examenes", "TURNO_EXAMEN_ESTADO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Turnos_Examenes", "TURNO_EXAMEN_ESTADO");
        }
    }
}
