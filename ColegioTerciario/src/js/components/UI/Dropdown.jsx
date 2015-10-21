import React from 'react';
import Gravatar from 'react-gravatar';
import classNames from 'classnames';

export default class UIDropdown extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      dropdownVisible: false
    };
  }

  toggleDropdown() {
    this.setState({
      dropdownVisible: !this.state.dropdownVisible
    });
  }

  closeDropdown() {
    setTimeout(() => {
      this.setState({
        dropdownVisible: false
      });
    }, 500);
  }

  render() {
    const dropdownClasses = classNames({
      dropdown: true,
      'pull-right': true,
      open: this.state.dropdownVisible
    });

    return (
      <div className={dropdownClasses}>
        <button onClick={this.toggleDropdown.bind(this)}
          className="profile-dropdown-toggle">
          <span className="thumbnail-wrapper d32 circular inline m-t-5">
            <Gravatar email={this.props.user.data.UserName} />
          </span>
        </button>
        <ul className="dropdown-menu profile-dropdown" role="menu"
            onMouseLeave={this.closeDropdown.bind(this)}>
            {this.props.children}
        </ul>
      </div>
    );
  }
}

UIDropdown.propTypes = {
  user: React.PropTypes.object,
  children: React.PropTypes.array
};
