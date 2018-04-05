'use strict';

Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var CourseCatalogItemsPerPage = function (_Component) {
    _inherits(CourseCatalogItemsPerPage, _Component);

    function CourseCatalogItemsPerPage() {
        var _ref;

        var _temp, _this, _ret;

        _classCallCheck(this, CourseCatalogItemsPerPage);

        for (var _len = arguments.length, args = Array(_len), _key = 0; _key < _len; _key++) {
            args[_key] = arguments[_key];
        }

        return _ret = (_temp = (_this = _possibleConstructorReturn(this, (_ref = CourseCatalogItemsPerPage.__proto__ || Object.getPrototypeOf(CourseCatalogItemsPerPage)).call.apply(_ref, [this].concat(args))), _this), _this.onSelectChange = function () {
            var courseCatalogSelectElements = document.getElementsByClassName('coursecatalog-course-page-select');
            var selectElement = courseCatalogSelectElements.length === 0 ? null : courseCatalogSelectElements[0];

            var selectValue;
            if (selectElement === null) {
                selectValue = _this.getItemsPerPageCount();
            } else {

                selectValue = selectElement.value;
            }

            _this.props.handleItemsPerPageChange(selectValue);
        }, _this.getItemsPerPageCount = function () {
            var itemsPerPage = _this.props.itemsPerPage;

            var result = !itemsPerPage ? 1 : itemsPerPage;

            return result;
        }, _this.getCourseMenu = function () {
            var courseMenu = _this.props.courseMenu;

            var result = !courseMenu ? [] : courseMenu;

            return result;
        }, _this.getOptionsHtml = function () {
            var courseMenu = _this.getCourseMenu(),
                itemCount = courseMenu.length;
            var itemsPerPage = _this.getItemsPerPageCount();
            var optionCount = Math.ceil(itemCount / itemsPerPage);
            optionCount = optionCount <= 0 ? 1 : optionCount;

            var htmlArr = [];
            var html = "";

            for (var x = 1; x <= optionCount; x++) {
                html = _react2.default.createElement(
                    'option',
                    { key: x, value: x * itemsPerPage },
                    x * itemsPerPage,
                    ' per page'
                );
                htmlArr.push(html);
            }

            return htmlArr;
        }, _temp), _possibleConstructorReturn(_this, _ret);
    }

    _createClass(CourseCatalogItemsPerPage, [{
        key: 'render',
        value: function render() {

            return _react2.default.createElement(
                'select',
                { className: 'coursecatalog-course-page-select', onChange: this.onSelectChange },
                this.getOptionsHtml()
            );
        }
    }]);

    return CourseCatalogItemsPerPage;
}(_react.Component);

exports.default = CourseCatalogItemsPerPage;