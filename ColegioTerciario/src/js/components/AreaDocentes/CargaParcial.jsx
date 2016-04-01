import React from 'react';
import CursosStore from '../../stores/cursosStore';
import UserStore from '../../stores/userStore';
import Reflux from 'reflux';
import CursosActions from '../../actions/cursosActions';
import UISelect from '../UI/Select';
import _ from 'lodash';
import Notification from 'Notification';
import {RadioButtonGroup, RadioButton} from 'material-ui';
import EasyPieChart from 'easy-pie-chart/dist/easypiechart.js';
import reactMixin from 'react-mixin';
import DateInput from '../UI/DateInput';

const ParcialesValidos = ['P1', 'P2', 'R1', 'R2'];

class CargaParcial extends React.Component {

  constructor() {
    super();
    this.chart = null;
    this.state = {
      verTodos: true,
      verLibres: false,
      verRegulares: false
    };
    this.estilosFecha = {
      inputStyle: {
        color: 'white'
      },
      floatingLabelStyle: {color: 'white'}
    };
  }

  componentWillMount() {
    CursosActions.obtenerInfo(this.props.idCurso, this.props.parcial);
    CursosActions.obtenerAlumnos(this.props.idCurso, this.props.parcial, () => {
      this._actualizarPorcentajeAprobados();

      this.chart = new EasyPieChart(this.refs.porcentajeChart, {
        trackColor: 'rgba(255,255,255,0.2)',
        scaleColor: 'rgba(255,255,255,0.5)',
        barColor: 'rgba(255,255,255,0.7)',
        lineWidth: 7,
        lineCap: 'butt',
        size: 148
      });
    });
  }

  _getAlumnos() {
    return this.state.alumnos;
  }

  _cambiarNota(CursadaID, parcial, nota) {
    CursosActions.cambiarNota(nota, CursadaID, parcial, () => {
      this._actualizarPorcentajeAprobados();
    });
  }

  _exit() {
    this.props.history.goBack();
  }

  _mostrarPlanilla() {
    let url = '/Cursos/PDF/' +
      this.props.idCurso +
      '?instancia=' + this.props.parcial;

    if (this.state.verTodos || this.state.verLibres) {
      url = url + '&mostrarLibres=true';
    }

    window.open(url, '_blank');
  }

  _imprimirPlanilla(e) {
    e.preventDefault();
    let condicion;

    if (this.state.mostrarLibres) {
      condicion = _.find(this.state.alumnos, {Nota: null});
    } else {
      condicion = _.find(this.state.alumnos, {Libre: false, Nota: null});
    }

    if (condicion) {
      Notification.error('Faltan cargar notas');
    } else if (this.state.Fecha === null) {
      Notification.error('Faltan cargar la Fecha');
    } else {
      this._mostrarPlanilla();
    }
  }

  _formatDate(date) {
    let d = date.getDate();
    let m = date.getMonth() + 1;
    const y = date.getFullYear();

    if (d.toString().length === 1) { d = '0' + d; }
    if (m.toString().length === 1) { m = '0' + m; }
    const formattedDate = d + '/' + m + '/' + y;

    return formattedDate;
  }

  _parseMDY(str) {
    const parts = str.split('/');
    const day = parts[0];
    const month = parts[1];
    const year = parts[2];

    if (parts.length === 3) {
      return `${month}/${day}/${year}`;
    }
  }

  _setFecha(value) {
    if (value === null) {
      Notification.error('Fecha invalida');
    } else if (value.length === 10) {
      CursosActions.cambiarFecha(
        this.props.idCurso,
        this.props.parcial + '_FECHA',
        this._parseMDY(value)
      );
      this.setState({
        Fecha: value
      });
    }
  }

  _actualizarPorcentajeAprobados() {
    const notasCargadas = _.compact(_.map(_.pluck(this.state.alumnos, 'Nota'),
        function (value) { return value === 'Ausente' ? null : value; })
    );
    const aprobados = _.compact(_.map(notasCargadas,
      function (value) { return (parseInt(value, 10) >= 6) ? value : null; }
    ));
    const porcentaje = (aprobados.length / notasCargadas.length * 100).toFixed().toString();


    this.setState({
      porcentaje
    });

    if (this.chart) {
      this.chart.update(porcentaje);
    }
  }
  _cambiarVista(event, value) {
    switch (value) {
    case 'todos':
      this.setState({
        verTodos: true,
        verRegulares: false,
        verLibres: false
      });
      break;
    case 'regulares':
      this.setState({
        verTodos: false,
        verRegulares: true,
        verLibres: false
      });
      break;
    case 'libres':
      this.setState({
        verTodos: true,
        verRegulares: false,
        verLibres: true
      });
      break;
    default:
      break;
    }
  }

