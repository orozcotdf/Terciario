import React from 'react';
import Select from 'react-select';
import axios from 'axios';
import {DatePicker} from 'material-ui';

const Paso3Component = React.createClass({
  getInitialState() {
    return {
      INSCRIPCIONES_NACIMIENTO_FECHA: null,
      INSCRIPCIONES_NACIMIENTO_PAIS_ID: null,
      INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID: null,
      INSCRIPCIONES_NACIMIENTO_CIUDAD_ID: null
    };
  },
  _onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  },
  _cargarPaises(input, callback) {
    if (input.length >= 3) {
      axios.get('/api/Ubicaciones/Paises', {
        params: {
          busqueda: input.toLowerCase()
        }
      }).then((response) => {
        callback(null, {
          options: response.data
        });
      });
    }
  },
  _setPais(value) {
    this.setState({
      INSCRIPCIONES_NACIMIENTO_PAIS_ID: value
    });
  },

  _cargarProvincias(input, callback) {
    if (input.length >= 3) {
      axios.get('/api/Ubicaciones/Provincias', {
        params: {
          busqueda: input.toLowerCase()
        }
      }).then((response) => {
        callback(null, {
          options: response.data
        });
      });
    }
  },
  _setProvincia(value) {
    this.setState({
      INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID: value
    });
  },

  _cargarCiudades(input, callback) {
    if (input.length >= 3) {
      axios.get('/api/Ubicaciones/Ciudades', {
        params: {
          busqueda: input.toLowerCase()
        }
      }).then((response) => {
        callback(null, {
          options: response.data
        });
      });
    }
  },
  _setCiudad(value) {
    this.setState({
      INSCRIPCIONES_NACIMIENTO_CIUDAD_ID: value
    });
  },
  _formatDate(date) {
    let d = date.getDate();
    let m = date.getMonth() + 1;
    const y = date.getFullYear();

    if (d.toString().length === 1) { d = '0' + d; }
    if (m.toString().length === 1) { m = '0' + m; }
    const formattedDate = d + '/' + m + '/' + y;

    return formattedDate;
  },
  _formatDateForPost(date) {
    const d = date.getDate();
    const m = date.getMonth() + 1;
    const y = date.getFullYear();

    return m + '/' + d + '/' + y;
  },
  _setFecha(nill, value) {
    this.setState({
      INSCRIPCIONES_NACIMIENTO_FECHA: this._formatDateForPost(value)
    });
  },
  render() {
    return (
      <div>
        <div className="row">
          <div className="col-sm-4 col-sm-offset-2">
            <DatePicker
              floatingLabelText="Fecha de Nacimiento"
              hintText="Fecha de Nacimiento"
              name="INSCRIPCIONES_NACIMIENTO_FECHA"
              formatDate={this._formatDate}
              locale="es"
              autoOk={true}
              mode="inline"
              onChange={this._setFecha}
              inputStyle={{width: '100%'}}
            />
        </div>
        <div className="col-sm-4">
            <Select
              name="INSCRIPCIONES_NACIMIENTO_PAIS_ID"
              asyncOptions={this._cargarPaises}
              onChange={this._setPais}
              value=""
              clearable={true}
              placeholder="Pais de Nacimiento"
              autoload={false}
              style={{padding: '10px 0'}}
            />
        </div>
      </div>
      <div className="row">
        <div className="col-sm-4 col-sm-offset-2">
          <Select
            name="INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID"
            asyncOptions={this._cargarProvincias}
            onChange={this._setProvincia}
            value=""
            clearable={true}
            placeholder="Provincia de Nacimiento"
            autoload={false}
            style={{padding: '10px 0'}}
          />
        </div>
        <div className="col-sm-4">
          <Select
            name="INSCRIPCIONES_NACIMIENTO_CIUDAD_ID"
            asyncOptions={this._cargarCiudades}
            onChange={this._setCiudad}
            value=""
            clearable={true}
            placeholder="Ciudad de Nacimiento"
            autoload={false}
            style={{padding: '10px 0'}}
          />
        </div>
      </div>
      </div>
    );
  }
});

export default Paso3Component;
