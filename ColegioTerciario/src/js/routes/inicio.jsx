module.exports = {
  path: '/',

  getComponent(location, cb) {
    cb(null, require('../components/Inicio/Inicio'));
  }
};
