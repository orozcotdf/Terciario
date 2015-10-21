import React from 'react';
import PerfilActions from './actions/perfilActions';
import Reflux from 'reflux';
import PerfilStore from './stores/PerfilStore';
import Notification from '../UI/Notification';

const PerfilForm = React.createClass({
  mixins: [Reflux.connect(PerfilStore)],

  propTypes: {
    toggleEdit: React.PropTypes.func.isRequired
  },

  _onChange(e) {
    const nextState = {};

    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  },

  _save() {
    Notification.showLoading();

    PerfilActions.guardarDatosPersonales(this.state, (message) => {
      Notification.success(message, true);
      this.props.toggleEdit();
    });
  },

  render() {
    return (

        <div className="pmbb-body p-l-30">
          <div className="pmbb-view">
            <dl className="dl-horizontal">
              <dt>Telefono</dt>
              <dd>{this.state.PERSONA_TELEFONO}</dd>
            </dl>
            <dl className="dl-horizontal">
              <dt>Email</dt>
              <dd>{this.state.PERSONA_EMAIL}</dd>
            </dl>
          </div>

          <div className="pmbb-edit">
            <dl className="dl-horizontal">
              <dt className="p-t-10">Telefono</dt>
              <dd>
                <div className="fg-line">
                  <input type="text"
                    name="PERSONA_TELEFONO"
                    className="form-control"
                    onChange={this._onChange}
                    placeholder={this.state.PERSONA_TELEFONO}
                    value={this.state.PERSONA_TELEFONO}
                  />
                </div>
              </dd>
            </dl>
            <dl className="dl-horizontal">
              <dt className="p-t-10">Email</dt>
              <dd>
                <div className="fg-line">
                  <input type="text"
                    name="PERSONA_EMAIL"
                    className="form-control"
                    onChange={this._onChange}
                    placeholder={this.state.PERSONA_EMAIL}
                    value={this.state.PERSONA_EMAIL}
                  />
                </div>
              </dd>
            </dl>

            <div className="m-t-30">
              <button onClick={this._save} className="btn btn-primary btn-sm waves-effect">
                Guardar
              </button>
              <button onClick={this.props.toggleEdit} className="btn btn-link btn-sm waves-effect">
                Cancelar
              </button>
            </div>
          </div>
        </div>

    );
  }
});

export default PerfilForm;
