import React from 'react';
import {Checkbox, RaisedButton} from 'material-ui';

const Paso6Component = React.createClass({
  propTypes: {
    onSave: React.PropTypes.func.isRequired,
    height: React.PropTypes.string,
    width: React.PropTypes.string
  },
  getInitialState() {
    return {
      reglamentoAceptado: false
    };
  },
  getDefaultProps() {
    return {
      height: '100%',
      width: '100%'
    };
  },
  _onCheck(event, checked) {
    this.setState({reglamentoAceptado: checked});
  },
  _onClickInscribirse() {
    this.props.onSave();
  },
  render() {
    return (
      <div>
        <div className="row">
          <div className="col-md-12">
            <iframe ref="iframe"
              frameBorder="0"
              src="/Content/RAI.html"
              style={{height: this.props.height, width: this.props.width}}
              height={this.props.height}
              width={this.props.width}>
            </iframe>
          </div>
        </div>
        <div className="row">
          <div className="col-md-12">
            <p>
              La presente solicitud reviste el carácter de condicional y se encuentra sujeta a la
              autorización del Ministerio de Educación para la apertura de los respectivos ciclos.
            </p>
          </div>
        </div>
        <div className="row m-t-20">
          <div className="col-md-6 pull-right">
            <RaisedButton
              label="Inscribirse"
              secondary={true}
              disabled={!this.state.reglamentoAceptado}
              onClick={this._onClickInscribirse}
              style={{float: 'right'}}
            />
            <Checkbox
              name="reglamentoAceptado"
              value="reglamentoAceptado"
              label="HE LEIDO Y ACEPTO EL REGLAMENTO"
              onCheck={this._onCheck}
              style={{width: '290px', float: 'right', marginTop: '6px'}}
            />
          </div>
        </div>
      </div>
    );
  }
});

export default Paso6Component;
