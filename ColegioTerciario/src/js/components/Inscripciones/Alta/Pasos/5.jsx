import React from 'react';
import {TextField} from 'material-ui';
import Select from 'react-select';
import axios from 'axios';

const Paso5Component = React.createClass({
  getInitialState() {
    return {
      carreras: [],
      INSCRIPCIONES_TITULO_SECUNDARIO: null,
      INSCRIPCIONES_CARRERA_ID: null
    };
  },
  componentWillMount() {
    axios.get('/api/Inscripciones/GetCarrerasHabilitadas')
      .then((response) => {
        this.setState({
          carreras: response.data
        });
      });
  },
  _onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  },
  _setCarrera(value) {
    this.setState({
      INSCRIPCIONES_CARRERA_ID: value
    });
  },
  render() {
    return (
      <div>
        <div className="row">
          <div className="col-sm-8 col-sm-offset-2">
            <TextField
              name="INSCRIPCIONES_TITULO_SECUNDARIO"
              floatingLabelText="Titulo Secundario"
              onChange={this._onChange}
              style={{width: '100%'}}
            />
          </div>
        </div>

        <div className="row">
          <div className="col-sm-8 col-sm-offset-2">
            <Select
              name="INSCRIPCIONES_CARRERA_ID"
              options={this.state.carreras}
              onChange={this._setCarrera}
              value=""
              clearable={true}
              autoload={true}
              placeholder="Carrera"
              style={{padding: '10px 0'}}
            />
          </div>
        </div>
      </div>
    );
  }
});

export default Paso5Component;
