namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambiosenciclos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ciclos", "CICLO_MATRICULA_SEMESTRE1_INICIO", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Ciclos", "CICLO_MATRICULA_SEMESTRE1_FIN", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Ciclos", "CICLO_MATRICULA_SEMESTRE2_INICIO", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Ciclos", "CICLO_MATRICULA_SEMESTRE2_FIN", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Ciclos", "CICLO_INICIO", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Ciclos", "CICLO_FIN", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Ciclos", "CICLO_SEMESTRE_1_INICIO", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Ciclos", "CICLO_SEMESTRE_1_FIN", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Ciclos", "CICLO_SEMESTRE_2_INICIO", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Ciclos", "CICLO_SEMESTRE_2_FIN", c => c.DateTime(storeType: "date"));
            DropColumn("dbo.Ciclos", "CICLO_MATRICULA_INICIO");
            DropColumn("dbo.Ciclos", "CICLO_MATRICULA_FIN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ciclos", "CICLO_MATRICULA_FIN", c => c.DateTime());
            AddColumn("dbo.Ciclos", "CICLO_MATRICULA_INICIO", c => c.DateTime());
            AlterColumn("dbo.Ciclos", "CICLO_SEMESTRE_2_FIN", c => c.DateTime());
            AlterColumn("dbo.Ciclos", "CICLO_SEMESTRE_2_INICIO", c => c.DateTime());
            AlterColumn("dbo.Ciclos", "CICLO_SEMESTRE_1_FIN", c => c.DateTime());
            AlterColumn("dbo.Ciclos", "CICLO_SEMESTRE_1_INICIO", c => c.DateTime());
            AlterColumn("dbo.Ciclos", "CICLO_FIN", c => c.DateTime());
            AlterColumn("dbo.Ciclos", "CICLO_INICIO", c => c.DateTime());
            DropColumn("dbo.Ciclos", "CICLO_MATRICULA_SEMESTRE2_FIN");
            DropColumn("dbo.Ciclos", "CICLO_MATRICULA_SEMESTRE2_INICIO");
            DropColumn("dbo.Ciclos", "CICLO_MATRICULA_SEMESTRE1_FIN");
            DropColumn("dbo.Ciclos", "CICLO_MATRICULA_SEMESTRE1_INICIO");
        }
    }
}
