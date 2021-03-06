import React from 'react';
import Reflux from 'reflux';
import store from '../stores/adminInscripcionesStore';
import GriddleWithCallback from '../../lib/GriddleWithCallback';
import axios from 'axios';
import {Toggle} from 'material-ui';
import Notification from 'Notification';

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

class PresentoDocComponent extends React.Component {
  _onToggle(event, toggled) {
    const inscripcionID = this.props.rowData.ID;
    console.log(`Set toggled: ${toggled} for ${inscripcionID} `);
    axios.post(
      `/api/Inscripciones/CambiarEstadoDeDocumentacion/${inscripcionID}`
    ).then((response) => {
      this.refs.toggle.setToggled(toggled);
    }).catch((response) => {
      this.refs.toggle.setToggled(!toggled);
      Notification.error("Ocurrio un error, intente nuevamente.");
    });
  }

  render() {
    return (
      <div>
        <Toggle
          ref="toggle"
          defaultToggled={this.props.data}
          onToggle={this._onToggle.bind(this)}
        />
      </div>
    )
  }
}

PresentoDocComponent.propTypes = {
  data: React.PropTypes.bool
}

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
      'INSCRIPCIONES_PRESENTO_DOCUMENTACION',
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
        columnName: 'INSCRIPCIONES_PRESENTO_DOCUMENTACION',
        displayName: 'Presento Doc.',
        customComponent: PresentoDocComponent
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
