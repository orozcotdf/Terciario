import Notification from 'Notification';

module.exports = {
  path: '/area-docentes/cursos',

  onEnter(nextState, replaceState) {
    if (!User.isInRole('Docente')) {
      Notification.error('No tiene permisos');
      replaceState({nextPathname: nextState.location.pathname}, '/');
    }
  },

  getChildRoutes(location, cb) {
    require.ensure([], (require) => {
      cb(null, [
        require('./areaDocentesCargaParcial')
      ]);
    });
  },


  getComponent(location, cb) {
    // require.ensure([], (require) => {
    //  cb(null, require('./main'));
    // });
    require.ensure([], (require) => {
      cb(null, require('../components/AreaDocentes/index'));
    });
  }
};
