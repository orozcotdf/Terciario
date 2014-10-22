namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PERSONA_IDenUSER : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "USER_PERSONA_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "USER_PERSONA_ID");
            AddForeignKey("dbo.AspNetUsers", "USER_PERSONA_ID", "dbo.Personas", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "USER_PERSONA_ID", "dbo.Personas");
            DropIndex("dbo.AspNetUsers", new[] { "USER_PERSONA_ID" });
            DropColumn("dbo.AspNetUsers", "USER_PERSONA_ID");
        }
    }
}
