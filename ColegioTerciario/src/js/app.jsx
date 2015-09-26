import React from 'react'
import Router from 'react-router'
import routes from './config/routes'
import injectTapEventPlugin from "react-tap-event-plugin"

require('react-select/dist/default.css')


//Needed for onTouchTap
//Can go away when react 1.0 release
//Check this repo:
//https://github.com/zilverline/react-tap-event-plugin
injectTapEventPlugin();

Router.run(routes, function(Root){
    React.render(<Root />, document.getElementById('appContainer'));
});
