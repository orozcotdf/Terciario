import React from 'react';
import MaskedInput from 'react-input-mask';

class DateInput extends React.Component {
  _onChange(event) {
    const fecha = event.target.value;

    if (/([0-9][0-9]\/[0-9][0-9]\/[0-9][0-9][0-9][0-9])/.test(fecha)) {
      if (new Date(this._parseMDY(fecha)).toString() !== 'Invalid Date') {
        this.props.onInputValidDate(fecha);
      } else {
        this.props.onInputValidDate(null);
      }
    }
  }

  _parseMDY(str) {
    const parts = str.split('/');
    const day = parts[0];
    const month = parts[1];
    const year = parts[2];

    if (parts.length === 3) {
      return `${month}/${day}/${year}`;
    }
  }

  render() {
    const inputStyle = {
      background: 'none',
      color: 'white',
      border: 'none',
      borderBottom: '1px solid white',
      marginTop: '20px',
      fontSize: '22px'
    };

    return (
      <div className="fg-line">
        <MaskedInput {...this.props} placeholder="Fecha"
          mask="99/99/9999"
          style={inputStyle}
          size={10}
          onChange={this._onChange.bind(this)}
        />
      </div>
    );
  }
}

DateInput.propTypes = {
  onInputValidDate: React.PropTypes.func.isRequired
};

export default DateInput;
