import React from 'react';
import axios from'axios';
import Select from 'react-select';
import {RaisedButton} from 'material-ui';
import {Modal} from 'react-bootstrap';
import Notification from '../../UI/Notification';

export default React.createClass({

  propTypes: {
    modelId: React.PropTypes.integer,
    parentId: React.PropTypes.integer,
    showModal: React.PropTypes.bool,
    close: React.PropTypes.func
  },

  getInitialState() {
    return {
      EQUIVALENCIA_DETALLE_TIPO: '',
      EQUIVALENCIA_DETALLE_MATERIA_ID: '',
      EQUIVALENCIA_DETALLE_PROFESOR_ID: ''
    };
  },

  componentWillMount() {
    axios.get(`/api/EquivalenciaDetalle/GetEquivalencia_Detalle/${this.props.modelId}`)
    .then((result) => {
      this.setState({
        EQUIVALENCIA_DETALLE_TIPO: result.data.EQUIVALENCIA_DETALLE_TIPO,
        EQUIVALENCIA_DETALLE_MATERIA_ID: result.data.EQUIVALENCIA_DETALLE_MATERIA_ID,
        EQUIVALENCIA_DETALLE_PROFESOR_ID: result.data.EQUIVALENCIA_DETALLE_PROFESOR_ID,
        PERSONA_NOMBRE: result.data.PERSONA_NOMBRE,
        MATERIA_NOMBRE: result.data.MATERIA_NOMBRE
      });
      Notification.clearNotifications();
    });
  },

  _submitAndClose() {
    let materia;
    let profesor;

    Notification.showLoading();
    if (!isNaN(parseInt(this.refs.EQUIVALENCIA_DETALLE_MATERIA_ID.state.value, 10))) {
      materia = this.refs.EQUIVALENCIA_DETALLE_MATERIA_ID.state.value;
    } else {
      materia = this.state.EQUIVALENCIA_DETALLE_MATERIA_ID;
    }

    if (!isNaN(parseInt(this.refs.EQUIVALENCIA_DETALLE_PROFESOR_ID.state.value, 10))) {
      profesor = this.refs.EQUIVALENCIA_DETALLE_PROFESOR_ID.state.value;
    } else {
      profesor = this.state.EQUIVALENCIA_DETALLE_PROFESOR_ID;
    }
    axios.put('/api/EquivalenciaDetalle/PutEquivalencia_Detalle/' + this.props.modelId, {
      ID: this.props.modelId,
      EQUIVALENCIA_ID: this.props.parentId,
      EQUIVALENCIA_DETALLE_TIPO: this.refs.EQUIVALENCIA_DETALLE_TIPO.state.value,
      EQUIVALENCIA_DETALLE_MATERIA_ID: materia,
      EQUIVALENCIA_DETALLE_PROFESOR_ID: profesor
    }).then((data) => {
      Notification.success('Datos guardados correctamente', true);
      this.props.close();
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
    this.props.close();
  },
  tipos: [
    {value: 0, label: 'Total'},
    {value: 1, label: 'Parcial'},
    {value: 2, label: 'Denegada'}
  ],
  render() {
    return (
      <div>
        <Modal show={this.props.showModal} onHide={this._close}>
          <Modal.Header closeButton={true}>
            <Modal.Title>Modal heading</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <form>
              <div className="form-group">
                <Select
                  ref="EQUIVALENCIA_DETALLE_TIPO"
                  searchable={true}
                  name="EQUIVALENCIA_DETALLE_TIPO"
                  placeholder="Tipo"
                  options={this.tipos}
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
                  searchingText="Buscando..."
                  value={this.state.MATERIA_NOMBRE}
                  autoload={false}
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
                  searchingText="Buscando..."
                  value={this.state.PERSONA_NOMBRE}
                  autoload={false}
                  />
              </div>
            </form>
          </Modal.Body>
          <Modal.Footer>
            <RaisedButton bsStyle="primary" label="Guardar" onClick={this._submitAndClose}/>
          </Modal.Footer>
        </Modal>
      </div>
    );
  }
});
