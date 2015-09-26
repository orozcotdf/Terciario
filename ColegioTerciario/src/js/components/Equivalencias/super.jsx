import React from 'react'
import Component from '../Component/main'

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
    input = input.toLowerCase();
    if (input.length >= 3) {
      let _this = this;
      $.get('http://localhost:63440/api/Personas/SelectPersonas?busqueda=' + input, function(data){
        callback(null, {
          options: data,
          //complete: true
        });
      });
    }
  }

  cargarCarreras(input, callback) {
    input = input.toLowerCase();
    if (input.length >= 3) {
      let _this = this;
      $.get('http://localhost:63440/api/Carreras/SelectCarreras?busqueda=' + input, function(data){
        callback(null, {
          options: data,
          //complete: true
        });
      });
    }
  }

  clearAndFocusInput() {
      // Clear the input
    this.setState(this.emptyState, function() {
      // This code executes after the inputs are cleared
      React.findDOMNode(this.refs.EQUIVALENCIA_FECHA).focus();
    });
  }

  exit() {
    this.clearAndFocusInput();
    this.context.router.transitionTo('equivalencias');
  }
}
