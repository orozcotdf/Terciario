import Reflux from 'reflux';
import axios from 'axios';
import actions from '../actions/cursosActions';

const store = Reflux.createStore({

  listenables: [actions],
  data: {
    alumnos: []
  },

  getInitialState() {
    return {
      info: {},
      alumnos: []
    };
  },
  onObtenerInfo(idCurso) {
    axios
      .get(`/api/Cursos/Info/${idCurso}`)
      .then(
        (curso) => {
          this.trigger({info: curso.data});
        }
      );
  },
  onObtenerAlumnos(idCurso, parcial) {
    axios
    .get(
      `/api/Cursos/ObtenerParcial`, {
        params: {
          cursoId: idCurso,
          parcial
        }
      }
    )
    .then(
      (alumnos) => {
        this.trigger({alumnos: alumnos.data});
      }
    );
  }

});

export default store;
