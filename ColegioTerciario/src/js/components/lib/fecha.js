import React from 'react';
import moment from 'moment';

const FechaComponent = React.createClass({
  propTypes: {
    data: React.PropTypes.string
  },

  formatDate(date) {
    let d = date.getDate();
    let m = date.getMonth() + 1;
    const y = date.getFullYear();

    if (d.toString().length === 1) { d = '0' + d; }
    if (m.toString().length === 1) { m = '0' + m; }
    return d + '/' + m + '/' + y;
  },

  render() {
    const date = moment(this.props.data).toDate();
    return <div>{this.formatDate(date)}</div>;
  }
});

export default FechaComponent;
