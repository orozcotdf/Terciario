module.exports = {
  path: '/inscripciones',

  getComponent(location, cb) {
    // require.ensure([], (require) => {
    //  cb(null, require('./main'));
    // });
    cb(null, require('../components/InscripcionesAdmin/container'));
  }
};
