import Reflux from 'reflux';

const store = Reflux.createStore({
  data: {
    user: User
  },

  getInitialState() {
    return this.data;
  }
});

export default store;
