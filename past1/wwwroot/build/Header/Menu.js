'use strict';

Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _class, _temp2;

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _propTypes = require('prop-types');

var _propTypes2 = _interopRequireDefault(_propTypes);

var _MenuItem = require('../Header/MenuItem');

var _MenuItem2 = _interopRequireDefault(_MenuItem);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var Menu = (_temp2 = _class = function (_Component) {
    _inherits(Menu, _Component);

    function Menu() {
        var _ref;

        var _temp, _this, _ret;

        _classCallCheck(this, Menu);

        for (var _len = arguments.length, args = Array(_len), _key = 0; _key < _len; _key++) {
            args[_key] = arguments[_key];
        }

        return _ret = (_temp = (_this = _possibleConstructorReturn(this, (_ref = Menu.__proto__ || Object.getPrototypeOf(Menu)).call.apply(_ref, [this].concat(args))), _this), _this.getMenuList = function () {
            var menuList = _this.props.menuList;

            var result = !menuList ? [] : menuList;

            return result;
        }, _this.getNavMenuClass = function () {
            var defaultClass = 'nav-menu';
            var isExpand = _this.props.expand;
            var result = isExpand ? defaultClass + ' expand' : defaultClass + ' collapse';

            return result;
        }, _temp), _possibleConstructorReturn(_this, _ret);
    }

    _createClass(Menu, [{
        key: 'render',
        value: function render() {
            var menuList = this.getMenuList();
            return _react2.default.createElement(
                'div',
                { className: this.getNavMenuClass() },
                _react2.default.createElement(
                    'ul',
                    { className: 'nav-menu-list' },
                    menuList.map(function (item, key) {
                        return _react2.default.createElement(_MenuItem2.default, { key: key, linkLabel: item.linkLabel, linkUrl: item.linkUrl });
                    })
                )
            );
        }
    }]);

    return Menu;
}(_react.Component), _class.propTypes = {
    expand: _propTypes2.default.bool.isRequired,
    menuList: _propTypes2.default.array.isRequired
}, _temp2);
exports.default = Menu;