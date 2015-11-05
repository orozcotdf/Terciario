import React from 'react';
import {TextField} from 'material-ui';

const Paso2Component = React.createClass({
  getInitialState() {
    return {
      INSCRIPCIONES_EMAIL: null,
      INSCRIPCIONES_EMAIL2: null
    };
  },
  _onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  },
  _inputStyles: {
    width: '100%'
  },
  render() {
    return (
      <div>
        <div className="row">
          <div className="col-sm-8 col-sm-offset-2">
            Tenga en cuenta que esta direccion de mail sera utilizada
            para enviar el formulario de pre-inscripcion.
          </div>
        </div>
        <div className="row">
          <div className="col-sm-4 col-sm-offset-2">
            <TextField
              name="INSCRIPCIONES_EMAIL"
              floatingLabelText="Email"
              onChange={this._onChange}
              style={this._inputStyles} />
          </div>
          <div className="col-md-4">
            <TextField
              name="INSCRIPCIONES_EMAIL2"
              floatingLabelText="Repetir Email"
              onChange={this._onChange}
              style={this._inputStyles} />
          </div>
        </div>
      </div>
    );
  }
});

export default Paso2Component;
