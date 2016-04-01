import React from 'react';
import $ from 'jquery';
import GriddleWithCallback from '../lib/GriddleWithCallback';
import GriddleActionsComponent from '../lib/GriddleActionsComponent';
import UserStore from '../../stores/userStore';
import Reflux from 'reflux';
import AuthorizationMixin from '../../core/AuthorizationMixin';
import reactMixin from 'react-mixin';
import { connect } from 'react-redux';


class AreaDocentesMain extends React.Component {

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
  }

  _rowClick(rowData, event) {
    this.props.dispatch({type: "CHANGE_SELECTED_COURSE", course: rowData.props.data.ID})
  }

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
                  getExternalResults={this._getJsonData.bind(this)}
                  columnMetadata = {columnMeta}
                  columns={columns}
                  loadingText = "Cargando..."
                  tableClassName = "table table-vmiddle"
                  noDataMessage = "No se encontraron resultados"
                  nextText="Siguiente"
                  onRowClick={this._rowClick.bind(this)}/>
        </div>
      </div>
    );
  }
}

reactMixin.onClass(AreaDocentesMain, Reflux.connect(UserStore));
reactMixin.onClass(AreaDocentesMain, AuthorizationMixin);


//export default AreaDocentesMain;

export default connect()(AreaDocentesMain);
