namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaBarrioaPersona : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID", c => c.Int());
            CreateIndex("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID");
            AddForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID", "dbo.Barrios", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID", "dbo.Barrios");
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_BARRIO_ID" });
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID");
        }
    }
}
