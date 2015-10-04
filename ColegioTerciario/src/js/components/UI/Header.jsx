import React from 'react';
import Component from '../Component/main';
import UIDropdown from './Dropdown';

export default class UISidebar extends Component {

  logout() {
    document.getElementById('logoutForm').submit();
  }

  render() {
    const logoutStyles = {
      cursor: 'pointer'
    };

    return (
      <div className="header">
        <div className="pull-left full-height visible-sm visible-xs">
          { /* START ACTION BAR */ }
          <div className="sm-action-bar">
            <a href="#" className="btn-link toggle-sidebar" data-toggle="sidebar">
              <span className="icon-set menu-hambuger"></span>
            </a>
          </div>
        { /* END ACTION BAR */ }
        </div>

        <div className="pull-right full-height visible-sm visible-xs">
          { /* START ACTION BAR  */}
          <div className="sm-action-bar">
            <a href="#" className="btn-link"
              data-toggle="quickview" data-toggle-element="#quickview">
              <span className="icon-set menu-hambuger-plus"></span>
            </a>
          </div>
          { /* END ACTION BAR */ }
        </div>
        { /* END MOBILE CONTROLS */ }
        <div className=" pull-left sm-table">
          <div className="header-inner">
            <div className="brand inline">
              <img src="/img/LogoCENT_60x180_Transparente.png" alt="Cent11" width="78" />
            </div>

          </div>
        </div>

        <div className=" pull-right">
          { /* START User Info */ }
          <div className="visible-lg visible-md m-t-10">
            <div className="pull-left p-r-10 p-t-10 fs-16 font-heading">
              <span className="text-master">{this.state.user.data.UserName}</span>
            </div>
            <UIDropdown user={this.state.user}>
              <li>
                <a href="/Manage"><i className="pg-settings_small"></i> Preferencias</a>
              </li>
              { /* <li>
                <a href="#"><i className="pg-outdent"></i> Feedback</a>
              </li>
              <li>
                <a href="#"><i className="pg-signals"></i> Help</a>
              </li> */ }
              <li className="bg-master-lighter">
                <a className="clearfix" onClick={this.logout.bind(this)} style={logoutStyles}>
                  <span className="pull-left">Salir</span>
                  <span className="pull-right"><i className="pg-power"></i></span>
                </a>
              </li>
            </UIDropdown>
          </div>
          { /* END User Info */ }
        </div>
      </div>
    );
  }
}
