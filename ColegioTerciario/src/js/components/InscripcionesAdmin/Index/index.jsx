import React from 'react';
import Reflux from 'reflux';
import store from '../stores/adminInscripcionesStore';
import GriddleWithCallback from '../../lib/GriddleWithCallback';
import axios from 'axios';

class AccionesComponent extends React.Component {
  render() {
    return (
      <div className="dropdown">
        <a href="#" className="dropdown-toggle btn btn-link btn-icon waves-effect"
           data-toggle="dropdown" aria-expanded="true">
          <i className="zmdi zmdi-more-vert"></i>
        </a>

        <ul className="dropdown-menu pull-right bgm-bluegray">
          <li>
            <a
              href={`/Publico/Inscripciones/ImprimirInscripcion/${this.props.data}`}
              target="_blank">
              Imprimir
            </a>
          </li>
        </ul>
      </div>
    );
  }
}

AccionesComponent.propTypes = {
  data: React.PropTypes.string
};

const AdminInscripcionesIndex = React.createClass({
  mixins: [Reflux.connect(store)],
  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    axios({
      url: `/api/Inscripciones/GetInscripciones`,
      params: {
        Pagina: page,
        RegistrosPorPagina: pageSize,
        Filtro: filterString
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
  },
  render() {
    const columns = [
      'INSCRIPCIONES_CARRERA',
      'INSCRIPCIONES_NOMBRE',
      'INSCRIPCIONES_DOCUMENTO_NUMERO',
      'ID'
    ];

    const columnMeta = [
      {
        columnName: 'INSCRIPCIONES_CARRERA',
        displayName: 'Carrera'
      }, {
        columnName: 'INSCRIPCIONES_NOMBRE',
        displayName: 'Nombre'
      }, {
        columnName: 'INSCRIPCIONES_DOCUMENTO_NUMERO',
        displayName: 'Documento'
      }, {
        columnName: 'ID',
        displayName: '',
        customComponent: AccionesComponent
      }
    ];

    return (
      <div>
        <div className="card">
          <div className="card-header">
            <h2>Administrar Inscripciones</h2>
          </div>
          <div className="card-body">
            <GriddleWithCallback ref="w"
                getExternalResults={this._getJsonData}
                resultsPerPage={10}
                columns={columns}
                columnMetadata={columnMeta}
                loadingText="Cargando..."
                noDataMessage="No se encontraron resultados"
                tableClassName="table table-vmiddle"
                showFilter={true}
                enableSort={false}
              />
          </div>
        </div>
      </div>
    );
  }
});

export default AdminInscripcionesIndex;
