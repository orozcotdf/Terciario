import React from 'react';
import {Route, DefaultRoute} from 'react-router';
import equivalencias from '../components/Equivalencias/main';
import agregaEquivalencia from '../components/Equivalencias/agrega';
import editarEquivalencia from '../components/Equivalencias/Editar';
import Inicio from '../components/inicio';
import Layout from '../components/layout';
import CursosDeDocente from '../components/AreaDocentes/Cursos/main';
import CargaParcial from '../components/AreaDocentes/Cursos/CargaParcial';

export default (
  <Route name="app" path="/" handler={Layout}>
    <Route name="home" path="/" handler={Inicio}/>
    <Route name="equivalencias" path="/equivalencias" handler={equivalencias}/>
    <Route name="agrega-equivalencias" path="/equivalencias/agrega" handler={agregaEquivalencia}/>
    <Route
      name="editar-equivalencia"
      path="/equivalencias/:id/editar"
      handler={editarEquivalencia}
    />
    <Route name="cursos" path="/area-docentes/cursos" handler={CursosDeDocente}/>
    <Route
      name="CargaParcial"
      path="/area-docentes/cursos/:idCurso/CargaParcial/:parcial"
      handler={CargaParcial}
    />
    <DefaultRoute handler={Inicio} />
  </Route>
);
