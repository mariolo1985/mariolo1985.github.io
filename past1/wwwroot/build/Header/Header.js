'use strict';

Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _class, _temp;

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _propTypes = require('prop-types');

var _propTypes2 = _interopRequireDefault(_propTypes);

var _Menu = require('../Header/Menu');

var _Menu2 = _interopRequireDefault(_Menu);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var Header = (_temp = _class = function (_Component) {
    _inherits(Header, _Component);

    function Header(props) {
        _classCallCheck(this, Header);

        var _this = _possibleConstructorReturn(this, (Header.__proto__ || Object.getPrototypeOf(Header)).call(this, props));

        _this.handleShowMenu = function () {
            var showMenu = _this.props.showMenu;

            var result = !showMenu ? Header.defaultProps.showMenu : showMenu;

            return result;
        };

        _this.handleShowTitle = function () {
            var showTitle = _this.props.showTitle;

            var result = !showTitle ? Header.defaultProps.showTitle : showTitle;

            return result;
        };

        _this.onBtnMenuClick = function () {
            // toggle menu
            _this.setState({
                isMenuExpanded: !_this.state.isMenuExpanded
            });
        };

        _this.getTitle = function () {
            var title = _this.props.title;

            var result = !title ? Header.defaultProps.title : title;

            return result;
        };

        _this.componentDidUpdate = function (prevProps, prevState) {

            // hide menu on blur
            if (_this.state.isMenuExpanded) {
                document.addEventListener('click', _this.closeMenu);
            } else {
                document.removeEventListener('click', _this.closeMenu);
            }
        };

        _this.componentWillUnmount = function () {
            // hide menu blur events        
            document.removeEventListener('click', _this.closeMenu);
        };

        _this.closeMenu = function () {
            // Set dropdown to collapse
            _this.setState({
                isMenuExpanded: false
            });
        };

        _this.getMenuList = function () {
            var menuList = [{
                linkLabel: 'My Profile',
                linkUrl: './catalog.html'
            }, {
                linkLabel: 'Course Catalog',
                linkUrl: './coursecatalog.html'
            }, {
                linkLabel: 'Downlines',
                linkUrl: '#'
            }, {
                linkLabel: 'Sign Out',
                linkUrl: '#'
            }];

            return menuList;
        };

        _this.state = {
            isMenuExpanded: false
        };
        return _this;
    }

    _createClass(Header, [{
        key: 'render',
        value: function render() {
            var showMenu = this.handleShowMenu();
            var showTitle = this.handleShowTitle();
            var username = "James";

            var menuList = this.getMenuList();

            return _react2.default.createElement(
                'div',
                { className: 'nav-container clear' },
                _react2.default.createElement(
                    'div',
                    { className: 'nav-branding-wrapper clear' },
                    _react2.default.createElement(
                        'div',
                        { className: 'nav-branding-img-wrapper' },
                        _react2.default.createElement(
                            'a',
                            { href: '#' },
                            _react2.default.createElement('img', { src: './content/images/logo_usana.png', className: 'nav-branding-img' })
                        )
                    ),
                    showTitle ? _react2.default.createElement(
                        'div',
                        { className: 'nav-title-wrapper' },
                        _react2.default.createElement(
                            'div',
                            { className: 'nav-title' },
                            this.getTitle()
                        )
                    ) : null
                ),
                showMenu ? _react2.default.createElement(
                    'div',
                    { className: 'nav-menu-wrapper clear' },
                    _react2.default.createElement(
                        'div',
                        { className: 'nav-user' },
                        _react2.default.createElement(
                            'div',
                            { className: 'nav-username' },
                            'Hello, ',
                            username,
                            '!'
                        ),
                        _react2.default.createElement(
                            'a',
                            { href: '#', className: 'nav-logout' },
                            'Logout'
                        )
                    ),
                    _react2.default.createElement(
                        'div',
                        { className: 'nav-user-img' },
                        _react2.default.createElement(
                            'button',
                            { className: 'btn-nav-user-img' },
                            _react2.default.createElement(
                                'svg',
                                { className: 'nav-user-icon', fill: '#FFFFFF', height: '24', viewBox: '0 0 24 24', width: '24', xmlns: 'http://www.w3.org/2000/svg' },
                                _react2.default.createElement('path', { d: 'M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z' }),
                                _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' })
                            )
                        )
                    ),
                    _react2.default.createElement(
                        'div',
                        { className: 'nav-action' },
                        _react2.default.createElement(
                            'button',
                            { className: 'btn-nav-menu', onClick: this.onBtnMenuClick },
                            _react2.default.createElement(
                                'svg',
                                { fill: '#444444', height: '20', viewBox: '0 0 24 24', width: '22' },
                                _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' }),
                                _react2.default.createElement('path', { d: 'M3 18h18v-2H3v2zm0-5h18v-2H3v2zm0-7v2h18V6H3z' })
                            )
                        )
                    )
                ) : null,
                showMenu ? _react2.default.createElement(_Menu2.default, { expand: this.state.isMenuExpanded, menuList: menuList }) : null
            );
        }
    }]);

    return Header;
}(_react.Component), _class.PropTypes = {
    showMenu: _propTypes2.default.bool.isRequired,
    showTitle: _propTypes2.default.bool.isRequired,
    title: _propTypes2.default.string
}, _class.defaultProps = {
    showMenu: false,
    showTitle: false,
    title: ''
}, _temp);
exports.default = Header;