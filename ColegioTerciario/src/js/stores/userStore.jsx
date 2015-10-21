import Reflux from 'reflux';
import actions from '../actions/userActions';

const store = Reflux.createStore({
  listenables: [actions],
  data: {
    user: User
  },

  getInitialState() {
    return this.data;
  },

  onSavePersonalData(data) {
    // console.log(data);
  }
});

export default store;
