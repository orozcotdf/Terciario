import React from 'react';
import PerfilForm from './perfilForm';
import Reflux from 'reflux';
import PerfilStore from './stores/PerfilStore';
import classNames from 'classnames';

const Perfil = React.createClass({
  mixins: [Reflux.connect(PerfilStore)],

  _toggleEdit(e) {
    if (e) {
      e.preventDefault();
    }
    this.setState({personalDataEditToggled: !this.state.personalDataEditToggled});
  },

  render() {
    const personalDataBlockClasses = classNames({
      'pmb-block': true,
      toggled: this.state.personalDataEditToggled
    });

    return (
      <div>
        <div className="block-header">
          <h2>
            {this.state.PERSONA_APELLIDO},
             {' ' + this.state.PERSONA_NOMBRE}
          </h2>
          {/*
          <ul className="actions m-t-20 hidden-xs">
            <li className="dropdown">
              <a href="" data-toggle="dropdown">
                <i className="zmdi zmdi-more-vert"></i>
              </a>
              <ul className="dropdown-menu dropdown-menu-right">
                <li>
                  <a href="">Privacy Settings</a>
                </li>
                <li>
                  <a href="">Account Settings</a>
                </li>
                <li>
                  <a href="">Other Settings</a>
                </li>
              </ul>
            </li>
          </ul>
          */}
        </div>
        <div className="card" id="profile-main">
          <div className="pm-overview c-overflow">
            <div className="pmo-block pmo-contact hidden-xs">
              <h2>Contacto</h2>
              <ul>
                <li>
                  <i className="zmdi zmdi-phone"></i>
                  {this.state.PERSONA_TELEFONO}
                </li>
                <li>
                  <i className="zmdi zmdi-email"></i>
                  {this.state.PERSONA_EMAIL}
                </li>
                {/* <li>
                  <i className="zmdi zmdi-pin"></i>
                  <address className="m-b-0">
                      10098 ABC Towers, <br/>
                      Dubai Silicon Oasis, Dubai, <br/>
                      United Arab Emirates
                  </address>
                </li>*/}
              </ul>
            </div>
          </div>
          <div className="pm-body clearfix">
            <div className={personalDataBlockClasses}>
              <div className="pmbb-header">
                <h2><i className="zmdi zmdi-equalizer m-r-5"></i> Informacion Personal</h2>

                  <ul className="actions">
                    <li className="dropdown">
                      <a href="" data-toggle="dropdown">
                        <i className="zmdi zmdi-more-vert"></i>
                      </a>

                      <ul className="dropdown-menu dropdown-menu-right">
                        <li>
                            <a onClick={this._toggleEdit} href="#">Edit</a>
                        </li>
                      </ul>
                    </li>
                  </ul>
              </div>
              <PerfilForm toggleEdit={this._toggleEdit}/>
            </div>
          </div>
        </div>
      </div>
    );
  }
});

export default Perfil;
