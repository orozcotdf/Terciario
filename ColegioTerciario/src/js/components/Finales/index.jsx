import React from 'react';
import GriddleWithCallback from '../lib/GriddleWithCallback';
import axios from 'axios';
import FechaComponent from '../lib/fecha';

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
              href={`/ActaExamen/Edit/${this.props.data}`}
              target="_blank">
              Editar
            </a>
          </li>
          <li>
            <a
              href={`/ActaExamen/Details/${this.props.data}`}
              target="_blank">
              Imprimir
            </a>
          </li>
          <li>
            <a
              href={`/ActaExamen/Delete/${this.props.data}`}
              target="_blank">
              Eliminar
            </a>
          </li>
        </ul>
      </div>
    );
  }
}

AccionesComponent.propTypes = {
  data: React.PropTypes.number
};

class Finales extends React.Component {
  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    axios({
      url: `/api/Finales/GetFinales`,
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
      'ACTA_EXAMEN_NUMERO',
      'ACTA_EXAMEN_FECHA',
      'ACTA_EXAMEN_LIBRO',
      'ACTA_EXAMEN_FOLIO',
      'ACTA_EXAMEN_TURNO_EXAMEN',
      'ACTA_EXAMEN_CARRERA',
      'ACTA_EXAMEN_MATERIA',
      'ID'
    ];

    const columnMeta = [
      {
        columnName: 'ACTA_EXAMEN_NUMERO',
        displayName: 'Numero'
      }, {
        columnName: 'ACTA_EXAMEN_FECHA',
        displayName: 'Fecha',
        customComponent: FechaComponent
      }, {
        columnName: 'ACTA_EXAMEN_LIBRO',
        displayName: 'Libro'
      }, {
        columnName: 'ACTA_EXAMEN_FOLIO',
        displayName: 'Folio'
      }, {
        columnName: 'ACTA_EXAMEN_TURNO_EXAMEN',
        displayName: 'Turno'
      }, {
        columnName: 'ACTA_EXAMEN_CARRERA',
        displayName: 'Carrera'
      },{
        columnName: 'ACTA_EXAMEN_MATERIA',
        displayName: 'Materia'
      },{
        columnName: 'ID',
        displayName: '',
        customComponent: AccionesComponent
      }
    ];

    return (
      <div>
        <div className="card">
          <div className="card-header ch-alt">
            <h2>Administrar Finales</h2>
            <a href="/ActaExamen/Create"
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

export default Finales;
