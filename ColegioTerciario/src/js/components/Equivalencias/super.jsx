import React from 'react';
import Component from '../Component/main';
import $ from 'jquery';

export default class EquivalenciasSuper extends Component {
  constructor(props) {
    super(props);
    this.state = {user: window.User};
  }


  setAlumno(value) {
    this.setState({
      EQUIVALENCIA_ALUMNO_ID: value
    });
  }

  setCarrera(value) {
    this.setState({
      EQUIVALENCIA_CARRERA_ID: value
    });
  }

  cargarAlumnos(input, callback) {
    if (input.length >= 3) {
      $.get(`/api/Personas/SelectPersonas?busqueda=${input.toLowerCase()}`, function (data) {
        callback(null, {
          options: data
        });
      });
    }
  }

  cargarCarreras(input, callback) {
    if (input.length >= 3) {
      $.get(`/api/Carreras/SelectCarreras?busqueda=${input.toLowerCase()}`, function (data) {
        callback(null, {
          options: data
        });
      });
    }
  }

  clearAndFocusInput() {
      // Clear the input
    this.setState(this.emptyState, function () {
      // This code executes after the inputs are cleared
      React.findDOMNode(this.refs.EQUIVALENCIA_FECHA).focus();
    });
  }

  exit() {
    this.clearAndFocusInput();
    this.context.router.transitionTo('equivalencias');
  }
}
