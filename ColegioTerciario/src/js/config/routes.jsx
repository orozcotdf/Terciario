import React from 'react'
import router from 'react-router';
import equivalencias from '../components/Equivalencias/main';
import agregaEquivalencia from '../components/Equivalencias/agrega';
import editarEquivalencia from '../components/Equivalencias/Editar';
import Inicio from '../components/inicio';
import Layout from '../components/layout';
var DefaultRoute = router.DefaultRoute;
var Route = router.Route;

export default (
  <Route name="app" path="/" handler={Layout}>
    <Route name="home" path="/" handler={Inicio}/>
    <Route name="equivalencias" handler={equivalencias}/>
    <Route name="agrega-equivalencias" path="/equivalencias/agrega" handler={agregaEquivalencia}/>
    <Route name="editar-equivalencia" path="/equivalencias/:id/editar" handler={editarEquivalencia}/>

    <DefaultRoute handler={equivalencias} />
  </Route>
);
//<Route name="images" path="/images" handler={require('react-router-proxy?name=ImageGrid!../components/ImageGrid/ImageGrid.js')}/>
