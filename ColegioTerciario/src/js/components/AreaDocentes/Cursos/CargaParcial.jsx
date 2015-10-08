import React from 'react';
import CursosStore from '../../../stores/cursosStore';
import Reflux from 'reflux';
import CursosActions from '../../../actions/cursosActions';
import UISelect from '../../UI/Select';

const CargaParcial = React.createClass({

  parcialesValidos: ['P1', 'P2', 'R1', 'R2'],

  mixins: [Reflux.connect(CursosStore)],

  contextTypes: {
    router: React.PropTypes.func
  },

  componentWillMount() {
    CursosActions.obtenerInfo(this.props.params.idCurso);
    CursosActions.obtenerAlumnos(this.props.params.idCurso, this.props.params.parcial);
  },

  componentWillUpdate(nextProps, nextState) {
    if (nextProps) {
      if (this.parcialesValidos.indexOf(nextProps.params.parcial) < 0) {
        this.context.router.transitionTo('/');
      }
    }
  },

  propTypes() {
    return {
      idCurso: React.PropTypes.integer,
      parcial: React.PropTypes.oneOf(this.parcialesValidos)
    };
  },

  _getAlumnos() {
    return this.state.alumnos;
  },

  render() {
    const notas = [
      {payload: '1', text: '1'},
      {payload: '2', text: '2'},
      {payload: '3', text: '3'},
      {payload: '4', text: '4'},
      {payload: '5', text: '5'},
      {payload: '6', text: '6'},
      {payload: '7', text: '7'},
      {payload: '8', text: '8'},
      {payload: '9', text: '9'},
      {payload: '10', text: '10'},
      {payload: 'Ausente', text: 'Ausente'}
    ];

    return (
      <div>
        <div className="col-sm-6 col-sm-offset-3">
          <div className="block-header">
            <h2>{this.state.info.Carrera} - {this.state.info.Materia}</h2>
          </div>
        </div>
      <div className="col-sm-6 col-sm-offset-3">
        <div className="card card-light">
          <div className="card-header">
            <h2>Notas de {this.props.params.parcial} - Curso: {this.state.info.Nombre}</h2>
          </div>
          <div className="card-body card-padding">
            <table className="table table-striped">
              <thead>
                <tr>
                  <th>Alumno</th>
                  <th>Nota</th>
                </tr>
              </thead>
              <tbody>
                {this._getAlumnos().map(function (alumno) {
                  return (
                    <tr key={alumno.CursadaId}>
                      <td>{alumno.Alumno}</td>
                      <td>
                        <UISelect
                          options={notas}
                          emptyText="Inserte Nota"
                          defaultValue={alumno.Nota}
                        />
                      </td>
                    </tr>
                    );
                })}
              </tbody>
            </table>
          </div>
        </div>
      </div>
      </div>
    );
  }
});

export default CargaParcial;
