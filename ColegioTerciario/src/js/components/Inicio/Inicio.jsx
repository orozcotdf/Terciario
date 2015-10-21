import React from 'react';
import DashboardDocentes from './DashboardDocentes';
import DashboardBedeles from './DashboardBedeles';

export default class InicioComponent extends React.Component {
  esDocente() {
    return User.isInRole('Docente');
  }
  render() {
    let contenido;

    if (this.esDocente()) {
      contenido = <DashboardDocentes />;
    } else {
      contenido = <DashboardBedeles />;
    }
    return contenido;
  }
}
