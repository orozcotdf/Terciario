import React from 'react';
import ReactDOM from 'react-dom';
import $ from 'jquery';
import Select from 'react-select';
import {DatePicker, TextField, FlatButton} from 'material-ui';
import ModalForm from '../AgregaDetallesFormModal';
import GriddleWithCallback from '../../lib/GriddleWithCallback';
import axios from 'axios';
import EditForm from './EditForm';

const AccionesComponent = React.createClass({
  propTypes: {
    metadata: React.PropTypes.object,
    data: React.PropTypes.object
  },
  editarEquivalencia(e) {
    e.preventDefault();
    this.props.metadata.columnMetadata.editarEquivalencia(this.props.data);
  },

  eliminarEquivalencia(e) {
    e.preventDefault();
    this.props.metadata.columnMetadata.eliminarEquivalencia(this.props.data);
  },

  render() {
    return (
      <div className="dropdown">
        <a href="#" className="dropdown-toggle btn btn-link btn-icon waves-effect"
           data-toggle="dropdown" aria-expanded="true">
          <i className="zmdi zmdi-more-vert"></i>
        </a>

        <ul className="dropdown-menu pull-right bgm-bluegray">
          <li>
            <a href="#" onClick={this.editarEquivalencia}>Editar</a>
          </li>
          <li>
            <a href="#" onClick={this.eliminarEquivalencia}>Eliminar</a>
          </li>
        </ul>
      </div>
    );
  }
});

const DetalleTipoComponent = React.createClass({
  propTypes: {
    data: React.PropTypes.object
  },
  render() {
    let tipo;
    const data = parseInt(this.props.data, 10);

    if (data === 0) {
      tipo = 'Total';
    } else if (data === 1) {
      tipo = 'Parcial';
    } else if (data === 2) {
      tipo = 'Denegada';
    }
    return (
      <div>{tipo}</div>
    );
  }
});

