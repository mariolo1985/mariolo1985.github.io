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

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var CourseCatalogItem = (_temp2 = _class = function (_Component) {
    _inherits(CourseCatalogItem, _Component);

    function CourseCatalogItem() {
        var _ref;

        var _temp, _this, _ret;

        _classCallCheck(this, CourseCatalogItem);

        for (var _len = arguments.length, args = Array(_len), _key = 0; _key < _len; _key++) {
            args[_key] = arguments[_key];
        }

        return _ret = (_temp = (_this = _possibleConstructorReturn(this, (_ref = CourseCatalogItem.__proto__ || Object.getPrototypeOf(CourseCatalogItem)).call.apply(_ref, [this].concat(args))), _this), _this.getItemTitle = function () {
            var title = _this.props.item.title;

            var result = !title ? "" : title;

            return result;
        }, _this.getItemDescription = function () {
            var description = _this.props.item.description;

            var result = !description ? "" : description;

            return result;
        }, _this.getIsItemCompleted = function () {
            var isCompleted = _this.props.item.isCompleted;

            var result = !isCompleted ? false : isCompleted;

            return result;
        }, _this.getIsItemInComplete = function () {
            var isIncomplete = _this.props.item.isIncomplete;

            var result = !isIncomplete ? false : isIncomplete;

            return result;
        }, _this.getIsItemNotStarted = function () {
            var isCompleted = _this.getIsItemCompleted();
            var isIncomplete = _this.getIsItemInComplete();

            // Has not been start if not completed and not incomplete
            var result = isCompleted === false && isIncomplete === false ? true : false;
            return result;
        }, _this.getIsItemHighlighted = function () {
            var result = true; // to do - when is an item hightlighted?

            return result;
        }, _this.getCourseIconClass = function () {
            var defaultClass = 'course-icon';
            var isHighlighted = _this.getIsItemHighlighted();
            var result = isHighlighted ? defaultClass + ' highlight' : defaultClass;

            return result;
        }, _this.getCourseIcon1 = function () {
            var icon = _react2.default.createElement(
                'svg',
                { fill: '#444444', height: '36', viewBox: '0 0 24 24', width: '36', xmlns: 'http://www.w3.org/2000/svg' },
                _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' }),
                _react2.default.createElement('path', { d: 'M19 3h-4.18C14.4 1.84 13.3 1 12 1c-1.3 0-2.4.84-2.82 2H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm-7 0c.55 0 1 .45 1 1s-.45 1-1 1-1-.45-1-1 .45-1 1-1zm2 14H7v-2h7v2zm3-4H7v-2h10v2zm0-4H7V7h10v2z' })
            );

            return icon;
        }, _this.getCourseIcon2 = function () {
            var icon = _react2.default.createElement(
                'svg',
                { fill: '#444444', height: '36', viewBox: '0 0 24 24', width: '36', xmlns: 'http://www.w3.org/2000/svg' },
                _react2.default.createElement('path', { d: 'M0 0h24v24H0V0z', fill: 'none' }),
                _react2.default.createElement('path', { d: 'M23 11.01L18 11c-.55 0-1 .45-1 1v9c0 .55.45 1 1 1h5c.55 0 1-.45 1-1v-9c0-.55-.45-.99-1-.99zM23 20h-5v-7h5v7zM20 2H2C.89 2 0 2.89 0 4v12c0 1.1.89 2 2 2h7v2H7v2h8v-2h-2v-2h2v-2H2V4h18v5h2V4c0-1.11-.9-2-2-2zm-8.03 7L11 6l-.97 3H7l2.47 1.76-.94 2.91 2.47-1.8 2.47 1.8-.94-2.91L15 9h-3.03z' })
            );

            return icon;
        }, _this.getCourseIcon3 = function () {
            var icon = _react2.default.createElement(
                'svg',
                { fill: '#444444', height: '36', viewBox: '0 0 24 24', width: '36', xmlns: 'http://www.w3.org/2000/svg' },
                _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' }),
                _react2.default.createElement('path', { d: 'M11.99 2C6.47 2 2 6.48 2 12s4.47 10 9.99 10C17.52 22 22 17.52 22 12S17.52 2 11.99 2zm6.93 6h-2.95c-.32-1.25-.78-2.45-1.38-3.56 1.84.63 3.37 1.91 4.33 3.56zM12 4.04c.83 1.2 1.48 2.53 1.91 3.96h-3.82c.43-1.43 1.08-2.76 1.91-3.96zM4.26 14C4.1 13.36 4 12.69 4 12s.1-1.36.26-2h3.38c-.08.66-.14 1.32-.14 2 0 .68.06 1.34.14 2H4.26zm.82 2h2.95c.32 1.25.78 2.45 1.38 3.56-1.84-.63-3.37-1.9-4.33-3.56zm2.95-8H5.08c.96-1.66 2.49-2.93 4.33-3.56C8.81 5.55 8.35 6.75 8.03 8zM12 19.96c-.83-1.2-1.48-2.53-1.91-3.96h3.82c-.43 1.43-1.08 2.76-1.91 3.96zM14.34 14H9.66c-.09-.66-.16-1.32-.16-2 0-.68.07-1.35.16-2h4.68c.09.65.16 1.32.16 2 0 .68-.07 1.34-.16 2zm.25 5.56c.6-1.11 1.06-2.31 1.38-3.56h2.95c-.96 1.65-2.49 2.93-4.33 3.56zM16.36 14c.08-.66.14-1.32.14-2 0-.68-.06-1.34-.14-2h3.38c.16.64.26 1.31.26 2s-.1 1.36-.26 2h-3.38z' })
            );

            return icon;
        }, _this.getCompletedStatusIcon = function () {
            var result = _react2.default.createElement('div', { className: 'coursecatalog-item-status status-completed' });
            return result;
        }, _this.getIncompleteStatusIcon = function () {
            var result = _react2.default.createElement(
                'div',
                { className: 'coursecatalog-item-status status-incomplete' },
                _react2.default.createElement('div', { className: 'status-incomplete-front' }),
                _react2.default.createElement('div', { className: 'status-incomplete-back' })
            );

            return result;
        }, _this.getNotStartedStatusIcon = function () {
            var result = _react2.default.createElement('div', { className: 'coursecatalog-item-status status-notstarted' });

            return result;
        }, _temp), _possibleConstructorReturn(_this, _ret);
    }

    // Get item statuses


    // Course Icons


    // Status Icons


    _createClass(CourseCatalogItem, [{
        key: 'render',
        value: function render() {

            var title = this.getItemTitle();
            var description = this.getItemDescription();

            var isNotStarted = this.getIsItemNotStarted();
            var isCompleted = this.getIsItemCompleted();
            var isInComplete = this.getIsItemInComplete();

            var courseIcon1 = this.getCourseIcon1();
            var courseIcon2 = this.getCourseIcon2();
            var courseIcon3 = this.getCourseIcon3();

            return _react2.default.createElement(
                'div',
                { className: 'coursecatalog-table-row coursecatalog-item' },
                _react2.default.createElement(
                    'div',
                    { className: 'coursecatalog-table-description' },
                    _react2.default.createElement(
                        'span',
                        { className: this.getCourseIconClass() },
                        courseIcon1
                    ),
                    _react2.default.createElement(
                        'span',
                        { className: 'course-title' },
                        title
                    ),
                    _react2.default.createElement(
                        'span',
                        { className: 'course-title-divider' },
                        ':'
                    ),
                    _react2.default.createElement(
                        'span',
                        { className: 'course-description' },
                        description
                    )
                ),
                _react2.default.createElement(
                    'div',
                    { className: 'coursecatalog-table-status' },
                    _react2.default.createElement(
                        'span',
                        { 'class': 'course-status' },
                        isNotStarted ? this.getNotStartedStatusIcon() : isCompleted ? this.getCompletedStatusIcon() : isInComplete ? this.getIncompleteStatusIcon() : null
                    )
                )
            );
        }
    }]);

    return CourseCatalogItem;
}(_react.Component), _class.propTypes = {
    item: _propTypes2.default.shape({
        title: _propTypes2.default.string.isRequired,
        description: _propTypes2.default.string.isRequired,
        isCompleted: _propTypes2.default.bool.isRequired,
        isIncomplete: _propTypes2.default.bool.isRequired
    })
}, _temp2);
exports.default = CourseCatalogItem;