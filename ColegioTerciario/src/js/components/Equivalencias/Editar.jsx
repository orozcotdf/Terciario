import React from 'react';
import $ from 'jquery';
import Equivalencias from './super';
import Select from 'react-select';
import {DatePicker, TextField, FlatButton} from 'material-ui';
import ModalForm from './AgregaDetallesFormModal';
import GriddleWithCallback from '../lib/GriddleWithCallback';
import axios from 'axios';


class EditarEquivalencia extends Equivalencias {

  constructor(props) {
    super(props);
    this.state = {};
  }

  componentWillMount() {
    axios.get(`/api/Equivalencias/GetEquivalencia/${this.props.params.id}`).then((result) =>{
      this.setState(result.data);
    });
  }

  onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  }

  exit() {
    this._clearAndFocusInput();
    this.context.router.transitionTo('equivalencias');
  }

  setFecha(nill, date) {
    this.setState({
      EQUIVALENCIA_FECHA: this.formatDate(date)
    });
  }
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

    /*
    $.ajax({
      url: `/api/Equivalencias/PutEquivalencia/${this.props.params.id}`,
      dataType: 'json',
      method: 'PUT',
      data: {
        ID: this.props.params.id,
        EQUIVALENCIA_FECHA: this.formatDateForPost(this.refs.EQUIVALENCIA_FECHA.getDate()),
        EQUIVALENCIA_NRO_DISPOSICION: this.state.EQUIVALENCIA_NRO_DISPOSICION,
        EQUIVALENCIA_ALUMNO_ID: this.state.EQUIVALENCIA_ALUMNO_ID,
        EQUIVALENCIA_CARRERA_ID: this.state.EQUIVALENCIA_CARRERA_ID
      },
      success: () => {
        this.exit();
      }
    });
  */
  }

  _clearAndFocusInput() {
      // Clear the input
    this.setState({}, function () {
      // This code executes after the component is re-rendered
      React.findDOMNode(this.refs.EQUIVALENCIA_FECHA).focus();
    });
  }

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
  }

  _closeModal() {
    this.refs.w.setPage(0);
  }

  render() {
    const submitHandler = event => { return this.onSubmit(event); };
    const changeHandler = event => { return this.onChange(event); };
    const columnMeta = [
      {
        columnName: 'MATERIA_NOMBRE',
        displayName: 'Materia'
      }, {
        columnName: 'PERSONA_NOMBRE',
        displayName: 'Profesor'
      }, {
        columnName: 'EQUIVALENCIA_DETALLE_TIPO',
        displayName: 'Tipo'
      }
    ];

    return (
      <div>
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
                    onChange={this.setFecha.bind(this)}
                    ref="EQUIVALENCIA_FECHA"
                    fullWidth={true}
                    value={new Date(this.state.EQUIVALENCIA_FECHA)}
                  />

                  <TextField
                    name="EQUIVALENCIA_NRO_DISPOSICION"
                    hintText="Nro Disposicion"
                    onChange={changeHandler}
                    fullWidth={true}
                    value={this.state.EQUIVALENCIA_NRO_DISPOSICION}
                  />

                  <div className="form-group">
                    <Select
                      name="EQUIVALENCIA_ALUMNO_ID"
                      asyncOptions={this.cargarAlumnos}
                      onChange={this.setAlumno.bind(this)}
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
                      onChange={this.setCarrera.bind(this)}
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
                  <FlatButton label="Cancelar" onTouchTap={this.exit.bind(this)}/>
                  <FlatButton label="Guardar" onTouchTap={submitHandler} primary={true}/>
                </div>
              </form>
            </div>
          </div>
        </div>
          <div className="col-sm-6">
            <ModalForm modelId={this.props.params.id} onClose={this._closeModal.bind(this)}/>

            <div className="portlet light">
              <div className="portlet-body">
                <GriddleWithCallback ref="w"
                  getExternalResults={this._getJsonData.bind(this)}
                  columnMetadata = {columnMeta}
                  columns={['MATERIA_NOMBRE', 'PERSONA_NOMBRE', 'EQUIVALENCIA_DETALLE_TIPO']}
                  loadingText = "Cargando..."
                  noDataMessage = "No se encontraron resultados"/>

              </div>
            </div>
          </div>
        </div>
    );
  }
}

EditarEquivalencia.contextTypes = {
  router: React.PropTypes.func.isRequired
};

EditarEquivalencia.propTypes = {
  params: React.PropTypes.object
};


export default EditarEquivalencia;
