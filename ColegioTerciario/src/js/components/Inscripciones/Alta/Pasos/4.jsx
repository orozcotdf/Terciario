import React from 'react';
import {TextField, SelectField} from 'material-ui';
import Select from 'react-select';
import axios from 'axios';

const Paso4Component = React.createClass({
  getInitialState() {
    return {
      INSCRIPCIONES_SEXO: null,
      INSCRIPCIONES_DOMICILIO: null,
      INSCRIPCIONES_NACIMIENTO_BARRIO_ID: null,
      INSCRIPCIONES_TELEFONO: null
    };
  },
  _onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  },
  _cargarBarrios(input, callback) {
    if (input.length >= 3) {
      axios.get('/api/Ubicaciones/Barrios', {
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
  _setBarrio(value) {
    this.setState({
      INSCRIPCIONES_NACIMIENTO_BARRIO_ID: value
    });
  },
  _handleSelectValueChange(name, e) {
    const change = {};

    change[name] = e.target.value;
    this.setState(change);
  },
  _inputStyles: {
    width: '100%'
  },

  render() {
    const _sexos = [
     {payload: 'M', text: 'Masculino'},
     {payload: 'F', text: 'Femenino'}
    ];

    return (
      <div>
        <div className="row">
          <div className="col-sm-4 col-sm-offset-2">
            <SelectField
              value={this.state.INSCRIPCIONES_SEXO}
              onChange={this._handleSelectValueChange.bind(this, 'INSCRIPCIONES_SEXO')}
              floatingLabelText="Sexo"
              menuItems={_sexos}
              style={this._inputStyles}/>
          </div>
          <div className="col-md-4">
            <TextField
              type="text"
              name="INSCRIPCIONES_DOMICILIO"
              floatingLabelText="Domicilio"
              onChange={this._onChange}
              style={this._inputStyles}
            />
          </div>
        </div>
        <div className="row">
          <div className="col-sm-4 col-sm-offset-2">
            <TextField
              className="inputSmall"
              type="text"
              name="INSCRIPCIONES_TELEFONO"
              floatingLabelText="Telefono de Contacto"
              onChange={this._onChange}
              style={this._inputStyles}
            />
          </div>
          <div className="col-md-4">
            <Select
              name="INSCRIPCIONES_NACIMIENTO_BARRIO_ID"
              asyncOptions={this._cargarBarrios}
              onChange={this._setBarrio}
              value=""
              clearable={true}
              placeholder="Barrio"
              autoload={false}
              searchingText="Buscando..."
              searchPromptText="Escriba para buscar"
              noResultsText="No se encontraron resultados"
            />
          </div>
        </div>
      </div>
    );
  }
});

export default Paso4Component;
