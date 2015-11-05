import React from 'react';
import {Router} from 'react-router';
import ReactDOM from 'react-dom';
import injectTapEventPlugin from 'react-tap-event-plugin';
import UISidebar from './components/UI/Sidebar';
import UIHeader from './components/UI/Header';
import $ from 'jquery';

require('jquery.nicescroll/jquery.nicescroll');
require('../less/terciario.less');
require('react-select/dist/default.css');

// Needed for onTouchTap
// Can go away when react 1.0 release
// Check this repo:
// https://github.com/zilverline/react-tap-event-plugin
injectTapEventPlugin();

const rootRoute = {
  component: require('./components/layout'),
  childRoutes: [
    require('./components/Inicio'),
    require('./components/Perfil'),
    require('./components/Equivalencias'),
    require('./components/AreaDocentes/Cursos'),
    require('./components/Inscripciones'),
    require('./components/AdminInscripciones')
  ]
};

function run() {
  $('html').niceScroll({
    cursorcolor: 'rgba(0,0,0,0.3)',
    cursorborder: 0,
    cursorborderradius: 0,
    cursorwidth: '5px',
    bouncescroll: true,
    mousescrollstep: 100
    // autohidemode: false
  });

  // Router.render(routes, (Root, state) => {
  if (document.getElementById('appContainer')) {
    ReactDOM.render(<Router routes={rootRoute}/>, document.getElementById('appContainer'));
  }

  if (document.getElementById('sidebarComponent')) {
    ReactDOM.render(<UISidebar/>, document.getElementById('sidebarComponent'));
  }

  ReactDOM.render(<UIHeader title="Cent11"/>, document.getElementById('headerComponent'));
  // });
}


if (window.addEventListener) {
  window.addEventListener('DOMContentLoaded', run);
} else {
  window.attachEvent('onload', run);
}
