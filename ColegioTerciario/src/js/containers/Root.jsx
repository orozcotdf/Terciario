import React from 'react';
import {Provider} from 'react-redux';
import routes from '../routes';
import {ReduxRouter} from 'redux-router';
import DevTools from './DevTools';
import {createDevToolsWindow} from '../utils';

class Root extends React.Component {
  renderDevTools() {
    if (!this.props.debug) {
      return null;
    }

    return this.props.debugExternal ?
      createDevToolsWindow(this.props.store) : <DevTools />;
  }

  render() {
    return (
      <div>
        <Provider store={this.props.store}>
          <div>
            <ReduxRouter routes={routes}/>
            {this.renderDevTools()}
          </div>
        </Provider>
      </div>
    );
  }
}

Root.propTypes = {
  store: React.PropTypes.object.isRequired,
  debug: React.PropTypes.bool,
  debugExternal: React.PropTypes.bool
};

Root.defaultProps = {
  debug: false,
  debugExternal: false
};

export default Root;
