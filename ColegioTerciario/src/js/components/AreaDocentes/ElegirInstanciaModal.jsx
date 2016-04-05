import React from 'react';
import { connect } from 'react-redux';
import { Modal, Button } from 'react-bootstrap';
import {Link} from 'react-router';
import { bindActionCreators } from 'redux';
import * as AreaDocentesActions from '../../actions/areaDocentesActions';

class ElegirInstanciaModal extends React.Component {
  _closeModal() {
    this.props.actions.closeCourseModal();
  }

  _onExited() {
    this.props.actions.clearSelectedCourse()
  }

  _habilitarSegundoParcial() {
    this.props.actions.enableSecondExamAsync(this.props.areaDocentes.selectedCourse.id);
  }

  render() {
    var instancias = [];

    for (var i = 0; i < this.props.areaDocentes.selectedCourse.amountExams; i++) {
        instancias.push(
          <div>
          <p>
            <Link
              className="btn btn-primary"
              to={`/area-docentes/cursos/${this.props.areaDocentes.selectedCourse.id}/cargaParcial/P${i+1}`}>
                Carga Parcial {i+1}
            </Link>
          </p>
          <p>
            <Link
              className="btn btn-primary"
              to={`/area-docentes/cursos/${this.props.areaDocentes.selectedCourse.id}/cargaParcial/R${i+1}`}>
                Carga Recuperatorio {i+1}
            </Link>
          </p>
          </div>
        );
    }

    return (
      <Modal show={this.props.areaDocentes.showModal}
             onHide={this._closeModal.bind(this)}
             onExited={this._onExited.bind(this)}>
        <Modal.Header closeButton>
          <Modal.Title>Elegir instancia</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {this.props.areaDocentes.selectedCourse.amountExams > 0 ?
            <div>
              <span>Seleccione una instancia</span>
              {instancias}
            </div>
          : <div>
              <span>Debe elegir la cantidad de parciales</span>
              <p><Link
                className="btn btn-primary"
                to={`/area-docentes/cursos/${this.props.areaDocentes.selectedCourse.id}/cargaParcial/P1`}>
                  Carga Parcial 1
              </Link></p>
              <p><Link
                className="btn btn-primary"
                to={`/area-docentes/cursos/${this.props.areaDocentes.selectedCourse.id}/cargaParcial/R1`}>
                  Carga Recuperatorio 1
              </Link></p>
            </div>
          }

        </Modal.Body>
        <Modal.Footer>
          {this.props.areaDocentes.selectedCourse.amountExams < 2 ?
          <Button onClick={this._habilitarSegundoParcial.bind(this)} className="bgm-green">Habilitar Segundo Parcial</Button>
          : null }
        </Modal.Footer>
      </Modal>
    )
  }
}


const mapStateToProps = function(store) {
  return {
    areaDocentes: store.areaDocentes
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(AreaDocentesActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(ElegirInstanciaModal);

