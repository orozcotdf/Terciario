import React from 'react';
import AreaDocentesMain from './AreaDocentesMain';
import ElegirInstanciaModal from './ElegirInstanciaModal';
import UserStore from '../../stores/userStore';
import Reflux from 'reflux';
import createApiClientStore from '../../store/configureStore';
import { Provider } from 'react-redux';


// Import the store created in init.js
const store = createApiClientStore();

export default React.createClass({
  propTypes: {
    default: React.PropTypes.node
  },

  getInitialState() {
    return {
      curso_id: null
    }
  },

  _elegirCurso(curso_id) {
    this.setState({
      curso_id
    })
  },

  render() {
    return (
      <Provider store={store}>
        {this.props.default ? (
          <div>{this.props.default}</div>
        ) : (
          <div>
            <AreaDocentesMain onElegirCurso={this._elegirCurso}/>
            <ElegirInstanciaModal id={this.state.curso_id} />

          </div>
        )}
      </Provider>
    );
  }
});
