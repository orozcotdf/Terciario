import React from 'react';
import {TextField, SelectField} from 'material-ui';

const Paso1Component = React.createClass({
  getInitialState() {
    return {
      INSCRIPCIONES_NOMBRE: null,
      INSCRIPCIONES_APELLIDO: null,
      INSCRIPCIONES_DOCUMENTO_TIPO: 'DNI'
    };
  },
  _onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
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
    const _tiposDeDocumento = [
     {payload: 'DNI', text: 'DNI'},
     {payload: 'Libreta Unica', text: 'Libreta Unica'}
    ];

    return (
      <div>
        <div className="row">
          <div className="col-sm-4 col-sm-offset-2">
            <TextField
              name="INSCRIPCIONES_NOMBRE"
              floatingLabelText="Nombre"
              onChange={this._onChange}
              style={this._inputStyles} />
          </div>
          <div className="col-sm-4">
            <TextField
              name="INSCRIPCIONES_APELLIDO"
              floatingLabelText="Apellido"
              onChange={this._onChange}
              style={this._inputStyles}/>
          </div>
        </div>

        <div className="row">
          <div className="col-sm-4 col-sm-offset-2">
            <SelectField
              value={this.state.INSCRIPCIONES_DOCUMENTO_TIPO}
              onChange={this._handleSelectValueChange.bind(this, 'INSCRIPCIONES_DOCUMENTO_TIPO')}
              floatingLabelText="Tipo de Documento"
              menuItems={_tiposDeDocumento}
              style={this._inputStyles} />
          </div>
          <div className="col-sm-4">
            <TextField
              name="INSCRIPCIONES_DOCUMENTO_NUMERO"
              floatingLabelText="Numero de Documento"
              onChange={this._onChange}
              style={this._inputStyles}/>
          </div>
        </div>

      </div>
    );
  }
});

export default Paso1Component;
