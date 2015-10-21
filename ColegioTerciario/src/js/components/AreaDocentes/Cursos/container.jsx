import React from 'react';
import Main from './main';

export default React.createClass({
  propTypes: {
    children: React.PropTypes.node
  },

  render() {
    return (
      <div>
        {this.props.children ? (
          <div>{this.props.children}</div>
        ) : (
          <Main />

        )}
      </div>
    );
  }
});
