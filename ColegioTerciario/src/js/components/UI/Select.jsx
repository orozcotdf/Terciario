import React from 'react';
import $ from 'jquery';

require('bootstrap-select/dist/css/bootstrap-select.css');
require('bootstrap-select');

const UISelect = React.createClass({
  propTypes: {
    options: React.PropTypes.array.isRequired,
    emptyText: React.PropTypes.string,
    defaultValue: React.PropTypes.string,
    onChange: React.PropTypes.func
  },

  getDefaultProps() {
    return {
      defaultValue: '',
      emptyText: 'Seleccione una opcion'
    };
  },

  componentDidMount() {
    $(React.findDOMNode(this.refs.select)).selectpicker();
  },

  _onChange(event) {
    this.props.onChange(event.target.value);
  },

  render() {
    return (
      <select
        className="selectpicker"
        ref="select"
        data-size="5"
        defaultValue={this.props.defaultValue}
        onChange={this._onChange}
      >
        <option value="">{this.props.emptyText}</option>
        {this.props.options.map((option) => {
          return (
            <option key={option.payload} value={option.payload}>{option.text}</option>
          );
        })}
      </select>
    );
  }
});

export default UISelect;
