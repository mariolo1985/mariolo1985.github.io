'use strict';

Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _universalCookie = require('universal-cookie');

var _universalCookie2 = _interopRequireDefault(_universalCookie);

var _consts = require('../consts');

var Constants = _interopRequireWildcard(_consts);

function _interopRequireWildcard(obj) { if (obj && obj.__esModule) { return obj; } else { var newObj = {}; if (obj != null) { for (var key in obj) { if (Object.prototype.hasOwnProperty.call(obj, key)) newObj[key] = obj[key]; } } newObj.default = obj; return newObj; } }

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var cookies = new _universalCookie2.default();

var Login = function (_Component) {
    _inherits(Login, _Component);

    function Login() {
        _classCallCheck(this, Login);

        var _this = _possibleConstructorReturn(this, (Login.__proto__ || Object.getPrototypeOf(Login)).call(this));

        _this.onBtnLoginClick = function () {
            console.log('Login clicked');
        };

        _this.state = { loggingIn: false };
        return _this;
    }

    _createClass(Login, [{
        key: 'render',
        value: function render() {

            var loginText = this.state.loggingIn ? "please wait.." : "LOGIN";

            return _react2.default.createElement(
                'div',
                { className: 'login-container' },
                _react2.default.createElement(
                    'div',
                    { className: 'input-wrapper' },
                    _react2.default.createElement(
                        'div',
                        { className: 'input-row' },
                        _react2.default.createElement('input', { type: 'text', placeholder: 'user id', name: 'Email', className: 'login-userid' })
                    ),
                    _react2.default.createElement(
                        'div',
                        { className: 'input-row' },
                        _react2.default.createElement('input', { type: 'password', placeholder: 'password', name: 'Password', className: 'login-userpw' })
                    ),
                    _react2.default.createElement(
                        'div',
                        { className: 'input-row' },
                        _react2.default.createElement('input', { type: 'checkbox', className: 'login-remember-userid', id: 'login-remember-userid' }),
                        _react2.default.createElement(
                            'label',
                            { htmlFor: 'login-remember-userid', className: 'login-remember-userid-label' },
                            'Remember my user ID'
                        )
                    ),
                    _react2.default.createElement(
                        'div',
                        { className: 'input-row' },
                        _react2.default.createElement(
                            'button',
                            { onClick: this.onBtnLoginClick, disabled: this.state.loggingIn, type: 'submit', className: 'btn btn-login btn-primary' },
                            loginText
                        )
                    )
                )
            );
        }
    }]);

    return Login;
}(_react.Component);

exports.default = Login;