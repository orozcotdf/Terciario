import React from 'react';
import {Router} from 'react-router';
import ReactDOM from 'react-dom';
import injectTapEventPlugin from 'react-tap-event-plugin';
import UISidebar from './components/UI/Sidebar';
import UIHeader from './components/UI/Header';
import $ from 'jquery';
import routes from './routes';
import { createStore, combineReducers, bindActionCreators } from 'redux';
// import Root from './containers/Root';
// import configureStore from './store/configureStore';

require('jquery.nicescroll/jquery.nicescroll');
require('../less/terciario.scss');
require('react-select/dist/default.css');

injectTapEventPlugin();

function run() {
  $('html').niceScroll({
    cursorcolor: 'rgba(0,0,0,0.3)',
    cursorborder: 0,
    cursorborderradius: 0,
    cursorwidth: '5px',
    bouncescroll: true,
    mousescrollstep: 100
  });

  const target = document.getElementById('appContainer');

  // Router.render(routes, (Root, state) => {
  if (target) {
    ReactDOM.render(<Router routes={routes}/>, target);

    /*
    const store = configureStore(window.__INITIAL_STATE__, __DEBUG__);

    const node = (
      <Root store={store}
            debug={__DEBUG__}
            debugExternal={__DEBUG_NW__} />
    );

    ReactDOM.render(node, target);
    */
  }

  if (document.getElementById('sidebarComponent')) {
    ReactDOM.render(<UISidebar/>, document.getElementById('sidebarComponent'));
  }

  ReactDOM.render(<UIHeader title="Cent11"/>, document.getElementById('headerComponent'));
}

if (window.addEventListener) {
  window.addEventListener('DOMContentLoaded', run);
} else {
  window.attachEvent('onload', run);
}
