import Reflux from 'reflux';
import actions from '../actions/navigationActions';
import $ from 'jquery';
require('bootstrap-notify/bootstrap-notify');


const store = Reflux.createStore({
  listenables: [actions],
  message: {},

  getInitialState() {
    return this.message;
  },
  onSetMessage(message) {
    console.log(message);
    $.notify(message);
  }
});

export default store;
