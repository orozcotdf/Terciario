import React from 'react';
import {Route, DefaultRoute} from 'react-router';
import equivalencias from '../components/Equivalencias/main';
import agregaEquivalencia from '../components/Equivalencias/agrega';
import editarEquivalencia from '../components/Equivalencias/Editar';
import Inicio from '../components/inicio';
import Layout from '../components/layout';
import CursosDeDocente from '../components/AreaDocentes/Cursos/main';

export default (
  <Route name="app" path="/" handler={Layout}>
    <Route name="home" path="/" handler={Inicio}/>
    <Route name="equivalencias" handler={equivalencias}/>
    <Route name="agrega-equivalencias" path="/equivalencias/agrega"
      handler={agregaEquivalencia}/>
    <Route name="editar-equivalencia" path="/equivalencias/:id/editar"
      handler={editarEquivalencia}/>

    <Route name="AreaDocentes" path="area-docentes">
      <Route name="cursos" path="cursos" handler={CursosDeDocente} />
    </Route>
    <DefaultRoute handler={equivalencias} />
  </Route>
);
