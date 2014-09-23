using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models.Configs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ColegioTerciario.Models
{
    public class ColegioTerciarioContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ColegioTerciarioContext() : base("name=ColegioTerciarioContext")
        {
        }

        public System.Data.Entity.DbSet<ColegioTerciario.DAL.Models.Persona> Personas { get; set; }
        public System.Data.Entity.DbSet<ColegioTerciario.DAL.Models.Pais> Paises { get; set; }
        public System.Data.Entity.DbSet<ColegioTerciario.DAL.Models.Ciudad> Ciudades { get; set; }
        public System.Data.Entity.DbSet<ColegioTerciario.DAL.Models.Provincia> Provincias { get; set; }
        public DbSet<ColegioTerciario.DAL.Models.Ciclo> Ciclos { get; set; }
        public DbSet<ColegioTerciario.DAL.Models.Carrera> Carreras { get; set; }
        public DbSet<ColegioTerciario.DAL.Models.Matricula> Matriculas { get; set; }
        public DbSet<ColegioTerciario.DAL.Models.Materia> Materias { get; set; }
        public IDbSet<Acta_Examen> Actas_Examenes { get; set; }
        public IDbSet<Acta_Examen_Detalle> Actas_Examenes_Detalles { get; set; }
        public DbSet<ColegioTerciario.DAL.Models.Cursada> Cursadas { get; set; }
        public DbSet<ColegioTerciario.DAL.Models.Hora> Horas { get; set; }
        public DbSet<ColegioTerciario.DAL.Models.Horario_Cursada> Horarios_Cursadas { get; set; }
        public DbSet<ColegioTerciario.DAL.Models.Materia_x_Curso> Materias_X_Cursos { get; set; }
        public DbSet<ColegioTerciario.DAL.Models.Turno_Examen> Turnos_Examenes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActaExamenConfig());
            modelBuilder.Configurations.Add(new ActaExamenDetalleConfig());
            base.OnModelCreating(modelBuilder);
            
            
        }
    }

    
}
