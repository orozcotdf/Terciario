import React from 'react';
import { connect } from 'react-redux';


class ElegirInstanciaModal extends React.Component {
  render() {
    return (
      <div>{this.props.areaDocentes.selected_course}</div>
    )
  }
}


const mapStateToProps = function(store) {
  return {
    areaDocentes: store.areaDocentes
  };
}
export default connect(mapStateToProps)(ElegirInstanciaModal);

