module.exports = {
  path: '/alumnos',

  getComponent(location, cb) {
    cb(null, require('../components/Alumnos'));
  }
};
