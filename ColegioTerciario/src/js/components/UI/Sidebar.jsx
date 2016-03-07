import React from 'react';
import ReactDOM from 'react-dom';
import Reflux from 'reflux';
import classNames from 'classnames';
import NavigationActions from '../../actions/navigationActions';
import NavigationStore from '../../stores/navigationStore';
import UserStore from '../../stores/userStore';
import Gravatar from 'react-gravatar';
import $ from 'jquery';

const UISidebar = React.createClass({
  getInitialState() {
    return {
      profileMenuActive: false
    };
  },

  mixins: [
    Reflux.connect(NavigationStore, 'navigation'),
    Reflux.connect(UserStore)
  ],

  componentDidMount() {
    $(ReactDOM.findDOMNode(this.refs.sidebarInner)).niceScroll({
      cursorcolor: 'rgba(0,0,0,0.5)',
      cursorborder: 0,
      cursorborderradius: 0,
      cursorwidth: '5px',
      bouncescroll: true,
      mousescrollstep: 100
      // autohidemode: false
    });
  },
  sidebarItems() {
    const items = [{
      title: 'Alumnos',
      url: '/#/alumnos',
      icon: 'zmdi-accounts-list',
      role: 'Bedel'
    }, {
      title: 'Docentes y Bedeles',
      url: '/Personas',
      icon: 'zmdi-accounts-list',
      role: 'Bedel'
    }, {
      title: 'Cursos',
      url: '/Cursos',
      icon: 'zmdi-calendar-note',
      role: 'Bedel'
    }, {
      title: 'Finales',
      url: '/#/finales',
      icon: 'zmdi-graduation-cap',
      role: 'Bedel'
    }, {
      title: 'Equivalencias',
      url: '/#/equivalencias',
      icon: 'zmdi-view-list',
      role: 'Bedel'
    }, {
      title: 'Inscripcion a Finales',
      url: '/inscribiralumnos',
      icon: 'zmdi-view-list',
      role: 'Bedel'
    }, {
      title: 'Inscripcion a Cursada',
      url: '/#/inscripciones',
      icon: 'zmdi-view-list',
      role: 'Bedel'
    }];

    if (User.isInRole('Admin')) {
      items.push({
        title: 'Usuarios',
        url: '/Admin/Usuarios',
        icon: 'zmdi-account'
      });

      items.push({
        title: 'Roles',
        url: '/Admin/Roles',
        icon: 'zmdi-accounts'
      });
    }

    for (var index = 0; index < items.length; index++) {
      items[index].id = index;
    }

    return items;
  },

  _toggleProfileMenu(e) {
    e.preventDefault();
    this.setState({
      profileMenuActive: !this.state.profileMenuActive
    });

    $(ReactDOM.findDOMNode(this.refs.mainmenu)).slideToggle(200);
  },

  _toggleSidebar() {
    NavigationActions.toggleSidebar();
  },

  _logout(e) {
    e.preventDefault();
    document.getElementById('logoutForm').submit();
  },

  render() {
    const sidebarItemID = 0;
    const classes = classNames({
      toggled: this.state.navigation.sidebarActive
    });
    const profileMenuClasses = classNames({
      'profile-menu': true,
      toggled: this.state.profileMenuActive
    });

    return (
      <aside id="sidebar" className={classes}>
        <div className="sidebar-inner c-overflow" ref="sidebarInner">
          <div className={profileMenuClasses}>
              <a href="#" onClick={this._toggleProfileMenu}>
                <div className="profile-pic">
                  <Gravatar email={this.state.user.data.DatosPersonales.PERSONA_EMAIL} />
                </div>

                <div className="profile-info">
                  {this.state.user.data.UserName}

                  <i className="zmdi zmdi-arrow-drop-down"></i>
                </div>
              </a>

              <ul className="main-menu" ref="mainmenu">
                <li>
                    <a href="/#/Perfil">
                      <i className="zmdi zmdi-account"></i> Ver Perfil
                    </a>
                </li>
                <li>
                  <a href="/Manage"><i className="zmdi zmdi-settings"></i> Preferencias</a>
                </li>
                <li>
                  <a href="#" onClick={this._logout}>
                    <i className="zmdi zmdi-time-restore"></i>
                    Salir
                  </a>
                </li>
              </ul>
          </div>
          <ul className="main-menu">
            {this.sidebarItems().map((result) => {
              const iconClass = `zmdi ${result.icon}`;
              if (this.state.user.isInRole(result.role)) {
                return (
                  <li key={result.id}>
                    <a href={result.url} onClick={this._toggleSidebar}>
                      <i className={iconClass}></i>
                      {result.title}
                    </a>
                  </li>
                );
              };

            })}

          </ul>
        </div>
      </aside>
    );
  }
});

export default UISidebar;
