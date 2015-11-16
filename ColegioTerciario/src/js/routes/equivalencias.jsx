import Notification from 'Notification';

module.exports = {
  path: '/equivalencias',

  onEnter(nextState, replaceState) {
    if (!User.isInRole('Bedel')) {
      Notification.error('No tiene permisos');
      replaceState({nextPathname: nextState.location.pathname}, '/');
    }
  },

  getChildRoutes(location, cb) {
    require.ensure([], (require) => {
      cb(null, [
        require('./equivalenciasAgrega'),
        require('./equivalenciasEdita')
      ]);
    });
  },

  getComponent(location, cb) {
    // require.ensure([], (require) => {
    //  cb(null, require('./main'));
    // });
    require.ensure([], (require) => {
      cb(null, require('../components/Equivalencias/container'));
    });
  }
};
