import React from 'react';
import ReactDOM from 'react-dom';
import CursosStore from '../../../../stores/cursosStore';
import Reflux from 'reflux';
import CursosActions from '../../../../actions/cursosActions';
import UISelect from '../../../UI/Select';
import _ from 'lodash';
import Notification from 'Notification';
import {DatePicker} from 'material-ui';
import EasyPieChart from 'easy-pie-chart/dist/easypiechart.js';

const CargaParcial = React.createClass({

  chart: null,
  parcialesValidos: ['P1', 'P2', 'R1', 'R2'],

  mixins: [Reflux.connect(CursosStore)],

  contextTypes: {
    router: React.PropTypes.func
  },

  componentWillMount() {
    CursosActions.obtenerInfo(this.props.params.idCurso, this.props.params.parcial);
    CursosActions.obtenerAlumnos(this.props.params.idCurso, this.props.params.parcial, () => {
      this._actualizarPorcentajeAprobados();

      this.chart = new EasyPieChart(ReactDOM.findDOMNode(this.refs.porcentajeChart), {
        trackColor: 'rgba(255,255,255,0.2)',
        scaleColor: 'rgba(255,255,255,0.5)',
        barColor: 'rgba(255,255,255,0.7)',
        lineWidth: 7,
        lineCap: 'butt',
        size: 148
      });
    });
  },

  componentWillUpdate(nextProps, nextState) {
    // Chequea si se le pasa una instancia de parcial valida
    if (nextProps) {
      if (this.parcialesValidos.indexOf(nextProps.params.parcial) < 0) {
        this.props.history.pushState(null, '/area-docentes/cursos/');
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

  _cambiarNota(CursadaID, parcial, nota) {
    CursosActions.cambiarNota(nota, CursadaID, parcial, () => {
      this._actualizarPorcentajeAprobados();
    });
  },

  _exit() {
    this.props.history.goBack();
  },

  _imprimirPlanilla(e) {
    e.preventDefault();
    if (_.find(this.state.alumnos, {Nota: null})) {
      Notification.error('Faltan cargar notas');
    } else {
      location.href = '/Cursos/PDF/' +
        this.props.params.idCurso +
        '?instancia=' + this.props.params.parcial;
    }
  },

  _formatDate(date) {
    let d = date.getDate();
    let m = date.getMonth() + 1;
    const y = date.getFullYear();

    if (d.toString().length === 1) { d = '0' + d; }
    if (m.toString().length === 1) { m = '0' + m; }
    const formattedDate = d + '/' + m + '/' + y;

    return formattedDate;
  },

  _setFecha(nill, value) {
    CursosActions.cambiarFecha(
      this.props.params.idCurso,
      this.props.params.parcial + '_FECHA',
      value);
    this.setState({
      Fecha: value
    });
  },

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
  },

  estilosFecha: {
    inputStyle: {
      color: 'white'
    },
    floatingLabelStyle: {color: 'white'}
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
        <div className="col-sm-10 col-sm-offset-1 text-center">
          <div className="block-header">
            <h2>{this.state.Carrera} - {this.state.Materia}</h2>
          </div>
        </div>
      <div className="col-sm-6 col-sm-offset-1">
        <div className="card">
          <div className="card-header ch-alt m-b-20">
            <h2>
              Notas de {this.props.params.parcial}
              <small>Curso: {this.state.Nombre}</small>
            </h2>
            <ul className="actions">
              <li>
                <a href="#" onClick={this._exit}>
                  <i className="zmdi zmdi-close"></i>
                </a>
              </li>
            </ul>
            <a href="#"
               onClick={this._imprimirPlanilla}
               className="btn bgm-red btn-float waves-effect waves-circle waves-float">
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
                  return (
                    <tr key={alumno.CursadaId}>
                      <td>{alumno.Alumno}</td>
                      <td>
                        <UISelect
                          options={notas}
                          emptyText="Inserte Nota"
                          defaultValue={alumno.Nota}
                          onChange={
                            this._cambiarNota.bind(
                              this, alumno.CursadaId,
                              this.props.params.parcial
                            )
                          }
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
      <div className="col-sm-3">
        <div className="mini-charts-item bgm-lightgreen">
          <div className="clearfix">
            <div className="chart stats-line">
             <i className="zmdi zmdi-calendar zmdi-hc-5x"
               style={{width: '85px', height: '45px', padding: '5px 15px 0', color: '#FFF'}}>
             </i>
            </div>
            <div className="count">
              <DatePicker
                floatingLabelText="Modificar fecha"
                hintText="Modificar fecha"
                name="FECHA"
                value={this.state.Fecha}
                formatDate={this._formatDate}
                locale="es"
                autoOk={true}
                mode="inline"
                onChange={this._setFecha}
                inputStyle={this.estilosFecha.inputStyle}
                floatingLabelStyle={this.estilosFecha.floatingLabelStyle}
                />
            </div>

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
    );
  }
});

export default CargaParcial;
