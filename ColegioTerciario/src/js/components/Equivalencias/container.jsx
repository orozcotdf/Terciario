import React from 'react';
import Main from './Main/main';

export default React.createClass({
  propTypes: {
    children: React.PropTypes.node
  },

  render() {
    return (
      <div>
        EQUIVALENCIAS
        {this.props.children ? (
          <div>{this.props.children}</div>
        ) : (
          <Main />

        )}
      </div>
    );
  }
});
