import Layout from '../components/layout';

module.exports = {
  component: Layout,
  childRoutes: [
    require('./inicio'),
    require('./perfil'),
    require('./equivalencias'),
    require('./areaDocentes'),
    require('./inscripcionesAdmin')
  ]
};
