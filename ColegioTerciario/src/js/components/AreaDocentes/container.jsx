import React from 'react';
import Main from './main';

export default React.createClass({
  propTypes: {
    default: React.PropTypes.node
  },

  render() {
    return (
      <div>
        {this.props.default ? (
          <div>{this.props.default}</div>
        ) : (
          <Main />

        )}
      </div>
    );
  }
});
