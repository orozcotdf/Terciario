import Reflux from 'reflux';
import axios from 'axios';
import actions from '../actions/cursosActions';
import Notification from 'Notification';
import _ from 'lodash';

const store = Reflux.createStore({

  listenables: [actions],
  data: {
    alumnos: []
  },

  getInitialState() {
    return {
      alumnos: []
    };
  },
  onObtenerInfo(idCurso, instancia) {
    axios
      .get(`/api/Cursos/Info/${idCurso}?instancia=${instancia}`)
      .then(
        (curso) => {
          const data = curso.data;

          if (data.Fecha !== null) {
            data.Fecha = new Date(data.Fecha);
          }
          this.trigger(data);
        }
      );
  },
  onObtenerAlumnos(idCurso, parcial, cb) {
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
        this.data.alumnos = alumnos.data;
        this.trigger({alumnos: this.data.alumnos});
        cb();
      }
    );
  },
  onCambiarNota(nota, curso, parcial, cb) {
    Notification.showLoading();
    axios({
      method: 'post',
      url: `/api/Cursos/PonerNotaEnParcial/${curso}`,
      data: {
        value: nota,
        name: parcial
      }
    }).then((resultado) => {
      const index = _.findIndex(this.data.alumnos, {CursadaId: curso});

      this.data.alumnos[index].Nota = nota;
      Notification.success('Nota guardada correctamente', true);
      this.trigger({alumnos: this.data.alumnos});
      cb();
      // this.trigger(NotificationActions.setMessage('BIEN'));
      // this.trigger({notaActualizada: true});
    }).catch((response) => {
      Notification.error('Error al guardar nota');
    });
  },
  onCambiarFecha(id, instancia, fecha) {
    Notification.showLoading();
    axios({
      method: 'post',
      url: `api/Cursos/SetearFecha`,
      data: {
        pk: id,
        name: instancia,
        value: fecha
      }
    }).then((resultado) => {
      Notification.success('Fecha guardada correctamente', true);
    }).catch((response) => {
      Notification.error('Error al guardar fecha');
    });
  }

});

export default store;
