namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agregaestadoacademicoydeasistenciaacursada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursadas", "CURSADA_ESTADO_ACADEMICO", c => c.String());
            AddColumn("dbo.Cursadas", "CURSADA_ESTADO_ASISTENCIA", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursadas", "CURSADA_ESTADO_ASISTENCIA");
            DropColumn("dbo.Cursadas", "CURSADA_ESTADO_ACADEMICO");
        }
    }
}
