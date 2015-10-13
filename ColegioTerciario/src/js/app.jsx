import React from 'react';
import Router from 'react-router';
import routes from './core/routes';
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

  Router.run(routes, (Root, state) => {
    if (document.getElementById('appContainer')) {
      React.render(<Root {...state}/>, document.getElementById('appContainer'));
    }

    if (document.getElementById('sidebarComponent')) {
      React.render(<UISidebar {...state}/>, document.getElementById('sidebarComponent'));
    }

    React.render(<UIHeader {...state} title="Cent11"/>, document.getElementById('headerComponent'));
  });
}


if (window.addEventListener) {
  window.addEventListener('DOMContentLoaded', run);
} else {
  window.attachEvent('onload', run);
}

