import React from 'react';
import Main from './Main/main';

export default React.createClass({
  propTypes: {
    default: React.PropTypes.node
  },

  render() {
    return (

      this.props.default ? (
        <div>{this.props.default}</div>
      ) : (
        <Main />

      )

    );
  }
});
