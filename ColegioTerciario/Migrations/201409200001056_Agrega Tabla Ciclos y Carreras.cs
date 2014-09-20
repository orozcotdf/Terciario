namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaTablaCiclosyCarreras : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carreras",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CARRERA_CODIGO = c.String(),
                        CARRERA_FECHA_DESDE = c.DateTime(),
                        CARRERA_FECHA_HASTA = c.DateTime(),
                        CARRERA_NOMBRE = c.String(nullable: false),
                        CARRERA_NOMBRE_CORTO = c.String(),
                        CARRERA_RESOLUCION_PLAN = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ciclos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CICLO_NOMBRE = c.String(nullable: false),
                        CICLO_INICIO = c.DateTime(),
                        CICLO_FIN = c.DateTime(),
                        CICLO_ANIO = c.String(),
                        CICLO_MATRICULA_INICIO = c.DateTime(),
                        CICLO_MATRICULA_FIN = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ciclos");
            DropTable("dbo.Carreras");
        }
    }
}
