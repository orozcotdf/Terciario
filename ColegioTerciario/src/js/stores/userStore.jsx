import Reflux from 'reflux';
import actions from '../actions/userActions';

const store = Reflux.createStore({
  listenables: [actions],
  data: {
    user: User
  },

  getInitialState() {
    this.data.user.data.UserName = this.data.user.data.DatosPersonales ?
      this.data.user.data.DatosPersonales.PERSONA_APELLIDO + ', ' +
      this.data.user.data.DatosPersonales.PERSONA_NOMBRE :
      this.data.user.data.UserName;
    return this.data;
  },

  onSavePersonalData(data) {
    // console.log(data);
  }
});

export default store;
