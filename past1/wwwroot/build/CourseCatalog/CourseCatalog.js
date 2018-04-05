'use strict';

Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _class, _temp, _initialiseProps;

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _propTypes = require('prop-types');

var _propTypes2 = _interopRequireDefault(_propTypes);

var _CourseCatalogItem = require('../CourseCatalog/CourseCatalogItem');

var _CourseCatalogItem2 = _interopRequireDefault(_CourseCatalogItem);

var _CourseCatalogItemsPerPage = require('../CourseCatalog/CourseCatalogItemsPerPage');

var _CourseCatalogItemsPerPage2 = _interopRequireDefault(_CourseCatalogItemsPerPage);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var CourseCatalog = (_temp = _class = function (_Component) {
    _inherits(CourseCatalog, _Component);

    function CourseCatalog(props) {
        _classCallCheck(this, CourseCatalog);

        // Pages of catalog items
        var _this = _possibleConstructorReturn(this, (CourseCatalog.__proto__ || Object.getPrototypeOf(CourseCatalog)).call(this, props));

        _initialiseProps.call(_this);

        var courseMenu = _this.getCourseMenu();

        _this.state = {
            courseMenu: courseMenu,
            pagePosition: 1,
            staticItemsPerPage: _this.props.itemsPerPage,
            itemsPerPage: _this.props.itemsPerPage
        };

        return _this;
    }

    _createClass(CourseCatalog, [{
        key: 'render',
        value: function render() {
            var courseMenu = this.state.courseMenu;
            var staticItemsPerPage = this.state.staticItemsPerPage;
            var itemsPerPage = this.state.itemsPerPage;
            var courseHeaderTitle = 'Course Catalog';

            return _react2.default.createElement(
                'div',
                { className: 'coursecatalog-content-container' },
                _react2.default.createElement('div', { className: 'coursecatalog-content-bg' }),
                _react2.default.createElement(
                    'div',
                    { className: 'coursecatalog-content-wrapper' },
                    _react2.default.createElement(
                        'div',
                        { className: 'coursecatalog-header-wrapper clear' },
                        _react2.default.createElement(
                            'div',
                            { className: 'coursecatalog-header-title' },
                            courseHeaderTitle
                        ),
                        _react2.default.createElement(
                            'div',
                            { className: 'coursecatalog-search-wrapper' },
                            _react2.default.createElement(
                                'div',
                                { className: 'searchbox-pad-helper' },
                                _react2.default.createElement('input', { className: 'coursecatalog-search-input' })
                            ),
                            _react2.default.createElement(
                                'button',
                                { className: 'btn-course-search' },
                                this.getBtnSearchIcon()
                            )
                        )
                    ),
                    _react2.default.createElement(
                        'div',
                        { className: 'coursecatalog-course-wrapper' },
                        _react2.default.createElement(
                            'div',
                            { className: 'coursecatalog-course-pager clear' },
                            _react2.default.createElement(
                                'div',
                                { className: 'coursecatalog-course-filter' },
                                _react2.default.createElement(
                                    'span',
                                    { className: 'coursecatalog-filter-label' },
                                    'Show'
                                ),
                                _react2.default.createElement(_CourseCatalogItemsPerPage2.default, { courseMenu: courseMenu, itemsPerPage: staticItemsPerPage, handleItemsPerPageChange: this.onItemsPerPageChange })
                            ),
                            _react2.default.createElement(
                                'div',
                                { className: 'coursecatalog-course-paging' },
                                this.getBtnPagePrevHtml(),
                                _react2.default.createElement('span', { className: 'divider' }),
                                this.getBtnPageNextHtml()
                            )
                        ),
                        _react2.default.createElement(
                            'div',
                            { className: 'coursecatalog-table' },
                            _react2.default.createElement('div', { className: 'coursecatalog-border' }),
                            _react2.default.createElement(
                                'div',
                                { className: 'coursecatalog-table-row coursecatalog-table-header' },
                                _react2.default.createElement(
                                    'div',
                                    { className: 'coursecatalog-table-description' },
                                    'Course'
                                ),
                                _react2.default.createElement(
                                    'div',
                                    { className: 'coursecatalog-table-status' },
                                    'Started'
                                )
                            ),
                            this.getCourseMenuHtmlByPagePosition()
                        )
                    )
                )
            );
        }
    }]);

    return CourseCatalog;
}(_react.Component), _class.propTypes = {
    courseMenu: _propTypes2.default.array.isRequired,
    itemsPerPage: _propTypes2.default.number.isRequired
}, _class.defaultProps = {
    courseMenu: [],
    itemsPerPage: 20
}, _initialiseProps = function _initialiseProps() {
    var _this2 = this;

    this.getCourseMenu = function () {
        var courseList = [];
        try {
            console.log(_this2.props);
            var _iteratorNormalCompletion = true;
            var _didIteratorError = false;
            var _iteratorError = undefined;

            try {
                for (var _iterator = _this2.props.coursemenu[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
                    var coursemenu = _step.value;

                    console.log(coursemenu);
                    var _iteratorNormalCompletion2 = true;
                    var _didIteratorError2 = false;
                    var _iteratorError2 = undefined;

                    try {
                        for (var _iterator2 = coursemenu.ActivityItems[Symbol.iterator](), _step2; !(_iteratorNormalCompletion2 = (_step2 = _iterator2.next()).done); _iteratorNormalCompletion2 = true) {
                            var item = _step2.value;

                            console.log(item);
                            courseList.push({
                                title: item.Activity.Name,
                                isCompleted: item.Result && item.Result.CompletionStatus == 3,
                                isIncomplete: item.Result && item.Result.CompletionStatus != 3,
                                isRequired: item.Required,
                                description: item.Activity.Description ? item.Activity.Description : "",
                                activityId: item.Activity.Id
                            });
                        }
                    } catch (err) {
                        _didIteratorError2 = true;
                        _iteratorError2 = err;
                    } finally {
                        try {
                            if (!_iteratorNormalCompletion2 && _iterator2.return) {
                                _iterator2.return();
                            }
                        } finally {
                            if (_didIteratorError2) {
                                throw _iteratorError2;
                            }
                        }
                    }
                }
            } catch (err) {
                _didIteratorError = true;
                _iteratorError = err;
            } finally {
                try {
                    if (!_iteratorNormalCompletion && _iterator.return) {
                        _iterator.return();
                    }
                } finally {
                    if (_didIteratorError) {
                        throw _iteratorError;
                    }
                }
            }
        } catch (e) {}
        return courseList;
    };

    this.getBtnSearchIcon = function () {
        var result = _react2.default.createElement(
            'svg',
            { fill: '#444444', height: '24', viewBox: '0 0 24 24', width: '24', xmlns: 'http://www.w3.org/2000/svg' },
            _react2.default.createElement('path', { d: 'M15.5 14h-.79l-.28-.27C15.41 12.59 16 11.11 16 9.5 16 5.91 13.09 3 9.5 3S3 5.91 3 9.5 5.91 16 9.5 16c1.61 0 3.09-.59 4.23-1.57l.27.28v.79l5 4.99L20.49 19l-4.99-5zm-6 0C7.01 14 5 11.99 5 9.5S7.01 5 9.5 5 14 7.01 14 9.5 11.99 14 9.5 14z' }),
            _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' })
        );

        return result;
    };

    this.getCourseMenuHtmlByPagePosition = function () {
        var pagePosition = _this2.state.pagePosition;
        var itemsPerPage = _this2.state.itemsPerPage;

        var courseMenu = _this2.state.courseMenu;
        var itemCount = courseMenu.length;
        var startPosition = pagePosition === 1 ? 1 : itemsPerPage * (pagePosition - 1) + 1;
        var stopPosition = itemsPerPage * pagePosition;
        stopPosition = itemCount < stopPosition ? itemCount : stopPosition;

        var htmlArr = [];
        var html = "";

        for (var x = startPosition; x <= stopPosition; x++) {
            var item = courseMenu[x - 1];
            html = _react2.default.createElement(_CourseCatalogItem2.default, { item: item, key: x });
            htmlArr.push(html);
        }

        return htmlArr;
    };

    this.getBtnPagePrevHtml = function () {
        var defaultClass = 'btn-coursecatalog-page btn-coursecatalog-page-prev';
        var pagePosition = _this2.state.pagePosition;

        var newClass = pagePosition === 1 ? defaultClass + ' disabled' : '' + defaultClass;
        var isDisabled = pagePosition === 1 ? true : false;

        var html = _react2.default.createElement(
            'button',
            { className: newClass, onClick: _this2.onBtnPreviousClick, disabled: isDisabled },
            'Previous'
        );

        return html;
    };

    this.getBtnPageNextHtml = function () {
        var defaultClass = 'btn-coursecatalog-page btn-coursecatalog-page-next';
        var pagePosition = _this2.state.pagePosition;
        var courseMenu = _this2.state.courseMenu,
            itemCount = courseMenu.length;
        var itemsPerPage = _this2.state.itemsPerPage;
        var maxPages = itemCount === 0 ? 1 : Math.ceil(itemCount / itemsPerPage);

        var newClass = pagePosition === maxPages ? defaultClass + ' disabled' : '' + defaultClass;
        var isDisabled = pagePosition === maxPages ? true : false;

        var html = _react2.default.createElement(
            'button',
            { className: newClass, onClick: _this2.onBtnNextClick, disabled: isDisabled },
            'Next'
        );

        return html;
    };

    this.onItemsPerPageChange = function (itemsPerPage) {
        _this2.setState({
            pagePosition: 1,
            itemsPerPage: itemsPerPage
        });
    };

    this.onBtnPreviousClick = function () {
        _this2.setState({
            pagePosition: _this2.state.pagePosition - 1
        });
    };

    this.onBtnNextClick = function () {
        _this2.setState({
            pagePosition: _this2.state.pagePosition + 1
        });
    };
}, _temp);
exports.default = CourseCatalog;