import React from 'react';
import axios from'axios';
import Select from 'react-select';
import {RaisedButton} from 'material-ui';
import Component from '../Component/main';
import {Modal, Button} from 'react-bootstrap';


export default class AgregaDetallesFormModal extends Component {
  constructor(props) {
    super(props);
    this.state = {
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
  }

  _submitAndClose() {
    axios.post('/api/Equivalencias/AgregaMateria', {
      EQUIVALENCIA_ID: this.props.modelId,
      EQUIVALENCIA_DETALLE_TIPO: this.state.EQUIVALENCIA_DETALLE_TIPO,
      EQUIVALENCIA_DETALLE_MATERIA_ID: this.state.EQUIVALENCIA_DETALLE_MATERIA_ID,
      EQUIVALENCIA_DETALLE_PROFESOR_ID: this.state.EQUIVALENCIA_DETALLE_PROFESOR_ID
    }).then((data) => {
      this.props.onClose();
      this._close();
    });
  }

  _getMaterias(input, callback) {
    if (input.length >= 3) {
      axios.get('/api/Materias/SelectMaterias?busqueda=' + input.toLowerCase(), (data) => {
        callback(null, {
          options: data,
          complete: true
        });
      });
    }
  }

  _getProfesores(input, callback) {
    if (input.length >= 3) {
      axios.get('/api/Personas/SelectPersonas?busqueda=' + input.toLowerCase(),
        {docente: true, cantidad: 5},
        (data) => {
          callback(null, {
            options: data,
            complete: true
          });
        });
    }
  }

  _close() {
    this.setState({showModal: false});
  }

  _open() {
    this.setState({showModal: true});
  }

  _set(field, value) {
    const nextState = {};

    nextState[field] = value;
    this.setState(nextState);
  }

  _setTipo(value, values) {
    this.setState({EQUIVALENCIA_DETALLE_TIPO: value});
  }

  render() {
    return (
      <div>
        <RaisedButton label="Agregar Materias" onTouchTap={this._open.bind(this)}/>
        <Modal show={this.state.showModal} onHide={this._close.bind(this)}>
          <Modal.Header closeButton={true}>
            <Modal.Title>Modal heading</Modal.Title>
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
                  onChange={this._set.bind(this, 'EQUIVALENCIA_DETALLE_TIPO')}
                  value={this.state.EQUIVALENCIA_DETALLE_TIPO}
                />
              </div>
              <div className="form-group">
                <Select
                  name="EQUIVALENCIA_DETALLE_MATERIA_ID"
                  asyncOptions={this._getMaterias}
                  clearable={true}
                  cacheAsyncResults={false}
                  onChange={this._set.bind(this, 'EQUIVALENCIA_DETALLE_MATERIA_ID')}
                  placeholder="Materia"
                  value={this.state.EQUIVALENCIA_DETALLE_MATERIA_ID}
                  autoload={false}
                />
              </div>
              <div className="form-group">
                <Select
                  name="EQUIVALENCIA_DETALLE_PROFESOR_ID"
                  asyncOptions={this._getProfesores}
                  clearable={true}
                  cacheAsyncResults={false}
                  onChange={this._set.bind(this, 'EQUIVALENCIA_DETALLE_PROFESOR_ID')}
                  placeholder="Profesor"
                  value={this.state.EQUIVALENCIA_DETALLE_PROFESOR_ID}
                  autoload={false}
                />
              </div>
            </form>
          </Modal.Body>
          <Modal.Footer>
            <Button bsStyle="primary" onClick={this._submitAndClose.bind(this)}>Guardar</Button>
          </Modal.Footer>
        </Modal>
      </div>
    );
  }
}

AgregaDetallesFormModal.propTypes = {
  modelId: React.PropTypes.string.isRequired,
  onClose: React.PropTypes.func.isRequired
};
