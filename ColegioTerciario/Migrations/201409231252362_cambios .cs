namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Barrios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BARRIO_NOMBRE = c.String(),
                        BARRIO_CIUDAD_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ciudades", t => t.BARRIO_CIUDAD_ID, cascadeDelete: true)
                .Index(t => t.BARRIO_CIUDAD_ID);
            
            AddColumn("dbo.Personas", "PERSONA_NOMBRE_PARA_MOSTRAR", c => c.String());
            AddColumn("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID", c => c.Int());
            CreateIndex("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID");
            AddForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID", "dbo.Barrios", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID", "dbo.Barrios");
            DropForeignKey("dbo.Barrios", "BARRIO_CIUDAD_ID", "dbo.Ciudades");
            DropIndex("dbo.Barrios", new[] { "BARRIO_CIUDAD_ID" });
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_BARRIO_ID" });
            DropColumn("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID");
            DropColumn("dbo.Personas", "PERSONA_NOMBRE_PARA_MOSTRAR");
            DropTable("dbo.Barrios");
        }
    }
}
