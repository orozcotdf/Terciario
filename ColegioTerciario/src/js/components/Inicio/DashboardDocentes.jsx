import React from 'react';
import {Link} from 'react-router';

class DashboardDocentes extends React.Component {
  render() {
    return (
      <div className="card">
        <div className="card-header">
          <h2>Cent 11</h2>
        </div>
        <div className="card-body card-padding">
          <div className="portlet light">
            <div>
              <p>Contenido solo para Docentes</p>
              <p>
                <Link to={"/area-docentes/cursos"} className="btn btn-primary btn-lg">
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
                    <span className="caption-subject font-red-sunglo bold uppercase">
                      Parciales
                    </span>
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
