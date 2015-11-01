import Notification from 'Notification';

export default {
  path: '/area-docentes/cursos',

  onEnter(nextState, replaceState) {
    if (!User.isInRole('Docente')) {
      Notification.error('No tiene permisos');
      replaceState({nextPathname: nextState.location.pathname}, '/');
    }
  },

  getChildRoutes(location, cb) {
    // require.ensure([], (require) => {
    cb(null, [
      require('./CargaParcial')
    ]);
    // })
  },


  getComponent(location, cb) {
    // require.ensure([], (require) => {
    //  cb(null, require('./main'));
    // });

    cb(null, require('./container'));
  }
};
