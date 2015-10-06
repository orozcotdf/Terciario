import Reflux from 'reflux';
import actions from '../actions/navigationActions';

const store = Reflux.createStore({
  listenables: [actions],

  data: {
    sidebarActive: false
  },

  getInitialState() {
    return this.data;
  },

  onToggleSidebar() {
    this.data.sidebarActive = !this.data.sidebarActive;
    this.trigger(this.data);
  }
});

export default store;
