using ColegioTerciario.DAL.Models;
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

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Barrio> Barrios { get; set; }
        public DbSet<Ciclo> Ciclos { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Acta_Examen> Actas_Examenes { get; set; }
        public DbSet<Acta_Examen_Detalle> Actas_Examenes_Detalles { get; set; }
        public DbSet<Cursada> Cursadas { get; set; }
        public DbSet<Hora> Horas { get; set; }
        public DbSet<Horario_Cursada> Horarios_Cursadas { get; set; }
        public DbSet<Materia_x_Curso> Materias_X_Cursos { get; set; }
        public DbSet<Turno_Examen> Turnos_Examenes { get; set; }
        public DbSet<Sede> Sedes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



    }

    
}
