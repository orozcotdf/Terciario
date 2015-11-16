module.exports = {
  path: 'agrega',

  getComponent(location, cb) {
    // require.ensure([], (require) => {
    //  cb(null, require('./main'));
    // });

    cb(null, require('../components/Equivalencias/Agrega'));
  }
};
