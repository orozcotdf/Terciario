import React from 'react';
import {Route, DefaultRoute} from 'react-router';
import equivalencias from '../components/Equivalencias/main';
import agregaEquivalencia from '../components/Equivalencias/agrega';
import editarEquivalencia from '../components/Equivalencias/Editar';
import Inicio from '../components/inicio';
import Layout from '../components/layout';
import CursosDeDocente from '../components/AreaDocentes/Cursos/main';
import CargaParcial from '../components/AreaDocentes/Cursos/CargaParcial';
import Perfil from '../components/Perfil/Perfil';

/*
function requireAuth(nextState, replaceState) {
  if (!auth.loggedIn()) {
    replaceState({nextPathname: nextState.location.pathname}, '/login');
  }
}
*/

export default (
  <Route name="app" path="/" component={Layout}>
    <Route name="home" path="/" component={Inicio}/>
    <Route name="perfil" path="/Perfil" component={Perfil}/>
    <Route name="equivalencias" path="/equivalencias" component={equivalencias}/>
    <Route name="agrega-equivalencias" path="/equivalencias/agrega" component={agregaEquivalencia}/>
    <Route
      name="editar-equivalencia"
      path="/equivalencias/:id/editar"
      component={editarEquivalencia}
    />
    <Route name="cursos" path="/area-docentes/cursos" component={CursosDeDocente}/>
    <Route
      name="CargaParcial"
      path="/area-docentes/cursos/:idCurso/CargaParcial/:parcial"
      component={CargaParcial}
    />
    <DefaultRoute component={Inicio} />
  </Route>
);
