import {createReducer} from '../utils';
import {COUNTER_INCREMENT} from '../constants/counter';

const initialState = {
  selectedCourse: {
    id: 0,
    amountExams: 0
  },
  refreshCourseList: false,
  showModal: false
};

const area_docentes_reducer = (state = initialState, action) => {
  switch (action.type) {
    case "CHANGE_SELECTED_COURSE":
      return Object.assign({}, state, {
        selectedCourse: action.course,
        showModal: true
      });
    case "CLOSE_COURSE_MODAL":
      return Object.assign({}, state, {
        showModal: false
      });
    case "CLEAR_SELECTED_COURSE":
      return Object.assign({}, state, initialState);
    case "ENABLE_SECOND_EXAM":
      const selectedCourse = Object.assign({}, state.selectedCourse);
      selectedCourse.amountExams = 2;
      return Object.assign({}, state, {
        selectedCourse: selectedCourse
      });
    case "REFRESH_COURSE_LIST":
      return Object.assign({}, state, {
        refreshCourseList: true
      })
    default:
      return state;
  }
}

export default area_docentes_reducer;
