import React from 'react';
import Reflux from 'reflux';
import store from '../stores/adminInscripcionesStore';
import GriddleWithCallback from '../../lib/GriddleWithCallback';
import axios from 'axios';

const AdminInscripcionesIndex = React.createClass({
  mixins: [Reflux.connect(store)],
  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    axios({
      url: `/api/Inscripciones/GetInscripciones`,
      params: {
        Pagina: page, RegistrosPorPagina: pageSize
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
                loadingText="Cargando..."
                noDataMessage="No se encontraron resultados"
                tableClassName="table table-vmiddle"
              />
          </div>
        </div>
      </div>
    );
  }
});

export default AdminInscripcionesIndex;
