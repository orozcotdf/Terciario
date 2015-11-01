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
            La presente solicitud deberá ser acompañada por:
            A) Fotocopia autenticada del título secundario.
            B) Fotocopia del documento de identidad. C) 2 (dos) foto carnet.
            De acogerse el postulante a las prescripciones del Art. 7° del a Ley Nacional 24521
            por no haber cumplimentado sus estudios de nivel medio, deberá presentar sin
            excepción las constancias pertinentes que acrediten los estudios cursados,
            las correspondientes que acrediten la experiencia y/o idoneidad que se corresponda
            con la carrera a la cual se inscribe, y rendir oportunamente la evaluación establecida
            en la normativa de mención. La presente solicitud reviste el carácter de condicional
            y se encuentra sujeta a la autorización del Ministerio de Educación para la apertura
            de los respectivos ciclos.
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
