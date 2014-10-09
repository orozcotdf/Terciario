namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Actaexamencamposestadisticosnoobligatorios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_INSCRIPTOS", c => c.Int());
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_APROBADOS", c => c.Int());
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_REPROBADOS", c => c.Int());
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_AUSENTES", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_AUSENTES", c => c.Int(nullable: false));
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_REPROBADOS", c => c.Int(nullable: false));
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_APROBADOS", c => c.Int(nullable: false));
            AlterColumn("dbo.Actas_Examenes", "ACTA_EXAMEN_INSCRIPTOS", c => c.Int(nullable: false));
        }
    }
}
