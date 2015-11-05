import React from 'react';
import {TextField} from 'material-ui';
import Select from 'react-select';
import axios from 'axios';

const LimiteDeInscripcionesAlert = React.createClass({
  propTypes: {
    show: React.PropTypes.bool
  },
  render() {
    return this.props.show ? (
      <div className="row">
        <div className="col-sm-8 col-sm-offset-2">
          <div className="alert alert-warning" role="alert" style={{textAlign: 'justify'}}>
            Se ha superado el limite de inscripciones para esta carrera.
            Usted se esta por inscribir en lista de espera
          </div>
        </div>
      </div>
    ) : null;
  }
});

const Paso5Component = React.createClass({
  getInitialState() {
    return {
      carreras: [],
      INSCRIPCIONES_TITULO_SECUNDARIO: null,
      INSCRIPCIONES_CARRERA_ID: null,
      INSCRIPCIONES_EN_LISTA_DE_ESPERA: false
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
      INSCRIPCIONES_EN_LISTA_DE_ESPERA: false,
      INSCRIPCIONES_CARRERA_ID: value
    });

    axios.get(`/api/Inscripciones/GetCantidadInscriptosPorCarrera/${value}`)
      .catch((response) => {
        // Devolvio un mensaje!
        this.setState({
          INSCRIPCIONES_EN_LISTA_DE_ESPERA: true
        });
      });
  },
  render() {
    return (
      <div>
        <LimiteDeInscripcionesAlert show={this.state.INSCRIPCIONES_EN_LISTA_DE_ESPERA}/>
        <div className="row">
          <div className="col-sm-8 col-sm-offset-2">
            <TextField
              className="inputSmall"
              name="INSCRIPCIONES_TITULO_SECUNDARIO"
              floatingLabelText="Titulo Secundario (opcional)"
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
              value={this.state.INSCRIPCIONES_CARRERA_ID}
              clearable={true}
              autoload={true}
              placeholder="Carrera a la que se inscribe"
              style={{padding: '10px 0'}}
            />
          </div>
        </div>
      </div>
    );
  }
});

export default Paso5Component;
