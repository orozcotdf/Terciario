namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeraVersion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actas_Examenes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ACTA_EXAMEN_FECHA = c.DateTime(),
                        ACTA_EXAMEN_INSCRIPTOS = c.Int(nullable: false),
                        ACTA_EXAMEN_APROBADOS = c.Int(nullable: false),
                        ACTA_EXAMEN_REPROBADOS = c.Int(nullable: false),
                        ACTA_EXAMEN_AUSENTES = c.Int(nullable: false),
                        ACTA_EXAMEN_LIBRO = c.String(),
                        ACTA_EXAMEN_FOLIO = c.String(),
                        ACTA_EXAMEN_TURNOS_EXAMENES_ID = c.Int(),
                        ACTA_EXAMEN_CARRERAS_ID = c.Int(),
                        ACTA_EXAMEN_MATERIAS_ID = c.Int(),
                        ACTA_EXAMEN_PRESIDENTE_ID = c.Int(),
                        ACTA_EXAMEN_VOCAL1_ID = c.Int(),
                        ACTA_EXAMEN_VOCAL2_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carreras", t => t.ACTA_EXAMEN_CARRERAS_ID)
                .ForeignKey("dbo.Materias", t => t.ACTA_EXAMEN_MATERIAS_ID)
                .ForeignKey("dbo.Personas", t => t.ACTA_EXAMEN_PRESIDENTE_ID)
                .ForeignKey("dbo.Turnos_Examenes", t => t.ACTA_EXAMEN_TURNOS_EXAMENES_ID)
                .ForeignKey("dbo.Personas", t => t.ACTA_EXAMEN_VOCAL1_ID)
                .ForeignKey("dbo.Personas", t => t.ACTA_EXAMEN_VOCAL2_ID)
                .Index(t => t.ACTA_EXAMEN_TURNOS_EXAMENES_ID)
                .Index(t => t.ACTA_EXAMEN_CARRERAS_ID)
                .Index(t => t.ACTA_EXAMEN_MATERIAS_ID)
                .Index(t => t.ACTA_EXAMEN_PRESIDENTE_ID)
                .Index(t => t.ACTA_EXAMEN_VOCAL1_ID)
                .Index(t => t.ACTA_EXAMEN_VOCAL2_ID);
            
            CreateTable(
                "dbo.Carreras",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CARRERA_CODIGO = c.String(nullable: false),
                        CARRERA_FECHA_DESDE = c.DateTime(),
                        CARRERA_FECHA_HASTA = c.DateTime(),
                        CARRERA_NOMBRE = c.String(nullable: false),
                        CARRERA_NOMBRE_CORTO = c.String(),
                        CARRERA_RESOLUCION_PLAN = c.String(),
                        CARRERA_TITULO_NOMBRE = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Materias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MATERIA_CODIGO = c.String(),
                        MATERIA_CARRERAS_ID = c.Int(),
                        MATERIA_ANIO = c.String(),
                        MATERIA_APROBADAS_PARA_CURSAR = c.String(),
                        MATERIA_APROBADAS_PARA_RENDIR = c.String(),
                        MATERIA_CURSADAS_PARA_CURSAR = c.String(),
                        MATERIA_CURSADAS_PARA_RENDIR = c.String(),
                        MATERIA_DURACION = c.Int(nullable: false),
                        MATERIA_HORAS_CATEDRA = c.Int(nullable: false),
                        MATERIA_NOMBRE = c.String(nullable: false),
                        MATERIA_NOMBRE_CORTO = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carreras", t => t.MATERIA_CARRERAS_ID)
                .Index(t => t.MATERIA_CARRERAS_ID);
            
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
                        PERSONA_NOMBRE_PARA_MOSTRAR = c.String(),
                        PERSONA_DOCUMENTO_TIPO = c.String(),
                        PERSONA_DOCUMENTO_NUMERO = c.String(),
                        PERSONA_NACIMIENTO_FECHA = c.DateTime(storeType: "date"),
                        PERSONA_EMAIL = c.String(),
                        PERSONA_DOMICILIO = c.String(),
                        PERSONA_TELEFONO = c.String(),
                        PERSONA_SEXO = c.String(),
                        PERSONA_FECHA_ALTA = c.DateTime(storeType: "date"),
                        PERSONA_FECHA_BAJA = c.DateTime(storeType: "date"),
                        PERSONA_TITULO_SECUNDARIO = c.String(),
                        PERSONA_OBSERVACION = c.String(),
                        PERSONA_FOTO = c.String(),
                        PERSONA_CUIL = c.String(),
                        PERSONA_ES_ALUMNO = c.Boolean(),
                        PERSONA_ES_DOCENTE = c.Boolean(),
                        PERSONA_ES_NODOCENTE = c.Boolean(),
                        PERSONA_NACIMIENTO_PAIS_ID = c.Int(),
                        PERSONA_NACIMIENTO_PROVINCIA_ID = c.Int(),
                        PERSONA_NACIMIENTO_CIUDAD_ID = c.Int(),
                        PERSONA_NACIMIENTO_BARRIO_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Barrios", t => t.PERSONA_NACIMIENTO_BARRIO_ID)
                .ForeignKey("dbo.Ciudades", t => t.PERSONA_NACIMIENTO_CIUDAD_ID)
                .ForeignKey("dbo.Paises", t => t.PERSONA_NACIMIENTO_PAIS_ID)
                .ForeignKey("dbo.Provincias", t => t.PERSONA_NACIMIENTO_PROVINCIA_ID)
                .Index(t => t.PERSONA_NACIMIENTO_PAIS_ID)
                .Index(t => t.PERSONA_NACIMIENTO_PROVINCIA_ID)
                .Index(t => t.PERSONA_NACIMIENTO_CIUDAD_ID)
                .Index(t => t.PERSONA_NACIMIENTO_BARRIO_ID);
            
            CreateTable(
                "dbo.Actas_Examenes_Detalles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ACTA_EXAMEN_DETALLE_NOTA = c.String(),
                        ACTA_EXAMEN_DETALLE_ESTADO = c.String(),
                        ACTA_EXAMEN_DETALLE_ALUMNOS_ID = c.Int(),
                        ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Actas_Examenes", t => t.ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID)
                .ForeignKey("dbo.Personas", t => t.ACTA_EXAMEN_DETALLE_ALUMNOS_ID)
                .Index(t => t.ACTA_EXAMEN_DETALLE_ALUMNOS_ID)
                .Index(t => t.ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID);
            
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
            
            CreateTable(
                "dbo.Turnos_Examenes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TURNO_EXAMEN_NUMERO = c.Int(nullable: false),
                        TURNO_EXAMEN_NOMBRE = c.String(nullable: false),
                        TURNO_EXAMEN_FECHA_INICIO = c.DateTime(storeType: "date"),
                        TURNO_EXAMEN_FECHA_FIN = c.DateTime(storeType: "date"),
                        TURNO_EXAMEN_CICLOS_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ciclos", t => t.TURNO_EXAMEN_CICLOS_ID)
                .Index(t => t.TURNO_EXAMEN_CICLOS_ID);
            
            CreateTable(
                "dbo.Ciclos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CICLO_NOMBRE = c.String(nullable: false),
                        CICLO_INICIO = c.DateTime(storeType: "date"),
                        CICLO_FIN = c.DateTime(storeType: "date"),
                        CICLO_ANIO = c.String(),
                        CICLO_MATRICULA_SEMESTRE1_INICIO = c.DateTime(storeType: "date"),
                        CICLO_MATRICULA_SEMESTRE1_FIN = c.DateTime(storeType: "date"),
                        CICLO_MATRICULA_SEMESTRE2_INICIO = c.DateTime(storeType: "date"),
                        CICLO_MATRICULA_SEMESTRE2_FIN = c.DateTime(storeType: "date"),
                        CICLO_SEMESTRE_1_INICIO = c.DateTime(storeType: "date"),
                        CICLO_SEMESTRE_1_FIN = c.DateTime(storeType: "date"),
                        CICLO_SEMESTRE_2_INICIO = c.DateTime(storeType: "date"),
                        CICLO_SEMESTRE_2_FIN = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cursadas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CURSADA_NOTA_P1 = c.String(),
                        CURSADA_NOTA_P2 = c.String(),
                        CURSADA_NOTA_R1 = c.String(),
                        CURSADA_NOTA_R2 = c.String(),
                        CURSADA_ALUMNOS_ID = c.Int(),
                        CURSADA_MATERIAS_X_CURSOS_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Personas", t => t.CURSADA_ALUMNOS_ID)
                .ForeignKey("dbo.Materias_X_Cursos", t => t.CURSADA_MATERIAS_X_CURSOS_ID)
                .Index(t => t.CURSADA_ALUMNOS_ID)
                .Index(t => t.CURSADA_MATERIAS_X_CURSOS_ID);
            
            CreateTable(
                "dbo.Materias_X_Cursos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MATERIA_X_CURSO_CICLOS_ID = c.Int(),
                        MATERIA_X_CURSO_CARRERAS_ID = c.Int(),
                        MATERIA_X_CURSO_MATERIAS_ID = c.Int(),
                        MATERIA_X_CURSO_DOCENTE_ID = c.Int(),
                        MATERIA_X_CURSO_SEDES_ID = c.Int(),
                        MATERIA_X_CURSO_CURSO_NOMBRE = c.String(),
                        MATERIA_X_CURSO_ES_PROMOCIONAL = c.Boolean(nullable: false),
                        MATERIA_X_CURSO_P1_FECHA = c.DateTime(),
                        MATERIA_X_CURSO_P2_FECHA = c.DateTime(),
                        MATERIA_X_CURSO_R1_FECHA = c.DateTime(),
                        MATERIA_X_CURSO_R2_FECHA = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carreras", t => t.MATERIA_X_CURSO_CARRERAS_ID)
                .ForeignKey("dbo.Ciclos", t => t.MATERIA_X_CURSO_CICLOS_ID)
                .ForeignKey("dbo.Personas", t => t.MATERIA_X_CURSO_DOCENTE_ID)
                .ForeignKey("dbo.Materias", t => t.MATERIA_X_CURSO_MATERIAS_ID)
                .ForeignKey("dbo.Sedes", t => t.MATERIA_X_CURSO_SEDES_ID)
                .Index(t => t.MATERIA_X_CURSO_CICLOS_ID)
                .Index(t => t.MATERIA_X_CURSO_CARRERAS_ID)
                .Index(t => t.MATERIA_X_CURSO_MATERIAS_ID)
                .Index(t => t.MATERIA_X_CURSO_DOCENTE_ID)
                .Index(t => t.MATERIA_X_CURSO_SEDES_ID);
            
            CreateTable(
                "dbo.Horarios_Cursadas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HORARIO_CURSADA_DIA = c.String(),
                        HORARIO_CURSADA_MATERIAS_X_CURSOS_ID = c.Int(),
                        HORARIO_CURSADA_HORAS_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Horas", t => t.HORARIO_CURSADA_HORAS_ID)
                .ForeignKey("dbo.Materias_X_Cursos", t => t.HORARIO_CURSADA_MATERIAS_X_CURSOS_ID)
                .Index(t => t.HORARIO_CURSADA_MATERIAS_X_CURSOS_ID)
                .Index(t => t.HORARIO_CURSADA_HORAS_ID);
            
            CreateTable(
                "dbo.Horas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HORA_INICIO = c.DateTime(),
                        HORA_FIN = c.DateTime(),
                        HORA_NOMBRE = c.String(),
                        HORA_TURNO = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sedes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SEDE_NOMBRE = c.String(nullable: false),
                        SEDE_DIRECCION = c.String(),
                        SEDE_TELEFONO = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Matriculas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MATRICULA_NOMBRE = c.String(nullable: false),
                        MATRICULA_FECHA = c.DateTime(),
                        MATRICULA_PERSONAS_ID = c.Int(nullable: false),
                        MATRICULA_CARRERAS_ID = c.Int(nullable: false),
                        MATRICULA_CICLOS_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Personas", t => t.MATRICULA_PERSONAS_ID, cascadeDelete: true)
                .ForeignKey("dbo.Carreras", t => t.MATRICULA_CARRERAS_ID, cascadeDelete: true)
                .ForeignKey("dbo.Ciclos", t => t.MATRICULA_CICLOS_ID, cascadeDelete: true)
                .Index(t => t.MATRICULA_PERSONAS_ID)
                .Index(t => t.MATRICULA_CARRERAS_ID)
                .Index(t => t.MATRICULA_CICLOS_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matriculas", "MATRICULA_CICLOS_ID", "dbo.Ciclos");
            DropForeignKey("dbo.Matriculas", "MATRICULA_CARRERAS_ID", "dbo.Carreras");
            DropForeignKey("dbo.Matriculas", "MATRICULA_PERSONAS_ID", "dbo.Personas");
            DropForeignKey("dbo.Cursadas", "CURSADA_MATERIAS_X_CURSOS_ID", "dbo.Materias_X_Cursos");
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_SEDES_ID", "dbo.Sedes");
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_MATERIAS_ID", "dbo.Materias");
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_DOCENTE_ID", "dbo.Personas");
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CICLOS_ID", "dbo.Ciclos");
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CARRERAS_ID", "dbo.Carreras");
            DropForeignKey("dbo.Horarios_Cursadas", "HORARIO_CURSADA_MATERIAS_X_CURSOS_ID", "dbo.Materias_X_Cursos");
            DropForeignKey("dbo.Horarios_Cursadas", "HORARIO_CURSADA_HORAS_ID", "dbo.Horas");
            DropForeignKey("dbo.Cursadas", "CURSADA_ALUMNOS_ID", "dbo.Personas");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_VOCAL2_ID", "dbo.Personas");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_VOCAL1_ID", "dbo.Personas");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_TURNOS_EXAMENES_ID", "dbo.Turnos_Examenes");
            DropForeignKey("dbo.Turnos_Examenes", "TURNO_EXAMEN_CICLOS_ID", "dbo.Ciclos");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_PRESIDENTE_ID", "dbo.Personas");
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PROVINCIA_ID", "dbo.Provincias");
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_PAIS_ID", "dbo.Paises");
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_CIUDAD_ID", "dbo.Ciudades");
            DropForeignKey("dbo.Personas", "PERSONA_NACIMIENTO_BARRIO_ID", "dbo.Barrios");
            DropForeignKey("dbo.Barrios", "BARRIO_CIUDAD_ID", "dbo.Ciudades");
            DropForeignKey("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_ALUMNOS_ID", "dbo.Personas");
            DropForeignKey("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID", "dbo.Actas_Examenes");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_MATERIAS_ID", "dbo.Materias");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_CARRERAS_ID", "dbo.Carreras");
            DropForeignKey("dbo.Materias", "MATERIA_CARRERAS_ID", "dbo.Carreras");
            DropIndex("dbo.Matriculas", new[] { "MATRICULA_CICLOS_ID" });
            DropIndex("dbo.Matriculas", new[] { "MATRICULA_CARRERAS_ID" });
            DropIndex("dbo.Matriculas", new[] { "MATRICULA_PERSONAS_ID" });
            DropIndex("dbo.Horarios_Cursadas", new[] { "HORARIO_CURSADA_HORAS_ID" });
            DropIndex("dbo.Horarios_Cursadas", new[] { "HORARIO_CURSADA_MATERIAS_X_CURSOS_ID" });
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_SEDES_ID" });
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_DOCENTE_ID" });
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_MATERIAS_ID" });
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_CARRERAS_ID" });
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_CICLOS_ID" });
            DropIndex("dbo.Cursadas", new[] { "CURSADA_MATERIAS_X_CURSOS_ID" });
            DropIndex("dbo.Cursadas", new[] { "CURSADA_ALUMNOS_ID" });
            DropIndex("dbo.Turnos_Examenes", new[] { "TURNO_EXAMEN_CICLOS_ID" });
            DropIndex("dbo.Barrios", new[] { "BARRIO_CIUDAD_ID" });
            DropIndex("dbo.Actas_Examenes_Detalles", new[] { "ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID" });
            DropIndex("dbo.Actas_Examenes_Detalles", new[] { "ACTA_EXAMEN_DETALLE_ALUMNOS_ID" });
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_BARRIO_ID" });
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_CIUDAD_ID" });
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_PROVINCIA_ID" });
            DropIndex("dbo.Personas", new[] { "PERSONA_NACIMIENTO_PAIS_ID" });
            DropIndex("dbo.Materias", new[] { "MATERIA_CARRERAS_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_VOCAL2_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_VOCAL1_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_PRESIDENTE_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_MATERIAS_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_CARRERAS_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_TURNOS_EXAMENES_ID" });
            DropTable("dbo.Matriculas");
            DropTable("dbo.Sedes");
            DropTable("dbo.Horas");
            DropTable("dbo.Horarios_Cursadas");
            DropTable("dbo.Materias_X_Cursos");
            DropTable("dbo.Cursadas");
            DropTable("dbo.Ciclos");
            DropTable("dbo.Turnos_Examenes");
            DropTable("dbo.Provincias");
            DropTable("dbo.Paises");
            DropTable("dbo.Ciudades");
            DropTable("dbo.Barrios");
            DropTable("dbo.Actas_Examenes_Detalles");
            DropTable("dbo.Personas");
            DropTable("dbo.Materias");
            DropTable("dbo.Carreras");
            DropTable("dbo.Actas_Examenes");
        }
    }
}
