module.exports = {
  path: '/finales',

  getComponent(location, cb) {
    cb(null, require('../components/Finales'));
  }
};
