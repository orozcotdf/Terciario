namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PERSONA_CODIGO = c.String(),
                        PERSONA_USUARIO = c.String(),
                        PERSONA_CLAVE = c.String(),
                        PERSONA_NOMBRE = c.String(),
                        PERSONA_APELLIDO = c.String(),
                        PERSONA_DOCUMENTO_TIPO = c.String(),
                        PERSONA_DOCUMENTO_NUMERO = c.String(),
                        PERSONA_NACIMIENTO_FECHA = c.DateTime(),
                        PERSONA_EMAIL = c.String(),
                        PERSONA_DOMICILIO = c.String(),
                        PERSONA_TELEFONO = c.String(),
                        PERSONA_SEXO = c.String(),
                        PERSONA_FECHA_ALTA = c.DateTime(),
                        PERSONA_FECHA_BAJA = c.DateTime(),
                        PERSONA_TITULO_SECUNDARIO = c.String(),
                        PERSONA_OBSERVACION = c.String(),
                        PERSONA_FOTO = c.String(),
                        PERSONA_NACIMIENTO_PAIS_ID = c.Int(),
                        PERSONA_NACIMIENTO_PROVINCIA_ID = c.Int(),
                        PERSONA_NACIMIENTO_LOCALIDAD_ID = c.Int(),
                        PERSONA_CUIL = c.String(),
                        PERSONA_BARRIO_ID = c.Int(),
                        PERSONA_ES_ALUMNO = c.Boolean(),
                        PERSONA_ES_DOCENTE = c.Boolean(),
                        PERSONA_ES_NODOCENTE = c.Boolean(),
                        CIUDADES_ID = c.Int(),
                        PAISES_ID = c.Int(),
                        PROVINCIAS_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ciudades", t => t.CIUDADES_ID)
                .ForeignKey("dbo.Paises", t => t.PAISES_ID)
                .ForeignKey("dbo.Provincias", t => t.PROVINCIAS_ID)
                .Index(t => t.CIUDADES_ID)
                .Index(t => t.PAISES_ID)
                .Index(t => t.PROVINCIAS_ID);
            
            CreateTable(
                "dbo.Ciudades",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CIUDAD_NAME = c.String(),
                        CIUDAD_NAME_ASCII = c.String(),
                        CIUDAD_SLUG = c.String(),
                        CIUDAD_GEONAME_ID = c.Int(),
                        CIUDAD_COUNTRY_ID = c.Int(nullable: false),
                        CIUDAD_LATITUDE = c.Decimal(precision: 18, scale: 2),
                        CIUDAD_LONGITUDE = c.Decimal(precision: 18, scale: 2),
                        CIUDAD_SEARCH_NAMES = c.String(),
                        CIUDAD_REGION_ID = c.Int(),
                        CIUDAD_ALTERNATE_NAMES = c.String(),
                        CIUDAD_DISPLAY_NAME = c.String(),
                        CIUDAD_POPULATION = c.Long(),
                        CIUDAD_FEATURE_CODE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Paises",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PAIS_NAME = c.String(),
                        PAIS_NAME_ASCII = c.String(),
                        PAIS_SLUG = c.String(),
                        PAIS_CODE2 = c.String(),
                        PAIS_CODE3 = c.String(),
                        PAIS_CONTINENT = c.String(),
                        PAIS_TLD = c.String(),
                        PAIS_GEONAME_ID = c.Int(),
                        PAIS_ALTERNATE_NAMES = c.String(),
                        PAIS_PHONE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Provincias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PROVINCIA_NAME_ASCII = c.String(),
                        PROVINCIA_SLUG = c.String(),
                        PROVINCIA_NAME = c.String(),
                        PROVINCIA_GEONAME_CODE = c.String(),
                        PROVINCIA_COUNTRY_ID = c.Int(nullable: false),
                        PROVINCIA_GEONAME_ID = c.Int(),
                        PROVINCIA_ALTERNATE_NAMES = c.String(),
                        PROVINCIA_DISPLAY_NAME = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "PROVINCIAS_ID", "dbo.Provincias");
            DropForeignKey("dbo.Personas", "PAISES_ID", "dbo.Paises");
            DropForeignKey("dbo.Personas", "CIUDADES_ID", "dbo.Ciudades");
            DropIndex("dbo.Personas", new[] { "PROVINCIAS_ID" });
            DropIndex("dbo.Personas", new[] { "PAISES_ID" });
            DropIndex("dbo.Personas", new[] { "CIUDADES_ID" });
            DropTable("dbo.Provincias");
            DropTable("dbo.Paises");
            DropTable("dbo.Ciudades");
            DropTable("dbo.Personas");
        }
    }
}
