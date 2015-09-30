import React from 'react';
import Component from './Component/main';
import DashboardDocentes from './DashboardDocentes';
import DashboardBedeles from './DashboardBedeles';

export default class InicioComponent extends Component {
  esDocente() {
    return this.state.user.isInRole('Docente');
  }
  render() {
    let contenido;
    if (this.esDocente()) {
      contenido = <DashboardDocentes />;
    } else {
      contenido = <DashboardBedeles />;
    };
    return contenido;
  }
};
