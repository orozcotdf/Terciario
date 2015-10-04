import React from 'react';
import Router from 'react-router';
import routes from './core/routes';
import injectTapEventPlugin from 'react-tap-event-plugin';
import UISidebar from './components/UI/Sidebar';
import UIHeader from './components/UI/Header';

require('../less/terciario.less');
require('react-select/dist/default.css');

// Needed for onTouchTap
// Can go away when react 1.0 release
// Check this repo:
// https://github.com/zilverline/react-tap-event-plugin
injectTapEventPlugin();

function run() {
  Router.run(routes, (Root, state) => {
    if (document.getElementById('appContainer')) {
      React.render(<Root {...state}/>, document.getElementById('appContainer'));
    }

    React.render(<UISidebar {...state}/>, document.getElementById('UISidebar'));
    React.render(<UIHeader {...state}/>, document.getElementById('UIHeader'));
  });
}


if (window.addEventListener) {
  window.addEventListener('DOMContentLoaded', run);
} else {
  window.attachEvent('onload', run);
}