  render() {
    const notas = [
      {payload: 'Ausente', text: 'Ausente'},
      {payload: '1', text: '1'},
      {payload: '2', text: '2'},
      {payload: '3', text: '3'},
      {payload: '4', text: '4'},
      {payload: '5', text: '5'},
      {payload: '6', text: '6'},
      {payload: '7', text: '7'},
      {payload: '8', text: '8'},
      {payload: '9', text: '9'},
      {payload: '10', text: '10'}
    ];
    const dateInputStyle = {
      background: 'none',
      color: 'white',
      border: 'none',
      borderBottom: '1px solid white',
      marginTop: '20px',
      fontSize: '22px'
    };

    const headerClass = [
      'card-header',
      'ch-alt',
      'm-b-20'
    ]

    if (this.state.Cerrado) {
      headerClass.push('bgm-red');
    } else {
      headerClass.push('bgm-cyan');
    }

    return (
      <div>
        <div className="block-header">
          <h2>
            <strong>Profesor:</strong> {this.state.user.data.UserName}
          </h2>
        </div>
        <div className="block-header">
          <h2>
            <strong>CARRERA:</strong> {this.props.Carrera} - <strong>MATERIA:</strong> {this.state.Materia}
          </h2>
        </div>
        <div className="row">
          <div className="col-sm-8">
            <div className="card">
              <div className={headerClass.join(' ')}>
                <h2>
                  Notas de {this.props.parcial}
                  {this.state.Cerrado == false ? null :
                    <span style={{float: 'right', marginRight: '40px'}}>NOTAS CERRADAS (modo solo lectura)</span> }
                  <small>Curso: {this.state.Nombre}</small>
                </h2>
                <ul className="actions actions-alt">
                  <li>
                    <a href="#" onClick={this._exit.bind(this)}>
                      <i className="zmdi zmdi-close"></i>
                    </a>
                  </li>
                </ul>
                <a href="#"
                   onClick={this._imprimirPlanilla.bind(this)}
                   className="btn btn-float bgm-white waves-effect waves-circle waves-float">
                  <i className="zmdi zmdi-print"></i>
                </a>
              </div>
              <div className="card-body">
                <div className="row">
                  <div className="col-md-4">

                  </div>
                </div>
                <table className="table">
                  <thead>
                    <tr>
                      <th>Alumno</th>
                      <th>Nota</th>
                    </tr>
                  </thead>
                  <tbody>
                    {this._getAlumnos().map((alumno) => {
                      if (!this.state.verTodos && !this.state.verLibres && alumno.Libre) {
                        return null;
                      }

                      if (!this.state.verTodos && !this.state.verRegulares && alumno.Regular) {
                        return null;
                      }

                      return (
                        <tr key={alumno.CursadaId}>
                          <td>
                            <p style={{marginBottom: 0}}>
                              {alumno.Alumno}
                            </p>
                            <small>
                              {alumno.Documento}
                            </small>
                          </td>
                          <td>
                            {this.state.Cerrado == false ?
                            <UISelect
                              options={notas}
                              emptyText="Inserte Nota"
                              defaultValue={alumno.Nota}
                              onChange={
                                this._cambiarNota.bind(
                                  this, alumno.CursadaId,
                                  this.props.parcial
                                )
                              }
                            />
                            : <span>{alumno.Nota}</span> }
                          </td>
                        </tr>
                        );
                    })}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
          <div className="col-sm-3">
            <div className="mini-charts-item bgm-lightgreen">
              <div className="clearfix">
                <div className="chart stats-line">
                 <i className="zmdi zmdi-calendar zmdi-hc-5x"
                   style={{width: '85px', height: '45px', padding: '5px 15px 0', color: '#FFF'}}>
                 </i>
                </div>
                <div className="count">
                  <DateInput
                    disabled={this.state.Cerrado}
                    style={dateInputStyle}
                    onInputValidDate={this._setFecha.bind(this)}
                    value={this.state.Fecha}
                  />
                </div>

            </div>
          </div>
        </div>
        <div className="col-sm-3">
          <div className="card">
            <div className="card-body card-padding">
                <RadioButtonGroup name="vistas"
                  defaultSelected="todos"
                  onChange={this._cambiarVista.bind(this)}>
                <RadioButton
                  value="todos"
                  label="Ver Todos"/>
                <RadioButton
                  value="regulares"
                  label="Ver Regulares"/>
                <RadioButton
                  value="libres"
                  label="Ver Libres"/>
                </RadioButtonGroup>
            </div>
          </div>
        </div>
        <div className="col-sm-3">
          <div className="epc-item bgm-orange">
            <div className="easy-pie main-pie" ref="porcentajeChart"
              data-percent={this.state.porcentaje}>
              <div className="percent">{this.state.porcentaje}</div>
              <div className="pie-title">Total de Aprobados</div>
            </div>
          </div>
        </div>
      </div>
    </div>
    );
  }
}

CargaParcial.propTypes = {
  idCurso: React.PropTypes.string,
  //parcial: React.PropTypes.oneOf(ParcialesValidos),
  params: React.PropTypes.object,
  history: React.PropTypes.object,
  Carrera: React.PropTypes.string,
  parcial: function(props, propName, componentName) {
    if (['P1', 'P2', 'R1', 'R2'].indexOf(props[propName]) < 0 ) {
      // return new Error('Validation failed!');

      props.history.pushState(null, '/area-docentes/cursos/');
    }
  }
};

CargaParcial.contextTypes = {
  router: React.PropTypes.func
};

reactMixin.onClass(CargaParcial, Reflux.connect(CursosStore));
reactMixin.onClass(CargaParcial, Reflux.connect(UserStore));

export default CargaParcial;
