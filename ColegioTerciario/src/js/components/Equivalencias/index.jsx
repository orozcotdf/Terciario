import Notification from 'Notification';

export default {
  path: '/equivalencias',

  onEnter(nextState, replaceState) {
    if (!User.isInRole('Bedel')) {
      Notification.error('No tiene permisos');
      replaceState({nextPathname: nextState.location.pathname}, '/');
    }
  },

  getChildRoutes(location, cb) {
    // require.ensure([], (require) => {
    cb(null, [
      require('./Agrega'),
      require('./Editar')
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
