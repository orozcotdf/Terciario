import {createReducer} from '../utils';
import {COUNTER_INCREMENT} from '../constants/counter';

const initialState = {
  selected_course: 0
};

const area_docentes_reducer = (state = initialState, action) => {
  switch (action.type) {
    case "CHANGE_SELECTED_COURSE":
      return Object.assign({}, state, {selected_course: action.course});
    default:
      return state;
  }
}

export default area_docentes_reducer;
