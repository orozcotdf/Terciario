export function enableSecondExam() {
  return {
    type: "ENABLE_SECOND_EXAM"
  };
}

export function decrement() {
  return {
    type: "DECREMENT_COUNTER"
  };
}

export function incrementIfOdd() {
  return (dispatch, getState) => {
    const { counter } = getState();

    if (counter % 2 === 0) {
      return;
    }

    dispatch(increment());
  };
}

export function enableSecondExamAsync(curso_id) {
  return dispatch => {
    return fetch(`/api/Cursos/HabilitarSegundoParcial/${curso_id}`,{
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      })
      .then(response => {
        dispatch(enableSecondExam())
        dispatch(refreshCourseList())
      });
  };
}

export function closeCourseModal() {
  return {
    type: "CLOSE_COURSE_MODAL"
  }
}

export function clearSelectedCourse() {
  return {
    type: "CLEAR_SELECTED_COURSE"
  }
}

export function fillCourseList() {
  return {
    type: "FILL_COURSE_LIST"
  }
}

export function refreshCourseList() {
  return {
    type: "REFRESH_COURSE_LIST"
  }
}

export function changeSelectedCourse(course) {
  return {
    type: "CHANGE_SELECTED_COURSE",
    course: course
  }
}
