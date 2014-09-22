namespace ColegioTerciario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaTablas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materias", "MATERIA_CARRERA_ID", "dbo.Carreras");
            DropIndex("dbo.Materias", new[] { "MATERIA_CARRERA_ID" });
            RenameColumn(table: "dbo.Materias", name: "MATERIA_CARRERA_ID", newName: "MATERIA_CARRERAS_ID");
            RenameColumn(table: "dbo.Matriculas", name: "MATRICULA_CARRERA_ID", newName: "MATRICULA_CARRERAS_ID");
            RenameColumn(table: "dbo.Matriculas", name: "MATRICULA_CICLO_ID", newName: "MATRICULA_CICLOS_ID");
            RenameColumn(table: "dbo.Matriculas", name: "MATRICULA_PERSONA_ID", newName: "MATRICULA_PERSONAS_ID");
            RenameIndex(table: "dbo.Matriculas", name: "IX_MATRICULA_PERSONA_ID", newName: "IX_MATRICULA_PERSONAS_ID");
            RenameIndex(table: "dbo.Matriculas", name: "IX_MATRICULA_CARRERA_ID", newName: "IX_MATRICULA_CARRERAS_ID");
            RenameIndex(table: "dbo.Matriculas", name: "IX_MATRICULA_CICLO_ID", newName: "IX_MATRICULA_CICLOS_ID");
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
                "dbo.Turnos_Examenes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TURNO_EXAMEN_NUMERO = c.Int(nullable: false),
                        TURNO_EXAMEN_NOMBRE = c.String(nullable: false),
                        TURNO_EXAMEN_FECHA_INICIO = c.DateTime(),
                        TURNO_EXAMEN_FECHA_FIN = c.DateTime(),
                        TURNO_EXAMEN_CICLOS_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ciclos", t => t.TURNO_EXAMEN_CICLOS_ID)
                .Index(t => t.TURNO_EXAMEN_CICLOS_ID);
            
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
                        MATERIA_X_CURSO_CURSO = c.String(),
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
                .Index(t => t.MATERIA_X_CURSO_CICLOS_ID)
                .Index(t => t.MATERIA_X_CURSO_CARRERAS_ID)
                .Index(t => t.MATERIA_X_CURSO_MATERIAS_ID)
                .Index(t => t.MATERIA_X_CURSO_DOCENTE_ID);
            
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
            
            AddColumn("dbo.Ciclos", "CICLO_SEMESTRE_1_INICIO", c => c.DateTime());
            AddColumn("dbo.Ciclos", "CICLO_SEMESTRE_1_FIN", c => c.DateTime());
            AddColumn("dbo.Ciclos", "CICLO_SEMESTRE_2_INICIO", c => c.DateTime());
            AddColumn("dbo.Ciclos", "CICLO_SEMESTRE_2_FIN", c => c.DateTime());
            AlterColumn("dbo.Materias", "MATERIA_CARRERAS_ID", c => c.Int());
            CreateIndex("dbo.Materias", "MATERIA_CARRERAS_ID");
            AddForeignKey("dbo.Materias", "MATERIA_CARRERAS_ID", "dbo.Carreras", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materias", "MATERIA_CARRERAS_ID", "dbo.Carreras");
            DropForeignKey("dbo.Cursadas", "CURSADA_MATERIAS_X_CURSOS_ID", "dbo.Materias_X_Cursos");
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_MATERIAS_ID", "dbo.Materias");
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_DOCENTE_ID", "dbo.Personas");
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CICLOS_ID", "dbo.Ciclos");
            DropForeignKey("dbo.Materias_X_Cursos", "MATERIA_X_CURSO_CARRERAS_ID", "dbo.Carreras");
            DropForeignKey("dbo.Horarios_Cursadas", "HORARIO_CURSADA_MATERIAS_X_CURSOS_ID", "dbo.Materias_X_Cursos");
            DropForeignKey("dbo.Horarios_Cursadas", "HORARIO_CURSADA_HORAS_ID", "dbo.Horas");
            DropForeignKey("dbo.Cursadas", "CURSADA_ALUMNOS_ID", "dbo.Personas");
            DropForeignKey("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_ALUMNOS_ID", "dbo.Personas");
            DropForeignKey("dbo.Actas_Examenes_Detalles", "ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID", "dbo.Actas_Examenes");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_VOCAL2_ID", "dbo.Personas");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_VOCAL1_ID", "dbo.Personas");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_TURNOS_EXAMENES_ID", "dbo.Turnos_Examenes");
            DropForeignKey("dbo.Turnos_Examenes", "TURNO_EXAMEN_CICLOS_ID", "dbo.Ciclos");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_PRESIDENTE_ID", "dbo.Personas");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_MATERIAS_ID", "dbo.Materias");
            DropForeignKey("dbo.Actas_Examenes", "ACTA_EXAMEN_CARRERAS_ID", "dbo.Carreras");
            DropIndex("dbo.Horarios_Cursadas", new[] { "HORARIO_CURSADA_HORAS_ID" });
            DropIndex("dbo.Horarios_Cursadas", new[] { "HORARIO_CURSADA_MATERIAS_X_CURSOS_ID" });
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_DOCENTE_ID" });
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_MATERIAS_ID" });
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_CARRERAS_ID" });
            DropIndex("dbo.Materias_X_Cursos", new[] { "MATERIA_X_CURSO_CICLOS_ID" });
            DropIndex("dbo.Cursadas", new[] { "CURSADA_MATERIAS_X_CURSOS_ID" });
            DropIndex("dbo.Cursadas", new[] { "CURSADA_ALUMNOS_ID" });
            DropIndex("dbo.Actas_Examenes_Detalles", new[] { "ACTA_EXAMEN_DETALLE_ACTAS_EXAMENES_ID" });
            DropIndex("dbo.Actas_Examenes_Detalles", new[] { "ACTA_EXAMEN_DETALLE_ALUMNOS_ID" });
            DropIndex("dbo.Turnos_Examenes", new[] { "TURNO_EXAMEN_CICLOS_ID" });
            DropIndex("dbo.Materias", new[] { "MATERIA_CARRERAS_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_VOCAL2_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_VOCAL1_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_PRESIDENTE_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_MATERIAS_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_CARRERAS_ID" });
            DropIndex("dbo.Actas_Examenes", new[] { "ACTA_EXAMEN_TURNOS_EXAMENES_ID" });
            AlterColumn("dbo.Materias", "MATERIA_CARRERAS_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Ciclos", "CICLO_SEMESTRE_2_FIN");
            DropColumn("dbo.Ciclos", "CICLO_SEMESTRE_2_INICIO");
            DropColumn("dbo.Ciclos", "CICLO_SEMESTRE_1_FIN");
            DropColumn("dbo.Ciclos", "CICLO_SEMESTRE_1_INICIO");
            DropTable("dbo.Horas");
            DropTable("dbo.Horarios_Cursadas");
            DropTable("dbo.Materias_X_Cursos");
            DropTable("dbo.Cursadas");
            DropTable("dbo.Actas_Examenes_Detalles");
            DropTable("dbo.Turnos_Examenes");
            DropTable("dbo.Actas_Examenes");
            RenameIndex(table: "dbo.Matriculas", name: "IX_MATRICULA_CICLOS_ID", newName: "IX_MATRICULA_CICLO_ID");
            RenameIndex(table: "dbo.Matriculas", name: "IX_MATRICULA_CARRERAS_ID", newName: "IX_MATRICULA_CARRERA_ID");
            RenameIndex(table: "dbo.Matriculas", name: "IX_MATRICULA_PERSONAS_ID", newName: "IX_MATRICULA_PERSONA_ID");
            RenameColumn(table: "dbo.Matriculas", name: "MATRICULA_PERSONAS_ID", newName: "MATRICULA_PERSONA_ID");
            RenameColumn(table: "dbo.Matriculas", name: "MATRICULA_CICLOS_ID", newName: "MATRICULA_CICLO_ID");
            RenameColumn(table: "dbo.Matriculas", name: "MATRICULA_CARRERAS_ID", newName: "MATRICULA_CARRERA_ID");
            RenameColumn(table: "dbo.Materias", name: "MATERIA_CARRERAS_ID", newName: "MATERIA_CARRERA_ID");
            CreateIndex("dbo.Materias", "MATERIA_CARRERA_ID");
            AddForeignKey("dbo.Materias", "MATERIA_CARRERA_ID", "dbo.Carreras", "ID", cascadeDelete: true);
        }
    }
}
