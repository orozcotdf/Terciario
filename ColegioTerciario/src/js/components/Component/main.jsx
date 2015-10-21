import React from 'react';
import {ThemeManager, LightRawTheme} from 'material-ui';

class Component extends React.Component {

  getInitialState() {
    return {
      muiTheme: ThemeManager.getMuiTheme(LightRawTheme)
    };
  }

  getChildContext() {
    return {
      muiTheme: this.state.muiTheme
    };
  }

  formatDate(date) {
    let d = date.getDate();
    let m = date.getMonth() + 1;
    const y = date.getFullYear();

    if (d.toString().length === 1) { d = '0' + d; }
    if (m.toString().length === 1) { m = '0' + m; }
    return d + '/' + m + '/' + y;
  }


  formatDateForPost(date) {
    const d = date.getDate();
    const m = date.getMonth() + 1;
    const y = date.getFullYear();

    return m + '/' + d + '/' + y;
  }

}

Component.childContextTypes = {
  muiTheme: React.PropTypes.object
};

export default Component;
