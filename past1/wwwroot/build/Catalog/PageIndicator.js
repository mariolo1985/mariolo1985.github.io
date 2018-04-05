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

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var PageIndicator = (_temp = _class = function (_Component) {
    _inherits(PageIndicator, _Component);

    function PageIndicator(props) {
        _classCallCheck(this, PageIndicator);

        var _this = _possibleConstructorReturn(this, (PageIndicator.__proto__ || Object.getPrototypeOf(PageIndicator)).call(this, props));

        _this.componentWillReceiveProps = function (nextProps) {
            _this.setState({
                currPageNumber: nextProps.currPage
            });
        };

        _this.getPageNumber = function () {
            var result = _this.state.pageNumber;
            return result;
        };

        _this.getCurrPageNumber = function () {
            var result = _this.state.currPageNumber;

            return result;
        };

        _this.getIsBtnCurrPage = function () {
            var pageNum = _this.getPageNumber();
            var currPageNum = _this.getCurrPageNumber();
            var result = pageNum === currPageNum;

            return result;
        };

        _this.getBtnClass = function () {
            var defaultClass = 'btn-catalog-page-indicator';
            var isBtnCurrPage = _this.getIsBtnCurrPage();
            var pageClass = isBtnCurrPage ? defaultClass + ' current' : '' + defaultClass;

            return pageClass;
        };

        _this.onPageIndicatorClick = function () {
            var isCurrPage = _this.getIsBtnCurrPage();

            if (!isCurrPage) {
                _this.props.onPageIndicatorClick(_this.getPageNumber());
            }
        };

        _this.state = {
            pageNumber: _this.props.page,
            currPageNumber: _this.props.currPage
        };
        return _this;
    }

    _createClass(PageIndicator, [{
        key: 'render',
        value: function render() {

            return _react2.default.createElement(
                'button',
                { className: this.getBtnClass(), page: this.getPageNumber(), onClick: this.onPageIndicatorClick, disabled: this.getIsBtnCurrPage() },
                this.getIsBtnCurrPage() ? _react2.default.createElement(
                    'svg',
                    { className: 'page-current', fill: '#444444', height: '18', viewBox: '0 0 24 24', width: '18' },
                    _react2.default.createElement('circle', { cx: '12', cy: '12', r: '10' }),
                    _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' })
                ) : _react2.default.createElement(
                    'svg',
                    { className: 'page-other', fill: '#444444', height: '18', viewBox: '0 0 24 24', width: '18' },
                    _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' }),
                    _react2.default.createElement('path', { d: 'M12 2C6.47 2 2 6.47 2 12s4.47 10 10 10 10-4.47 10-10S17.53 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z' })
                )
            );
        }
    }]);

    return PageIndicator;
}(_react.Component), _class.PropTypes = {
    page: _propTypes2.default.number.isRequired,
    currPage: _propTypes2.default.number.isRequired
}, _temp);
exports.default = PageIndicator;