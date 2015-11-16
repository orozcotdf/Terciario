import React from 'react';
import {Link} from 'react-router';

class GriddleActionsComponent extends React.Component {
  render() {
    return (
      <div className="dropdown">
        <a href="#" className="dropdown-toggle btn btn-link btn-icon waves-effect"
          data-toggle="dropdown" aria-expanded="true">
          <i className="zmdi zmdi-more-vert"></i>
        </a>
        <ul className="dropdown-menu pull-right bgm-bluegray">
          <li role="presentation">
            <Link
              to={`/area-docentes/cursos/${this.props.data}/cargaParcial/P1`}>
                Carga Parcial 1
            </Link>
          </li>
          <li role="presentation">
            <Link
              to={`/area-docentes/cursos/${this.props.data}/cargaParcial/P2`}>
                Carga Parcial 2
            </Link>
          </li>
          <li role="presentation">
            <Link
              to={`/area-docentes/cursos/${this.props.data}/cargaParcial/R1`}>
                Carga Recuperatorio 1
            </Link>
          </li>
          <li role="presentation">
            <Link
              to={`/area-docentes/cursos/${this.props.data}/cargaParcial/R2`}>
                Carga Recuperatorio 2
            </Link>
          </li>
        </ul>
      </div>
    );
  }
}

GriddleActionsComponent.propTypes = {
  data: React.PropTypes.any.isRequired
};

export default GriddleActionsComponent;
