namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BarriosCascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Barrios", "BARRIO_CIUDAD_ID", "dbo.Ciudades");
            AddForeignKey("dbo.Barrios", "BARRIO_CIUDAD_ID", "dbo.Ciudades", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Barrios", "BARRIO_CIUDAD_ID", "dbo.Ciudades");
            AddForeignKey("dbo.Barrios", "BARRIO_CIUDAD_ID", "dbo.Ciudades", "ID", cascadeDelete: true);
        }
    }
}
