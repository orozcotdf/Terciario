using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models.Repositories;

namespace ColegioTerciario.Models
{
    public interface IColegioUnitOfWork : IEntityFrameworkUnitOfWork
    {
        IDbSet<Persona> Personas { get; }
        IDbSet<Pais> Paises { get; }
        IDbSet<Ciudad> Ciudades { get; }
        IDbSet<Provincia> Provincias { get; }
        IDbSet<Barrio> Barrios { get; }
        IDbSet<Ciclo> Ciclos { get; }
        IDbSet<Carrera> Carreras { get; }
        IDbSet<Matricula> Matriculas { get; }
        IDbSet<Materia> Materias { get; }
        IDbSet<Acta_Examen> Actas_Examenes { get; }
        IDbSet<Acta_Examen_Detalle> Actas_Examenes_Detalles { get; }
        IDbSet<Cursada> Cursadas { get; }
        IDbSet<Hora> Horas { get; }
        IDbSet<Horario_Cursada> Horarios_Cursadas { get; }
        IDbSet<Materia_x_Curso> Materias_X_Cursos { get; }
        IDbSet<Turno_Examen> Turnos_Examenes { get; }
        IDbSet<Sede> Sedes { get; }
    }
}