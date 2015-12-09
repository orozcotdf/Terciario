import React from 'react';
import $ from 'jquery';
import Select from 'react-select';
import {DatePicker, TextField, FlatButton} from 'material-ui';
import 'react-select/dist/react-select.css';

const AgregaEquivalencia = React.createClass({
  propTypes: {
    history: React.PropTypes.object
  },
  getInitialState() {
    return {
      EQUIVALENCIA_FECHA: '',
      EQUIVALENCIA_NRO_DISPOSICION: '',
      EQUIVALENCIA_ALUMNO_ID: '',
      EQUIVALENCIA_CARRERA_ID: ''
    };
  },

  onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  },

  onSubmit(e) {
    $.post('/api/Equivalencias/PostEquivalencia', {
      EQUIVALENCIA_FECHA: this.formatDateForPost(this.refs.EQUIVALENCIA_FECHA.getDate()),
      EQUIVALENCIA_NRO_DISPOSICION: this.state.EQUIVALENCIA_NRO_DISPOSICION,
      EQUIVALENCIA_ALUMNO_ID: this.state.EQUIVALENCIA_ALUMNO_ID,
      EQUIVALENCIA_CARRERA_ID: this.state.EQUIVALENCIA_CARRERA_ID
    }, () => {
      this.exit();
    }).fail((data) => {
      if (data.responseJSON.ModelState['equivalencia.EQUIVALENCIA_FECHA']) {
        this.refs.EQUIVALENCIA_FECHA.refs.input.setErrorText(
          data.responseJSON.ModelState['equivalencia.EQUIVALENCIA_FECHA']
        );
      }
      if (data.responseJSON.ModelState.EQUIVALENCIA_NRO_DISPOSICION) {
        this.refs.EQUIVALENCIA_NRO_DISPOSICION.setErrorText(
          data.responseJSON.ModelState.EQUIVALENCIA_NRO_DISPOSICION
        );
      }
    });
  },
  setAlumno(value) {
    this.setState({
      EQUIVALENCIA_ALUMNO_ID: value
    });
  },

  setCarrera(value) {
    this.setState({
      EQUIVALENCIA_CARRERA_ID: value
    });
  },

  cargarAlumnos(input, callback) {
    if (input.length >= 3) {
      $.get(`/api/Personas/SelectPersonas?busqueda=${input.toLowerCase()}`, function (data) {
        callback(null, {
          options: data
        });
      });
    }
  },

  cargarCarreras(input, callback) {
    if (input.length >= 3) {
      $.get(`/api/Carreras/SelectCarreras?busqueda=${input.toLowerCase()}`, function (data) {
        callback(null, {
          options: data
        });
      });
    }
  },

  setFecha(nill, value) {
    this.setState({
      EQUIVALENCIA_FECHA: this.formatDate(value)
    });
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

  clearAndFocusInput() {
    // Clear the input
    this.setState({
      EQUIVALENCIA_FECHA: '',
      EQUIVALENCIA_NRO_DISPOSICION: '',
      EQUIVALENCIA_ALUMNO_ID: '',
      EQUIVALENCIA_CARRERA_ID: ''
    }, function () {
      // This code executes after the inputs are cleared
      this.refs.EQUIVALENCIA_FECHA.focus();
    });
  },

  exit() {
    this.clearAndFocusInput();
    this.props.history.pushState(null, '/equivalencias');
  },

  render() {
    return (
        <div className="col-sm-6 col-sm-offset-3">
          <div className="card">
            <div className="card-header ch-alt">
              <h2>
                Nueva Equivalencia
              </h2>
            </div>
            <div className="card-body card-padding form">
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
                  />
                    <TextField name="EQUIVALENCIA_NRO_DISPOSICION"
                               hintText="Nro Disposicion"
                               ref="EQUIVALENCIA_NRO_DISPOSICION"
                               onChange={this.onChange} fullWidth={true}
                        />
                    <div className="form-group">
                        <Select
                            name="EQUIVALENCIA_ALUMNO_ID"
                            asyncOptions={this.cargarAlumnos}
                            onChange={this.setAlumno}
                            value=""
                            cacheAsyncResults={false}
                            clearable={true}
                            placeholder="Alumno"
                            autoload={false}
                            />
                    </div>
                    <div className="form-group">
                        <Select
                            name="EQUIVALENCIA_CARRERA_ID"
                            asyncOptions={this.cargarCarreras}
                            onChange={this.setCarrera}
                            value=""
                            clearable={true}
                            placeholder="Carrera"
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
    );
  }
});

export default AgregaEquivalencia;
