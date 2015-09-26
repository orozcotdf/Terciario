var React = require('react');

var $ = require('jquery');
import Equivalencias from './super'
import Router from 'react-router'
import Select from 'react-select'
import { DatePicker, TextField, FlatButton } from 'material-ui'
import {ReactBsTable, TableDataSet, BootstrapTable, TableHeaderColumn }  from "react-bootstrap-table"
import Griddle from 'griddle-react';
import ModalForm from './AgregaDetallesFormModal'
import GriddleWithCallback from '../lib/GriddleWithCallback'

export default class EditarEquivalencia extends Equivalencias {

  constructor(props) {
    super(props);
    this.emptyState = {
        EQUIVALENCIA_FECHA: '',
        EQUIVALENCIA_NRO_DISPOSICION: '',
        EQUIVALENCIA_ALUMNO_ID: '',
        EQUIVALENCIA_CARRERA_ID: ''
    };
    this.state = this.emptyState;
  }

  componentDidMount() {
    let _this = this;

    $.get("/api/Equivalencias/GetEquivalencia/" + this.props.params.id, function(data){
      _this.setState(data);
    });
  }

  onChange(e) {
    var nextState = {};
    nextState[e.target.name] = e.target.value;
    this.setState(nextState);
  }

  exit() {
    this.clearAndFocusInput();
    this.context.router.transitionTo('equivalencias');
  }

  setFecha(nill, date) {
    this.setState({
      EQUIVALENCIA_FECHA: this.formatDate(date)
    });
  }
  onSubmit(e) {
    //e.preventDefault();
    let _this = this;
    $.ajax({
      url: '/api/Equivalencias/PutEquivalencia/' + this.props.params.id,
      dataType: "json",
      method: 'PUT',
      data: {
        ID: this.props.params.id,
        EQUIVALENCIA_FECHA: this.formatDateForPost(this.refs.EQUIVALENCIA_FECHA.getDate()),
        EQUIVALENCIA_NRO_DISPOSICION: this.state.EQUIVALENCIA_NRO_DISPOSICION,
        EQUIVALENCIA_ALUMNO_ID: this.state.EQUIVALENCIA_ALUMNO_ID,
        EQUIVALENCIA_CARRERA_ID: this.state.EQUIVALENCIA_CARRERA_ID
      },
      success: function(){
        _this.exit();
      }
    });

  }

  clearAndFocusInput() {
      // Clear the input
    this.setState(this.emptyState, function() {
      // This code executes after the component is re-rendered
      React.findDOMNode(this.refs.EQUIVALENCIA_FECHA).focus();
    });
  }

  _getJsonData(filterString, sortColumn, sortAscending, page, pageSize, callback) {
    let _this = this;
    $.get("/api/equivalencias/GetDetalles/" +  this.props.params.id,
      { Pagina: page, RegistrosPorPagina: pageSize},
      function(data) {
        callback({
          totalResults: data.CantidadResultados,
          results: data.Resultados,
          pageSize: pageSize
        });
    });
  }

  _closeModal() {
    this.refs.w.setPage(0);
  }

  render() {
    let submitHandler = event => { return this.onSubmit(event); };
    let changeHandler = event => { return this.onChange(event); };
    let columnMeta = [
      {
        "columnName": "MATERIA_NOMBRE",
        "displayName": "Materia"
      }, {
        "columnName": "PERSONA_NOMBRE",
        "displayName": "Profesor"
      }, {
        "columnName": "EQUIVALENCIA_DETALLE_TIPO",
        "displayName": "Tipo"
      }
    ];

    return (
      <div>
        <div className="col-sm-6">
          <div className="portlet light">
            <div className="portlet-title">
              <div className="caption">
                Editar Equivalencia
              </div>
            </div>
            <div className="portlet-body form">
              <form>
                <div className="form-body">
                  <DatePicker  name="EQUIVALENCIA_FECHA"
                    hintText="Click para elegir fecha"
                    formatDate={this.formatDate}
                    autoOk={true}
                    mode="inline"
                    onChange={this.setFecha.bind(this)}
                    ref="EQUIVALENCIA_FECHA"
                    fullWidth={true}
                    value={new Date(this.state.EQUIVALENCIA_FECHA)}
                  />

                  <TextField
                    name="EQUIVALENCIA_NRO_DISPOSICION"
                    hintText="Nro Disposicion"
                    onChange={changeHandler}
                    fullWidth={true}
                    value={this.state.EQUIVALENCIA_NRO_DISPOSICION}
                  />

                  <div className="form-group">
                    <Select
                      name="EQUIVALENCIA_ALUMNO_ID"
                      asyncOptions={this.cargarAlumnos}
                      onChange={this.setAlumno.bind(this)}
                      cacheAsyncResults={false}
                      clearable={true}
                      placeholder="Alumno"
                      value={this.state.EQUIVALENCIA_ALUMNO_NOMBRE}
                    />
                  </div>
                  <div className="form-group">
                    <Select
                      name="EQUIVALENCIA_CARRERA_ID"
                      asyncOptions={this.cargarCarreras}
                      onChange={this.setCarrera.bind(this)}
                      clearable={true}
                      placeholder="Carrera"
                      value={this.state.EQUIVALENCIA_CARRERA_NOMBRE}
                    />
                  </div>
                </div>
                <div className="form-actions right">
                  <FlatButton label="Cancelar" onTouchTap={this.exit.bind(this)}/>
                  <FlatButton label="Guardar" onTouchTap={submitHandler} primary={true}/>
                </div>
              </form>
            </div>
          </div>
        </div>
          <div className="col-sm-6">
            <ModalForm modelId={this.props.params.id} onClose={this._closeModal.bind(this)}/>

            <div className="portlet light">
              <div className="portlet-body">
                <GriddleWithCallback ref="w"
                  getExternalResults={this._getJsonData.bind(this)}
                  columnMetadata = {columnMeta}
                  columns={["MATERIA_NOMBRE", "PERSONA_NOMBRE", "EQUIVALENCIA_DETALLE_TIPO"]}
                  loadingText = "Cargando..."
                  noDataMessage = "No se encontraron resultados"/>

              </div>
            </div>
          </div>
        </div>
    );
  }
}

EditarEquivalencia.contextTypes = {
  router: React.PropTypes.func.isRequired
};

/**


<Griddle results={this.state.EQUIVALENCIA_DETALLES} useGriddleStyles={false} useExternal={true}
                  tableClassName="table"
                  externalSetPage={this.setPage}
                  externalMaxPage={this.state.CantidadPaginas}
                  columns={["MATERIA_NOMBRE", "PROFESOR_NOMBRE", "EQUIVALENCIA_DETALLE_TIPO"]}
                  columnMetadata={[
                    {
                      "columnName": "MATERIA_NOMBRE",
                      "displayName": "Materia"
                    }, {
                      "columnName": "PROFESOR_NOMBRE",
                      "displayName": "Profesor"
                    }, {
                      "columnName": "EQUIVALENCIA_DETALLE_TIPO",
                      "displayName": "Tipo"
                    }
                  ]}
                />

                **/
