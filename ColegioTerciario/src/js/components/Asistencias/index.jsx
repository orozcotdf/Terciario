import React from 'react';
import axios from 'axios';

export default class Asistencias extends React.Component {
  render() {
    return (
      <div>ASISTENCIAS {this.props.params.course_id}</div>
    )
  }
}
