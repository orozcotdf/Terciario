import React from 'react';
import $ from 'jquery';
import GriddleWithCallback from '../../lib/GriddleWithCallback';
import UserStore from '../../../stores/userStore';
import Reflux from 'reflux';
import {Link} from 'react-router';
import AuthorizationMixin from '../../../core/AuthorizationMixin';

class GriddleActionsComponent extends React.Component {
  render() {
    return (
      <div className="dropdown">
        <a href="#" className="dropdown-toggle btn btn-link btn-icon waves-effect"
          data-toggle="dropdown" aria-expanded="true">
          <i className="zmdi zmdi-more-vert"></i>
        </a>
        <ul className="dropdown-menu pull-right bgm-bluegray">
          <li role="presentation">
            <Link
              to={`/area-docentes/cursos/${this.props.data}/cargaParcial/P1`}>
                Carga Parcial 1
            </Link>
          </li>
          <li role="presentation">
            <Link
              to={`/area-docentes/cursos/${this.props.data}/cargaParcial/P2`}>
                Carga Parcial 2
            </Link>
          </li>
          <li role="presentation">
            <Link
              to={`/area-docentes/cursos/${this.props.data}/cargaParcial/R1`}>
                Carga Recuperatorio 1
            </Link>
          </li>
          <li role="presentation">
            <Link
              to={`/area-docentes/cursos/${this.props.data}/cargaParcial/R2`}>
                Carga Recuperatorio 2
            </Link>
          </li>
        </ul>
      </div>
    );
  }
}

GriddleActionsComponent.propTypes = {
  data: Object
};

const CursosDeDocente = React.createClass({
  mixins: [
    Reflux.connect(UserStore),
    AuthorizationMixin
  ],
  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    $.get(`/api/Cursos/ObtenerCursos?docenteId=${this.state.user.data.Persona}`,
      {
        Pagina: page,
        RegistrosPorPagina: pageSize,
        OrdenarPorColumna: sortColumn,
        OrdenarAsc: sortAscending
      },
      function (data) {
        callback({
          totalResults: data.CantidadResultados,
          results: data.Resultados,
          pageSize
        });
      });
  },

  render() {
    const columns = [
      'CICLO_ANIO',
      'CARRERA_NOMBRE',
      'MATERIA_NOMBRE',
      'MATERIA_X_CURSO_CURSO_NOMBRE',
      'SEDE_NOMBRE',
      'ID'
    ];
    const columnMeta = [
      {
        columnName: 'CICLO_ANIO',
        displayName: 'Año'
      }, {
        columnName: 'CARRERA_NOMBRE',
        displayName: 'Carrera'
      }, {
        columnName: 'MATERIA_NOMBRE',
        displayName: 'Materia'
      }, {
        columnName: 'MATERIA_X_CURSO_CURSO_NOMBRE',
        displayName: 'Curso'
      }, {
        columnName: 'SEDE_NOMBRE',
        displayName: 'Sede'
      }, {
        columnName: 'ID',
        displayName: null,
        customComponent: GriddleActionsComponent
      }
    ];

    return (
      <div className="card light">
        <div className="card-header">
          <h2>Sus Cursos</h2>
        </div>
        <div className="card-body">
          <GriddleWithCallback ref="w"
                  getExternalResults={this._getJsonData}
                  columnMetadata = {columnMeta}
                  columns={columns}
                  loadingText = "Cargando..."
                  tableClassName = "table table-vmiddle"
                  noDataMessage = "No se encontraron resultados"/>
        </div>
      </div>
    );
  }
});

export default CursosDeDocente;
