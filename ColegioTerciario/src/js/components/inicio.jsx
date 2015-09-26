import React from "react"; // eslint-disable-line no-unused-vars
import Component from './Component/main';

export default class InicioComponent extends Component {
  esDocente() {
    return this.state.user.isInRole('Docente');
  }
  render() {
      return (
          <div className="jumbotron" style= {{ background: "none" }} >
              <div className="container">
                  <div className="portlet light">
                      <h1>CENT 11</h1>

                      { this.esDocente() ?
                      <div>
                          <p>Contenido solo para Docentes</p>
                          <p>
                              <a className="btn btn-primary btn-lg" href="#" role="button">Vea sus Cursos »</a>
                          </p>
                          </div>
                          : ""
                      }
                  </div>

                  <div className="row">
                      <div className="col-md-6">
                          <div className="portlet light">
                              <div className="portlet-title">
                                  <div className="caption">
                                      <span className="caption-subject font-green-sharp bold uppercase">Alumnos</span>
                                  </div>
                              </div>
                          </div>
                      </div>
                      <div className="col-md-6">
                          <div className="portlet light">
                              <div className="portlet-title">
                                  <div className="caption">
                                      <span className="caption-subject font-red-sunglo bold uppercase">Parciales</span>
                                  </div>

                              </div>
                          </div>
                      </div>
                  </div>
              </div>
          </div>
      );
  }
};
