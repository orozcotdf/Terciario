import React from 'react';
import GriddleWithCallback from '../lib/GriddleWithCallback';
import axios from 'axios';


const ActionsComponent = React.createClass({
  propTypes: {
    data: React.PropTypes.number
  },

  render() {
    return (
      <a
        href={`/Personas/Edit/${this.props.data}`}
        className="btn btn-link waves-effect"
        title="Ver">
        <i className="zmdi zmdi-search"></i>
      </a>
    );
  }
});


class DashboardBedeles extends React.Component {
  _getAlumnosSinSecundario(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    axios({
      url: `/api/Personas/GetAlumnosQueAdeudanTituloSecundario`,
      params: {
        Pagina: page, RegistrosPorPagina: pageSize,
        OrdenarPorColumna: sortColumn,
        OrdenarAsc: sortAscending
      }
    })
    .then(
      (response) => {
        callback({
          results: response.data.Resultados,
          totalResults: response.data.CantidadResultados,
          pageSize
        });
      }
    );
  }

  render() {
    const columns = [
      'PERSONA_APELLIDO',
      'PERSONA_NOMBRE',
      'PERSONA_DOCUMENTO_NUMERO',
      'ID'
    ];

    const columnMeta = [
      {
        columnName: 'PERSONA_APELLIDO',
        displayName: 'Apellido'
      }, {
        columnName: 'PERSONA_NOMBRE',
        displayName: 'Nombre'
      }, {
        columnName: 'PERSONA_DOCUMENTO_NUMERO',
        displayName: 'Documento'
      }, {
        columnName: 'ID',
        displayName: '',
        customComponent: ActionsComponent
      }
    ];

    return (
      <div className="row">
        <div className="col-md-6">
          <div className="card">
            <div className="card-header">
              <h2>Cent 11</h2>
            </div>
            <div className="card-body card-padding">
              <div className="row">
                <div className="col-md-6">
                  <div className="portlet light">
                    <div className="portlet-title">
                      <div className="caption">
                        <span className="caption-subject font-green-sharp bold uppercase">
                          Alumnos
                        </span>
                      </div>
                    </div>
                  </div>
                </div>

              </div>
            </div>
          </div>
        </div>
        <div className="col-md-6">
          <div className="card">
            <div className="card-header">
              <h2>
                Alumnos
                <small>Que adeudan titulo secundario</small>
              </h2>
            </div>
            <div className="card-body">
              <div className="caption">
                <span className="caption-subject font-red-sunglo bold uppercase">
                  <GriddleWithCallback ref="w"
                    getExternalResults={this._getAlumnosSinSecundario}
                    resultsPerPage={10}
                    columns={columns}
                    columnMetadata={columnMeta}
                    loadingText="Cargando..."
                    noDataMessage="No se encontraron resultados"
                    tableClassName="table table-vmiddle"
                  />
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default DashboardBedeles;
