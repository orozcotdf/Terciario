import React from 'react';
import Component from '../../Component/main';
import $ from 'jquery';
import GriddleWithCallback from '../../lib/GriddleWithCallback';

class GriddleActionsComponent extends React.Component {
  render() {
    const editUrl = `/Cursos/Edit/${this.props.data}`;

    return <div><a href={editUrl}>Editar</a></div>;
  }
}

GriddleActionsComponent.propTypes = {
  data: Object
};

export default class CursosDeDocente extends Component {
  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    $.get(`/api/Cursos/ObtenerCursos?docenteId=${this.state.user.Persona}`,
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
  }

  render() {
    const columns = [
      'ID',
      'CICLO_ANIO',
      'CARRERA_NOMBRE',
      'MATERIA_NOMBRE',
      'MATERIA_X_CURSO_CURSO_NOMBRE',
      'SEDE_NOMBRE'
    ];
    const columnMeta = [
      {
        columnName: 'ID',
        customComponent: GriddleActionsComponent
      }, {
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
      }
    ];

    return (
      <div className="portlet light">
        <GriddleWithCallback ref="w"
                  getExternalResults={this._getJsonData.bind(this)}
                  columnMetadata = {columnMeta}
                  columns={columns}
                  loadingText = "Cargando..."
                  noDataMessage = "No se encontraron resultados"/>
      </div>
    );
  }
}
