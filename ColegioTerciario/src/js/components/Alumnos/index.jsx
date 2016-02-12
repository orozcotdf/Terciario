import React from 'react';
import Reflux from 'reflux';
import GriddleWithCallback from '../lib/GriddleWithCallback';
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
              href={`/Personas/Edit/${this.props.data}`}
              target="_blank">
              Editar
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

class Alumnos extends React.Component {
  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    axios({
      url: `/api/Personas/GetAlumnos`,
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
  }

  render() {
    const columns = [
      'PERSONA_DOCUMENTO_NUMERO',
      'PERSONA_NOMBRE',
      'PERSONA_APELLIDO',
      'PERSONA_TELEFONO',
      'ID'
    ];

    const columnMeta = [
      {
        columnName: 'PERSONA_DOCUMENTO_NUMERO',
        displayName: 'Documento'
      }, {
        columnName: 'PERSONA_NOMBRE',
        displayName: 'Nombre'
      }, {
        columnName: 'PERSONA_APELLIDO',
        displayName: 'Apellido'
      }, {
        columnName: 'PERSONA_TELEFONO',
        displayName: 'Telefono'
      }, {
        columnName: 'ID',
        displayName: '',
        customComponent: AccionesComponent
      }
    ];

    return (
      <div>
        <div className="card">
          <div className="card-header ch-alt">
            <h2>Administrar Alumnos</h2>
            <a href="/Personas/Create"
              className="btn bgm-cyan btn-float waves-effect waves-circle waves-float">
              <i className="zmdi zmdi-plus"></i>
            </a>
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
}

export default Alumnos;
