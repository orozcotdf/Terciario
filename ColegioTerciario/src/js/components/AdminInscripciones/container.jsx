import React from 'react';
import AdminInscripciones from './Index/index.jsx';

export default React.createClass({
  propTypes: {
    children: React.PropTypes.node
  },

  render() {
    return (

      this.props.children ? (
        <div>
          {this.props.children}
        </div>
      ) : (
        <AdminInscripciones />
    )
    );
  }
});
