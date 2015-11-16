import React from 'react';

class Layout extends React.Component {
  render() {
    return (
      <div>
        {this.props.default}
      </div>
    );
  }
}


Layout.propTypes = {
  default: React.PropTypes.node
};


export default Layout;
