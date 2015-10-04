import React from 'react';
import $ from 'jquery';
import Component from '../Component/main';

export default class UISidebar extends Component {

  constructor(props) {
    super(props);
    this.state = {
      sidebarActive: false
    };
  }

  sidebarMouseEnter(e) {
    if (!this.state.sidebarPinned) {
      this.setState({
        sidebarActive: true
      });
      $('body').addClass('sidebar-visible');
    }
  }

  sidebarMouseLeave(e) {
    if (!this.state.sidebarPinned) {
      this.setState({
        sidebarActive: false
      });
      $('body').removeClass('sidebar-visible');
    }
  }

  togglePin() {
    this.setState({
      sidebarPinned: !this.state.sidebarPinned
    });

    $('body').toggleClass('menu-pin');
  }

  sidebarItems() {
    const items = [{
      title: 'Personas',
      url: '/Personas',
      icon: 'fa fa-user'
    }, {
      title: 'Cursos',
      url: '/Cursos',
      icon: 'fa fa-graduation-cap'
    }, {
      title: 'Finales',
      url: '/ActaExamen',
      icon: 'pg-calender'
    }, {
      title: 'Equivalencias',
      url: '/#/equivalencias',
      icon: 'fa fa-columns'
    }];

    if (User.isInRole('Admin')) {
      items.push({
        title: 'Usuarios',
        url: '/Admin/Usuarios',
        icon: 'fa fa-user'
      });

      items.push({
        title: 'Roles',
        url: '/Admin/Roles',
        icon: 'fa fa-users'
      });
    }
    return items;
  }

  render() {
    let headerControlsStyles;
    let firstItem = true;
    const activeSidebarStyles = {
      transform: 'translate3d(210px, 0,0)'
    };

    if (this.state.sidebarActive) {
      headerControlsStyles = {
        float: 'right',
        marginRight: '50px'
      };
    }

    if (this.state.sidebarPinned) {
      headerControlsStyles.marginRight = '20px';
    }

    return (
      <nav className="page-sidebar"
        style={(this.state.sidebarActive) ? activeSidebarStyles : {} }
        onMouseEnter={this.sidebarMouseEnter.bind(this)}
        onMouseLeave={this.sidebarMouseLeave.bind(this)}>

        <div id="appMenu" className="sidebar-overlay-slide from-top">
        </div>
        <div className="sidebar-header">
          <a href="/"><img src="/img/LogoCENT_60x180_Transparente.png" alt="Cent11" width="93" /></a>
          <div className="sidebar-header-controls" style={headerControlsStyles}>
            <button data-toggle-pin="sidebar" onClick={this.togglePin.bind(this)}
              className="btn btn-link visible-lg-inline" type="button">
              <i className="fa fs-12"></i>
            </button>
          </div>
        </div>
        <div className="sidebar-menu">
          <ul className="menu-items">

            {this.sidebarItems().map(function (result) {
              let itemClass;

              if (firstItem === true) {
                itemClass = 'm-t-30';
                firstItem = false;
              } else {
                itemClass = null;
              }
              return (
                <li className={itemClass}>
                  <a href={result.url} className="detailed">
                    <span className="title">{result.title}</span>
                  </a>
                  <span className="icon-thumbnail "><i className={result.icon}></i>
                  </span>
                </li>
                );
            })}

          </ul>
          <div className="clearfix"></div>
        </div>

      </nav>
    );
  }
}
