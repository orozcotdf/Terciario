import Reflux from 'reflux';
import axios from 'axios';
import actions from '../actions/cursosActions';
import Notification from '../components/UI/Notification';

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
  },
  onCambiarNota(nota, curso, parcial) {
    Notification.showLoading();
    axios({
      method: 'post',
      url: `/api/Cursos/PonerNotaEnParcial/${curso}`,
      data: {
        value: nota,
        name: parcial
      }
    }).then((resultado) => {
      Notification.success('Nota guardada correctamente', true);
        // this.trigger(NotificationActions.setMessage('BIEN'));
        // this.trigger({notaActualizada: true});
    }).catch((response) => {
      Notification.error('Error al guardar nota');
    });
  }

});

export default store;
