import React from 'react';
import Reflux from 'reflux';
import classNames from 'classnames';
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
    $(React.findDOMNode(this.refs.sidebarInner)).niceScroll({
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
      id: 1,
      title: 'Personas',
      url: '/Personas',
      icon: 'zmdi-accounts-list'
    }, {
      id: 2,
      title: 'Cursos',
      url: '/Cursos',
      icon: 'zmdi-calendar-note'
    }, {
      id: 3,
      title: 'Finales',
      url: '/ActaExamen',
      icon: 'zmdi-graduation-cap'
    }, {
      id: 4,
      title: 'Equivalencias',
      url: '/#/equivalencias',
      icon: 'zmdi-view-list'
    }];

    if (User.isInRole('Admin')) {
      items.push({
        id: 5,
        title: 'Usuarios',
        url: '/Admin/Usuarios',
        icon: 'zmdi-account'
      });

      items.push({
        id: 6,
        title: 'Roles',
        url: '/Admin/Roles',
        icon: 'zmdi-accounts'
      });
    }
    return items;
  },

  _toggleProfileMenu(e) {
    e.preventDefault();
    this.setState({
      profileMenuActive: !this.state.profileMenuActive
    });

    $(React.findDOMNode(this.refs.mainmenu)).slideToggle(200);
  },

  _logout(e) {
    e.preventDefault();
    document.getElementById('logoutForm').submit();
  },

  render() {
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
                      <Gravatar email={this.state.user.data.UserName} />
                  </div>

                  <div className="profile-info">
                      {this.state.user.data.UserName}

                      <i className="zmdi zmdi-arrow-drop-down"></i>
                  </div>
              </a>

              <ul className="main-menu" ref="mainmenu">
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
            {this.sidebarItems().map(function (result) {
              const iconClass = `zmdi ${result.icon}`;

              return (
                <li key={result.id}>
                  <a href={result.url}>
                    <i className={iconClass}></i>
                    {result.title}
                  </a>
                </li>
                );
            })}

          </ul>
        </div>
      </aside>
    );
  }
});

export default UISidebar;
