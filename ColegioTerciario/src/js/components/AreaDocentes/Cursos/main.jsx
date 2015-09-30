import React from 'react';
import Component from '../../Component/main';
import $ from 'jquery';
import GriddleWithCallback from '../../lib/GriddleWithCallback';

require('react-select/dist/default.css');

export default class CursosDeDocente extends Component {
  constructor(props) {
    super(props);

    this.setState({
      docenteId: docenteId
    });
  }

  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    $.get(`/api/Cursos/ObtenerCursos?docenteId=${this.state.docenteId}`,
      {Pagina: page, RegistrosPorPagina: pageSize},
      function (data) {
        callback({
          totalResults: data.CantidadResultados,
          results: data.Resultados,
          pageSize
        });
      });
  }

  render() {
    const columnMeta = [
      {
        columnName: 'Anio',
        displayName: 'Año'
      }, {
        columnName: 'CarreraId',
        displayName: 'Carrera'
      }, {
        columnName: 'CursoNombre',
        displayName: 'Curso'
      }, {
        columnName: 'SedeId',
        visible: false
      }, {
        columnName: 'SedeNombre',
        displayName: 'Sede'
      }
    ];

    return (
      <div>
        <GriddleWithCallback ref="w"
                  getExternalResults={this._getJsonData.bind(this)}
                  columnMetadata = {columnMeta}
                  columns={['Anio', 'CarreraId', 'CursoNombre', 'SedeId', 'SedeNombre']}
                  loadingText = "Cargando..."
                  noDataMessage = "No se encontraron resultados"/>
      </div>
    );
  }
}
