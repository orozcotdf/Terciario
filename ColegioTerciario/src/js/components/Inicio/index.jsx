export default {
  path: '/',

  /*
  getChildRoutes(location, cb) {
    require.ensure([], (require) => {
      cb(null, [
        require('./routes/Announcements'),
        require('./routes/Assignments'),
        require('./routes/Grades')
      ])
    })
  },
  */

  getComponent(location, cb) {
    // require.ensure([], (require) => {
    //  cb(null, require('./main'));
    // });

    cb(null, require('./Inicio'));
  }
};
