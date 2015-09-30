import React from 'react';
import $ from 'jquery';
import Equivalencias from './super';
import Select from 'react-select';
import {DatePicker, TextField, FlatButton} from 'material-ui';

require('react-select/dist/default.css');

export default class AgregaEquivalencia extends Equivalencias {

  constructor(props) {
    super(props);
    this.emptyState = {
      EQUIVALENCIA_FECHA: '',
      EQUIVALENCIA_NRO_DISPOSICION: '',
      EQUIVALENCIA_ALUMNO_ID: '',
      EQUIVALENCIA_CARRERA_ID: ''
    };
    this.state = this.emptyState;
  }

  onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  }

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
  }

  setFecha(nill, date) {
    this.setState({
      EQUIVALENCIA_FECHA: this.formatDate(date)
    });
  }

  render() {
    const submitHandler = event => { return this.onSubmit(event); };
    const changeHandler = event => { return this.onChange(event); };
    return (
        <div className="col-sm-6 col-sm-offset-3">
          <div className="portlet light">
            <div className="portlet-title">
              <div className="caption">
                Nueva Equivalencia
              </div>
            </div>
            <div className="portlet-body form">
              <form>
                <div className="form-body">
                  <DatePicker name="EQUIVALENCIA_FECHA"
                    hintText="Click para elegir fecha"
                    formatDate={this.formatDate}
                    autoOk={true}
                    mode="inline"
                    onChange={this.setFecha.bind(this)}
                    ref="EQUIVALENCIA_FECHA"
                    fullWidth={true}/>

                  <TextField name="EQUIVALENCIA_NRO_DISPOSICION"
                    hintText="Nro Disposicion"
                    ref="EQUIVALENCIA_NRO_DISPOSICION"
                    onChange={changeHandler} fullWidth={true}
                  />
                  <div className="form-group">
                    <Select
                      name="EQUIVALENCIA_ALUMNO_ID"
                      asyncOptions={this.cargarAlumnos}
                      onChange={this.setAlumno.bind(this)}
                      value=""
                      cacheAsyncResults={false}
                      clearable={true}
                      placeholder="Alumno"
                    />
                  </div>
                  <div className="form-group">
                    <Select
                      name="EQUIVALENCIA_CARRERA_ID"
                      asyncOptions={this.cargarCarreras}
                      onChange={this.setCarrera.bind(this)}
                      value=""
                      clearable={true}
                      placeholder="Carrera"
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
    );
  }
}


AgregaEquivalencia.contextTypes = {
  router: React.PropTypes.func.isRequired
};

