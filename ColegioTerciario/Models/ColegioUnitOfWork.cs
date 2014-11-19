using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models.Repositories;

namespace ColegioTerciario.Models
{
    public class ColegioUnitOfWork : DbContext, IColegioUnitOfWork
    {
        private IDbSet<Persona> _personas;
        private IDbSet<Pais> _paises;
        private IDbSet<Ciudad> _ciudades;
        private IDbSet<Provincia> _provincias;
        private IDbSet<Barrio> _barrios;
        private IDbSet<Ciclo> _ciclos;
        private IDbSet<Carrera> _carreras;
        private IDbSet<Matricula> _matriculas;
        private IDbSet<Materia> _materias;
        private IDbSet<Acta_Examen> _actas_examenes;
        private IDbSet<Acta_Examen_Detalle> _actas_examenes_detalles;
        private IDbSet<Cursada> _cursadas;
        private IDbSet<Hora> _horas;
        private IDbSet<Horario_Cursada> _horarios_cursadas;
        private IDbSet<Materia_x_Curso> _materias_x_cursos;
        private IDbSet<Turno_Examen> _turnos_examenes;
        private IDbSet<Sede> _sedes;

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void CommitAndRefreshChanges()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        

        public IDbSet<Persona> Personas
        {
            get { return _personas ?? (_personas = base.Set<Persona>()); }
        }

        public IDbSet<Pais> Paises
        {
            get { return _paises ?? (_paises = base.Set<Pais>()); }
        }

        public IDbSet<Ciudad> Ciudades
        {
            get { return _ciudades ?? (_ciudades = base.Set<Ciudad>()); }
        }

        public IDbSet<Provincia> Provincias
        {
            get { return _provincias ?? (_provincias = base.Set<Provincia>()); }
        }

        public IDbSet<Barrio> Barrios
        {
            get { return _barrios ?? (_barrios = base.Set<Barrio>()); }
        }

        public IDbSet<Ciclo> Ciclos
        {
            get { return _ciclos ?? (_ciclos = base.Set<Ciclo>()); }
        }

        public IDbSet<Carrera> Carreras
        {
            get { return _carreras ?? (_carreras = base.Set<Carrera>()); }
        }

        public IDbSet<Matricula> Matriculas
        {
            get { return _matriculas ?? (_matriculas = base.Set<Matricula>()); }
        }

        public IDbSet<Materia> Materias
        {
            get { return _materias ?? (_materias = base.Set<Materia>()); }
        }

        public IDbSet<Acta_Examen> Actas_Examenes
        {
            get { return _actas_examenes ?? (_actas_examenes = base.Set<Acta_Examen>()); }
        }

        public IDbSet<Acta_Examen_Detalle> Actas_Examenes_Detalles
        {
            get { return _actas_examenes_detalles ?? (_actas_examenes_detalles = base.Set<Acta_Examen_Detalle>()); }
        }

        public IDbSet<Cursada> Cursadas { get; set; }
        public IDbSet<Hora> Horas { get; set; }
        public IDbSet<Horario_Cursada> Horarios_Cursadas { get; set; }
        public IDbSet<Materia_x_Curso> Materias_X_Cursos { get; set; }
        public IDbSet<Turno_Examen> Turnos_Examenes { get; set; }
        public IDbSet<Sede> Sedes { get; set; }
    }
}