import React from 'react';
import {Router} from 'react-router';
import ReactDOM from 'react-dom';
import injectTapEventPlugin from 'react-tap-event-plugin';
import $ from 'jquery';
import Layout from './components/layout';

require('jquery.nicescroll/jquery.nicescroll');
require('../less/terciario.scss');
require('react-select/dist/react-select.css');

// Needed for onTouchTap
// Can go away when react 1.0 release
// Check this repo:
// https://github.com/zilverline/react-tap-event-plugin
injectTapEventPlugin();

const rootRoute = {
  component: Layout,
  childRoutes: [
    require('./routes/inscripciones')
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
  if (document.getElementById('publicContainer')) {
    ReactDOM.render(<Router routes={rootRoute}/>, document.getElementById('publicContainer'));
  }
}

if (window.addEventListener) {
  window.addEventListener('DOMContentLoaded', run);
} else {
  window.attachEvent('onload', run);
}
