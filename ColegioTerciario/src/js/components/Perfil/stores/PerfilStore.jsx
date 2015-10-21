import Reflux from 'reflux';
import actions from '../actions/perfilActions';
import axios from 'axios';

const store = Reflux.createStore({
  listenables: [actions],

  getInitialState() {
    return window.User.data.DatosPersonales;
  },

  onGuardarDatosPersonales(datos, cb) {
    return axios({
      method: 'PUT',
      url: `/api/Personas/PutPersona/${datos.ID}`,
      data: datos
    }).then((resultado) => {
      this.trigger(datos);
      return cb('Nota guardada correctamente');
    }).catch((response) => {
      return cb('Error al guardar nota');
    });
  }
});

export default store;
