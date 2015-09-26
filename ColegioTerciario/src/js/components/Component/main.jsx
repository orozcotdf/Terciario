import React from 'react'
import mui from 'material-ui'
let ThemeManager = new mui.Styles.ThemeManager();

export default class Component extends React.Component {

  static get childContextTypes()
  {
      return {
        muiTheme: React.PropTypes.object
      };
  }
  getChildContext() {
    return { muiTheme: ThemeManager.getCurrentTheme() };
  }

	constructor(props) {
    super(props);
    this.state = {user: window.User};
  }

  formatDate(date) {
    let d = date.getDate();
    if (d.toString().length == 1) d = "0" + d;
    let m = date.getMonth() + 1;
    if (m.toString().length == 1) m = "0" + m;
    let y = date.getFullYear();
    return d + '/' + m + '/' + y;
  }


  formatDateForPost(date) {
    let d = date.getDate();
    let m = date.getMonth() + 1;
    let y = date.getFullYear();
    return m + '/' + d + '/' + y;
  }

}
