import React from 'react';
import Inscripciones from './Alta/main';

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
        <Inscripciones />
    )
    );
  }
});
