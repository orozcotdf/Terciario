import { createStore, applyMiddleware } from 'redux';
import thunkMiddleware from 'redux-thunk';
import createLogger from 'redux-logger';
import reducer from '../reducers';

// Use redux-logger only in dev mode
const __DEV__ = true// SOME TYPE OF ENV VARIABLE;
const logger = createLogger({
  predicate: (getState, action) => __DEV__
});

const createStoreWithMiddleware = applyMiddleware(
  thunkMiddleware,
  logger
)(createStore);

export default function createApiClientStore(initialState) {
  return createStoreWithMiddleware(reducer, initialState);
}
