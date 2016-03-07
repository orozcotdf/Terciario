namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregacomentariosaActaExamen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_COMENTARIOS", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_COMENTARIOS");
        }
    }
}
