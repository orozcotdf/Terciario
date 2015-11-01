import React from 'react';
import axios from'axios';
import Select from 'react-select';
import {RaisedButton} from 'material-ui';
import {Modal} from 'react-bootstrap';

export default React.createClass({
  propTypes: {
    modelId: React.PropTypes.string.isRequired,
    onClose: React.PropTypes.func.isRequired
  },

  getInitialState() {
    return {
      tipos: [
        {value: 0, label: 'Total'},
        {value: 1, label: 'Parcial'},
        {value: 2, label: 'Denegada'}
      ],
      EQUIVALENCIA_DETALLE_TIPO: '',
      EQUIVALENCIA_DETALLE_MATERIA_ID: '',
      EQUIVALENCIA_DETALLE_PROFESOR_ID: '',
      showModal: false
    };
  },

  _submitAndClose() {
    axios.post('/api/Equivalencias/AgregaMateria', {
      EQUIVALENCIA_ID: this.props.modelId,
      EQUIVALENCIA_DETALLE_TIPO: this.state.EQUIVALENCIA_DETALLE_TIPO,
      EQUIVALENCIA_DETALLE_MATERIA_ID: this.refs.EQUIVALENCIA_DETALLE_MATERIA_ID.state.value,
      EQUIVALENCIA_DETALLE_PROFESOR_ID: this.refs.EQUIVALENCIA_DETALLE_PROFESOR_ID.state.value
    }).then((data) => {
      this.props.onClose();
      this._close();
    });
  },

  _getMaterias(input, callback) {
    if (input.length >= 3) {
      axios.get('/api/Materias/SelectMaterias', {
        params: {
          busqueda: input.toLowerCase()
        }
      })
      .then((response) => {
        callback(null, {
          options: response.data,
          complete: true
        });
      });
    }
  },

  _getProfesores(input, callback) {
    if (input.length >= 3) {
      axios.get('/api/Personas/SelectPersonas', {
        params: {
          busqueda: input.toLowerCase(),
          docente: true,
          cantidad: 5
        }
      })
      .then((response) => {
        callback(null, {
          options: response.data,
          complete: true
        });
      });
    }
  },

  _close() {
    this.setState({showModal: false});
  },

  _open(e) {
    e.preventDefault();
    this.setState({showModal: true});
  },

  _setTipo(value, values) {
    this.setState({EQUIVALENCIA_DETALLE_TIPO: value});
  },

  render() {
    return (
      <div>
        <a href="#" onClick={this._open}
              className="btn bgm-cyan btn-float waves-effect waves-circle waves-float">
          <i className="zmdi zmdi-plus"></i>
        </a>
        <Modal show={this.state.showModal} onHide={this._close}>
          <Modal.Header closeButton={true}>
            <Modal.Title>Agregar Materia a Equivalencia</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <form>
              <div className="form-group">
                <Select
                  searchable={true}
                  name="EQUIVALENCIA_DETALLE_TIPO"
                  placeholder="Tipo"
                  options={this.state.tipos}
                  cacheAsyncResults={false}
                  value={this.state.EQUIVALENCIA_DETALLE_TIPO}
                />
              </div>
              <div className="form-group">
                <Select
                  ref="EQUIVALENCIA_DETALLE_MATERIA_ID"
                  name="EQUIVALENCIA_DETALLE_MATERIA_ID"
                  asyncOptions={this._getMaterias}
                  clearable={true}
                  cacheAsyncResults={false}
                  placeholder="Materia"
                  autoload={false}
                  searchingText="Buscando..."
                />
              </div>
              <div className="form-group">
                <Select
                  ref="EQUIVALENCIA_DETALLE_PROFESOR_ID"
                  name="EQUIVALENCIA_DETALLE_PROFESOR_ID"
                  asyncOptions={this._getProfesores}
                  clearable={true}
                  cacheAsyncResults={false}
                  placeholder="Profesor"
                  autoload={false}
                  searchingText="Buscando..."
                />
              </div>
            </form>
          </Modal.Body>
          <Modal.Footer>
            <RaisedButton bsStyle="primary" label="Guardar" onClick={this._submitAndClose} />
          </Modal.Footer>
        </Modal>
      </div>
    );
  }
});

