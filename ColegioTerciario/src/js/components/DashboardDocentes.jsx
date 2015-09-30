import React from 'react';
import {Link} from 'react-router';

class DashboardDocentes extends React.Component {
  render() {
    return (
      <div className="jumbotron" style={{background: 'none'}} >
            <div className="container">
                <div className="portlet light">
                    <h1>CENT 11</h1>

                    <div>
                      <p>Contenido solo para Docentes</p>
                      <p>
                        <Link to="cursos" className="btn btn-primary btn-lg">
                          Vea sus Cursos »
                        </Link>
                      </p>
                    </div>
                </div>

                <div className="row">
                    <div className="col-md-6">
                        <div className="portlet light">
                            <div className="portlet-title">
                                <div className="caption">
                                    <span className="caption-subject font-green-sharp bold uppercase">
                                      Alumnos
                                    </span>
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
}

export default DashboardDocentes;
