namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inscripciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inscripciones",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        INSCRIPCIONES_CODIGO = c.String(),
                        INSCRIPCIONES_USUARIO = c.String(),
                        INSCRIPCIONES_CLAVE = c.String(),
                        INSCRIPCIONES_NOMBRE = c.String(nullable: false),
                        INSCRIPCIONES_APELLIDO = c.String(nullable: false),
                        INSCRIPCIONES_NOMBRE_PARA_MOSTRAR = c.String(),
                        INSCRIPCIONES_DOCUMENTO_TIPO = c.String(),
                        INSCRIPCIONES_DOCUMENTO_NUMERO = c.String(nullable: false),
                        INSCRIPCIONES_NACIMIENTO_FECHA = c.DateTime(storeType: "date"),
                        INSCRIPCIONES_EMAIL = c.String(),
                        INSCRIPCIONES_DOMICILIO = c.String(),
                        INSCRIPCIONES_TELEFONO = c.String(),
                        INSCRIPCIONES_SEXO = c.String(),
                        INSCRIPCIONES_FECHA_ALTA = c.DateTime(storeType: "date"),
                        INSCRIPCIONES_FECHA_BAJA = c.DateTime(storeType: "date"),
                        INSCRIPCIONES_TITULO_SECUNDARIO = c.String(),
                        INSCRIPCIONES_OBSERVACION = c.String(),
                        INSCRIPCIONES_FOTO = c.String(),
                        INSCRIPCIONES_CUIL = c.String(),
                        INSCRIPCIONES_ES_ALUMNO = c.Boolean(),
                        INSCRIPCIONES_ES_DOCENTE = c.Boolean(),
                        INSCRIPCIONES_ES_NODOCENTE = c.Boolean(),
                        INSCRIPCIONES_NACIMIENTO_PAIS_ID = c.Int(),
                        INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID = c.Int(),
                        INSCRIPCIONES_NACIMIENTO_CIUDAD_ID = c.Int(),
                        INSCRIPCIONES_NACIMIENTO_BARRIO_ID = c.Int(),
                        INSCRIPCIONES_CARRERA_ID = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTimeOffset(precision: 7),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Barrios", t => t.INSCRIPCIONES_NACIMIENTO_BARRIO_ID)
                .ForeignKey("dbo.InscripcionesCarreras", t => t.INSCRIPCIONES_CARRERA_ID)
                .ForeignKey("dbo.Ciudades", t => t.INSCRIPCIONES_NACIMIENTO_CIUDAD_ID)
                .ForeignKey("dbo.Paises", t => t.INSCRIPCIONES_NACIMIENTO_PAIS_ID)
                .ForeignKey("dbo.Provincias", t => t.INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID)
                .Index(t => t.INSCRIPCIONES_NACIMIENTO_PAIS_ID)
                .Index(t => t.INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID)
                .Index(t => t.INSCRIPCIONES_NACIMIENTO_CIUDAD_ID)
                .Index(t => t.INSCRIPCIONES_NACIMIENTO_BARRIO_ID)
                .Index(t => t.INSCRIPCIONES_CARRERA_ID);
            
            CreateTable(
                "dbo.InscripcionesCarreras",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CARRERA_NOMBRE = c.String(nullable: false),
                        CARRERA_ORIGINAL_ID = c.Int(nullable: false),
                        CARRERA_HABILITADA_DESDE = c.DateTime(),
                        CARRERA_HABILITADA_HASTA = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTimeOffset(precision: 7),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InscripcionesConfigs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CONFIG_TITULO = c.String(),
                        CONFIG_NOTIFICACION = c.String(),
                        CONFIG_VALIDA_DESDE = c.DateTime(),
                        CONFIG_VALIDA_HASTA = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTimeOffset(precision: 7),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Actas_Examenes", "InscripcionesCarrera_ID", c => c.Int());
            AddColumn("dbo.Materias", "InscripcionesCarrera_ID", c => c.Int());
            CreateIndex("dbo.Actas_Examenes", "InscripcionesCarrera_ID");
            CreateIndex("dbo.Materias", "InscripcionesCarrera_ID");
            AddForeignKey("dbo.Actas_Examenes", "InscripcionesCarrera_ID", "dbo.InscripcionesCarreras", "ID");
            AddForeignKey("dbo.Materias", "InscripcionesCarrera_ID", "dbo.InscripcionesCarreras", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inscripciones", "INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID", "dbo.Provincias");
            DropForeignKey("dbo.Inscripciones", "INSCRIPCIONES_NACIMIENTO_PAIS_ID", "dbo.Paises");
            DropForeignKey("dbo.Inscripciones", "INSCRIPCIONES_NACIMIENTO_CIUDAD_ID", "dbo.Ciudades");
            DropForeignKey("dbo.Inscripciones", "INSCRIPCIONES_CARRERA_ID", "dbo.InscripcionesCarreras");
            DropForeignKey("dbo.Materias", "InscripcionesCarrera_ID", "dbo.InscripcionesCarreras");
            DropForeignKey("dbo.Actas_Examenes", "InscripcionesCarrera_ID", "dbo.InscripcionesCarreras");
            DropForeignKey("dbo.Inscripciones", "INSCRIPCIONES_NACIMIENTO_BARRIO_ID", "dbo.Barrios");
            DropIndex("dbo.Inscripciones", new[] { "INSCRIPCIONES_CARRERA_ID" });
            DropIndex("dbo.Inscripciones", new[] { "INSCRIPCIONES_NACIMIENTO_BARRIO_ID" });
            DropIndex("dbo.Inscripciones", new[] { "INSCRIPCIONES_NACIMIENTO_CIUDAD_ID" });
            DropIndex("dbo.Inscripciones", new[] { "INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID" });
            DropIndex("dbo.Inscripciones", new[] { "INSCRIPCIONES_NACIMIENTO_PAIS_ID" });
            DropIndex("dbo.Materias", new[] { "InscripcionesCarrera_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "InscripcionesCarrera_ID" });
            DropColumn("dbo.Materias", "InscripcionesCarrera_ID");
            DropColumn("dbo.Actas_Examenes", "InscripcionesCarrera_ID");
            DropTable("dbo.InscripcionesConfigs");
            DropTable("dbo.InscripcionesCarreras");
            DropTable("dbo.Inscripciones");
        }
    }
}
