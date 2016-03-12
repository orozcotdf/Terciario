import React from 'react';
import CargaParcial from '../CargaParcial';


class CargaParcialContainer extends React.Component {

  render() {
    return (
      <CargaParcial
        idCurso={this.props.params.idCurso}
        parcial={this.props.params.parcial}
      />
    )
  }
}

export default CargaParcialContainer;
