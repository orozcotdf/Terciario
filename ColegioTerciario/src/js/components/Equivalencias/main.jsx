import React from 'react';
import $ from 'jquery';
import Component from '../Component/main';
import {Link} from 'react-router';
import GriddleWithCallback from '../lib/GriddleWithCallback';

require('react-bootstrap-table/css/react-bootstrap-table.min.css');

class FechaComponent extends Component {
  constructor(props) {
    super(props);
  }
  render() {
    const date = new Date(this.props.data);

    return <div>{this.formatDate(date)}</div>;
  }
}

class ActionsComponent extends React.Component {
  render() {
    return (
      <div>
        <Link to={`/equivalencias/${this.props.data}/editar`}
          className="btn btn-link waves-effect">
          <i className="zmdi zmdi-edit"></i>
        </Link>
      </div>
    );
  }
}

export default class EquivalenciasMain extends Component {

  onSelectAll(isSelected) {
    // console.log("is select all: " + isSelected);
  }

  onRowSelect(row, isSelected) {
    // console.log("selected: " + isSelected)
  }

  constructor(props) {
    super(props);
    this.state = {data: []};
    this.selectRowProp = {
      mode: 'checkbox',
      clickToSelect: true,
      bgColor: 'rgb(238, 193, 213)',
      onSelect: this.onRowSelect,
      onSelectAll: this.onSelectAll
    };
  }

  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    $.ajax({
      url: '/api/Equivalencias/GetEquivalencias',
      dataType: 'json',
      data: {Pagina: page, RegistrosPorPagina: pageSize},
      success: (data) => {
        callback({
          results: data.Resultados,
          totalResults: data.CantidadResultados,
          pageSize
        });
      },
      error: (xhr, status, err) => {
        // TODO: mostrar notificacion de errores
        // console.error(this.props.url, status, err.toString());
      }
    });
  }

  columnaAcciones(cell, row) {
    return <Link to={`/equivalencias/${cell}/editar`}>Editar</Link>;
  }

  render() {
    const columns = [
      'ID',
      'EQUIVALENCIA_FECHA',
      'EQUIVALENCIA_NRO_DISPOSICION',
      'EQUIVALENCIA_ALUMNO_NOMBRE',
      'EQUIVALENCIA_CARRERA_NOMBRE'
    ];
    const columnMeta = [
      {
        columnName: 'ID',
        displayName: '',
        customComponent: ActionsComponent
      },
      {
        columnName: 'EQUIVALENCIA_FECHA',
        displayName: 'Fecha',
        customComponent: FechaComponent
      }, {
        columnName: 'EQUIVALENCIA_NRO_DISPOSICION',
        displayName: 'Nro de Disposicion'
      }, {
        columnName: 'EQUIVALENCIA_ALUMNO_NOMBRE',
        displayName: 'Alumno'
      }, {
        columnName: 'EQUIVALENCIA_CARRERA_NOMBRE',
        displayName: 'Carrera'
      }
    ];

    return (
        <div className="card">
          <div className="card-header ch-alt m-b-20">
            <Link to="agrega-equivalencias"
              className="btn bgm-cyan btn-float waves-effect waves-circle waves-float">
              <i className="zmdi zmdi-plus"></i>
            </Link>
          </div>

          <GriddleWithCallback ref="w"
              getExternalResults={this._getJsonData.bind(this)}
              columnMetadata = {columnMeta}
              resultsPerPage={10}
              columns={columns}
              loadingText = "Cargando..."
              noDataMessage = "No se encontraron resultados"/>
        </div>
    );
  }
}

FechaComponent.propTypes = {
  data: React.PropTypes.string
};

ActionsComponent.propTypes = {
  data: React.PropTypes.number
};