export default React.createClass({

  propTypes: {
    params: React.PropTypes.object,
    history: React.PropTypes.object
  },

  getInitialState() {
    return {};
  },

  componentWillMount() {
    axios.get(`/api/Equivalencias/GetEquivalencia/${this.props.params.id}`).then((result) =>{
      this.setState(result.data);
    });
  },

  onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  },

  exit() {
    this._clearAndFocusInput();
    this.props.history.pushState(null, '/equivalencias');
  },

  setFecha(nill, date) {
    this.setState({
      EQUIVALENCIA_FECHA: this.formatDate(date)
    });
  },

  onSubmit(e) {
    axios.put(`/api/Equivalencias/PutEquivalencia/${this.props.params.id}`, {
      ID: this.props.params.id,
      EQUIVALENCIA_FECHA: this.formatDateForPost(this.refs.EQUIVALENCIA_FECHA.getDate()),
      EQUIVALENCIA_NRO_DISPOSICION: this.state.EQUIVALENCIA_NRO_DISPOSICION,
      EQUIVALENCIA_ALUMNO_ID: this.state.EQUIVALENCIA_ALUMNO_ID,
      EQUIVALENCIA_CARRERA_ID: this.state.EQUIVALENCIA_CARRERA_ID
    }).then((result) => {
      this.exit();
    });
  },

  _clearAndFocusInput() {
      // Clear the input
    this.setState({}, function () {
      // This code executes after the component is re-rendered
      ReactDOM.findDOMNode(this.refs.EQUIVALENCIA_FECHA).focus();
    });
  },

  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    $.get(`/api/equivalencias/GetDetalles/${this.props.params.id}`,
      {Pagina: page, RegistrosPorPagina: pageSize},
      function (data) {
        callback({
          totalResults: data.CantidadResultados,
          results: data.Resultados,
          pageSize
        });
      });
  },

  _closeModal() {
    this.refs.w.setPage(0);
  },

  formatDate(date) {
    let d = date.getDate();
    let m = date.getMonth() + 1;
    const y = date.getFullYear();

    if (d.toString().length === 1) { d = '0' + d; }
    if (m.toString().length === 1) { m = '0' + m; }
    return d + '/' + m + '/' + y;
  },

  formatDateForPost(date) {
    const d = date.getDate();
    const m = date.getMonth() + 1;
    const y = date.getFullYear();

    return m + '/' + d + '/' + y;
  },

  _eliminarEquivalencia(id) {
    axios.delete('/api/EquivalenciaDetalle/DeleteEquivalencia_Detalle/' + id)
      .then((result) => {
        this.forceUpdate();
      });
  },

  _editarEquivalencia(id) {
    this.setState({
      showEditForm: true,
      editId: id
    });
  },

  _closeEditForm() {
    this.setState({
      showEditForm: false
    });
  },

  render() {
    const columnMeta = [
      {
        columnName: 'ID',
        displayName: '',
        customComponent: AccionesComponent,
        columnMetadata: {
          eliminarEquivalencia: this._eliminarEquivalencia,
          editarEquivalencia: this._editarEquivalencia
        }
      }, {
        columnName: 'MATERIA_NOMBRE',
        displayName: 'Materia'
      }, {
        columnName: 'PERSONA_NOMBRE',
        displayName: 'Profesor'
      }, {
        columnName: 'EQUIVALENCIA_DETALLE_TIPO',
        displayName: 'Tipo',
        customComponent: DetalleTipoComponent
      }
    ];

    const editForm = this.state.editId ?
      <EditForm
        showModal={this.state.showEditForm}
        modelId={this.state.editId}
        parentId={this.props.params.id}
        close={this._closeEditForm}/> :
      null;

    return (
      <div>

        {editForm}
        <div className="col-sm-6">
          <div className="card card-light">
            <div className="card-header">
              <h2>
                Editar Equivalencia
              </h2>
            </div>
            <div className="card-body card-padding">
              <form>
                <div className="form-body">

                  <DatePicker name="EQUIVALENCIA_FECHA"
                    hintText="Click para elegir fecha"
                    formatDate={this.formatDate}
                    autoOk={true}
                    mode="inline"
                    onChange={this.setFecha}
                    ref="EQUIVALENCIA_FECHA"
                    fullWidth={true}
                    value={new Date(this.state.EQUIVALENCIA_FECHA)}
                  />

                  <TextField
                    name="EQUIVALENCIA_NRO_DISPOSICION"
                    hintText="Nro Disposicion"
                    onChange={this.onChange}
                    fullWidth={true}
                    value={this.state.EQUIVALENCIA_NRO_DISPOSICION}
                  />

                  <div className="form-group">
                    <Select
                      name="EQUIVALENCIA_ALUMNO_ID"
                      asyncOptions={this.cargarAlumnos}
                      onChange={this.setAlumno}
                      clearable={true}
                      placeholder="Alumno"
                      value={this.state.EQUIVALENCIA_ALUMNO_NOMBRE}
                      searchingText="Buscando..."
                      isLoading={false}
                      autoload={false}
                    />
                  </div>
                  <div className="form-group">
                    <Select
                      name="EQUIVALENCIA_CARRERA_ID"
                      asyncOptions={this.cargarCarreras}
                      onChange={this.setCarrera}
                      clearable={true}
                      placeholder="Carrera"
                      value={this.state.EQUIVALENCIA_CARRERA_NOMBRE}
                      searchingText="Buscando..."
                      isLoading={false}
                      autoload={false}
                    />
                  </div>

                </div>
                <div className="form-actions right">
                  <FlatButton label="Cancelar" onTouchTap={this.exit}/>
                  <FlatButton label="Guardar" onTouchTap={this.onSubmit} primary={true}/>
                </div>
              </form>
            </div>
          </div>
        </div>
          <div className="col-sm-6">
            <div className="card">
              <div className="card-header ch-alt">
                <h2>Materias</h2>
                <ModalForm modelId={this.props.params.id} onClose={this._closeModal}/>
              </div>
              <div className="card-body">
                <GriddleWithCallback ref="w"
                  getExternalResults={this._getJsonData}
                  columnMetadata = {columnMeta}
                  columns={['MATERIA_NOMBRE', 'PERSONA_NOMBRE', 'EQUIVALENCIA_DETALLE_TIPO', 'ID']}
                  loadingText = "Cargando..."
                  noDataMessage = "No se encontraron resultados"
                  tableClassName="table table-vmiddle"/>

              </div>
            </div>
          </div>
        </div>
    );
  }
});
