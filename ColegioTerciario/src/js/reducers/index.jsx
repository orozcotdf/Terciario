import {combineReducers} from 'redux';
import counter from './counter';
import areaDocentes from './AreaDocentesReducer';

export default combineReducers({
  counter,
  areaDocentes
});
