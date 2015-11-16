import Reflux from 'reflux';
import actions from '../actions/adminInscripcionesActions';
import axios from 'axios';

const store = Reflux.createStore({
  listenables: [actions],
  data: {
    inscripciones: []
  },

  getInitialState() {
    return this.data;
  },

  onGetInscripciones(cb) {
    axios
    .get(
      `/api/Inscripciones/GetInscripciones`
    )
    .then(
      (response) => {
        this.data.inscripciones = response.data;
        this.trigger({inscripciones: this.data.inscripciones});
        cb();
      }
    );
  }
});

export default store;
