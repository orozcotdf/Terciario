import React from 'react';
import NavigationStore from '../../stores/navigationStore';
import Reflux from 'reflux';
import classNames from 'classnames';
import NavigationActions from '../../actions/navigationActions';

const UIHeader = React.createClass({

  mixins: [Reflux.connect(NavigationStore, 'navigation')],

  propTypes() {
    return {
      title: React.PropTypes.string
    };
  },

  logout() {
    document.getElementById('logoutForm').submit();
  },

  _toggleSidebar() {
    this.setState({
      sidebarActive: false // !this.state.sidebarActive
    });

    /* $('body').toggleClass('modal-open');
    $('#header').toggleClass('sidebar-toggled');
    $('#sidebar').toggleClass('toggled');*/
  },

  _launchIntoFullscreen(e) {
    e.preventDefault();
    const element = document.documentElement;

    if (element.requestFullscreen) {
      element.requestFullscreen();
    } else if (element.mozRequestFullScreen) {
      element.mozRequestFullScreen();
    } else if (element.webkitRequestFullscreen) {
      element.webkitRequestFullscreen();
    } else if (element.msRequestFullscreen) {
      element.msRequestFullscreen();
    }
  },

  _exitFullscreen(e) {
    if (document.exitFullscreen) {
      document.exitFullscreen();
    } else if (document.mozCancelFullScreen) {
      document.mozCancelFullScreen();
    } else if (document.webkitExitFullscreen) {
      document.webkitExitFullscreen();
    }
  },

  render() {
    const classes = classNames({
      'sidebar-toggled': this.state.navigation.sidebarActive
    });


    return (
      <header id="header" className={classes}>
        <ul className="header-inner">
          <li id="menu-trigger" data-trigger="#sidebar" onClick={NavigationActions.toggleSidebar}>
              <div className="line-wrap">
                  <div className="line top"></div>
                  <div className="line center"></div>
                  <div className="line bottom"></div>
              </div>
          </li>
          <li className="logo hidden-xs">
              <a href="/#/">{this.props.title}</a>
          </li>
          <li className="pull-right">
            <ul className="top-menu">
              {/* <li id="toggle-width">
                <div className="toggle-switch">
                  <input id="tw-switch" type="checkbox" hidden="hidden" />
                  <label htmlFor="tw-switch" className="ts-helper"></label>
                </div>
              </li>

              <li id="top-search">
                <a className="tm-search" href=""></a>
              </li>
              */}
              <li className="dropdown">
                <a data-toggle="dropdown" className="tm-settings" href=""></a>
                <ul className="dropdown-menu dm-icon pull-right">
                  <li className="hidden-xs">
                    <a data-action="fullscreen" href="" onClick={this._launchIntoFullscreen}>
                      <i className="zmdi zmdi-fullscreen"></i>
                      Pantalla Completa
                    </a>
                  </li>
                </ul>
              </li>
            </ul>
          </li>

        </ul>

        <div id="top-search-wrap">
          <input type="text"/>
          <i id="top-search-close">&times;</i>
        </div>
      </header>
    );
  }
});

export default UIHeader;
