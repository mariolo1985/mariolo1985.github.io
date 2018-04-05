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

var _CatalogItem = require('../Catalog/CatalogItem');

var _CatalogItem2 = _interopRequireDefault(_CatalogItem);

var _PageIndicator = require('../Catalog/PageIndicator');

var _PageIndicator2 = _interopRequireDefault(_PageIndicator);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var Catalog = (_temp = _class = function (_Component) {
    _inherits(Catalog, _Component);

    function Catalog(props) {
        _classCallCheck(this, Catalog);

        // Pages of catalog items
        var _this = _possibleConstructorReturn(this, (Catalog.__proto__ || Object.getPrototypeOf(Catalog)).call(this, props));

        _initialiseProps.call(_this);

        var courseList = _this.getCourseList();
        var courseItemCount = courseList.length;
        var pageCount = courseItemCount === 0 ? 1 : courseItemCount / 8;
        pageCount = Math.ceil(pageCount);

        var catalogPageWidth = window.innerWidth;
        var catalogWidth = catalogPageWidth * pageCount;
        var itemWidth = catalogPageWidth / 4;

        _this.state = {
            courseList: courseList,
            catalogWidth: catalogWidth,
            catalogHeight: 0,
            catalogItemWidth: itemWidth,
            catalogPageWidth: catalogPageWidth,
            catalogLeftPos: 0, // page absolute position
            catalogPageCount: pageCount,
            catalogPageNum: 1,
            resizeWaitTime: 50,
            itemsPerPage: _this.props.itemsPerPage
        };

        _this.resizeTimeout;
        return _this;
    }

    _createClass(Catalog, [{
        key: 'render',
        value: function render() {
            var catalogWidth = this.state.catalogWidth,
                catalogHeight = this.state.catalogHeight,
                catalogLeftPos = this.state.catalogLeftPos;

            return _react2.default.createElement(
                'div',
                { className: 'catalog-content-container' },
                _react2.default.createElement(
                    'div',
                    { className: 'catalog-parent' },
                    _react2.default.createElement(
                        'div',
                        { className: 'catalog-container clear', style: { width: catalogWidth + 'px', height: catalogHeight + 'px', left: catalogLeftPos + 'px' } },

                        // includes blank items
                        this.getCatalogItemComponents()
                    )
                ),
                _react2.default.createElement(
                    'div',
                    { className: 'catalog-paging-wrapper' },
                    _react2.default.createElement(
                        'button',
                        { className: this.getBtnPageLeftClass(), onClick: this.onPageLeftClick, disabled: this.getIsBtnPageLeftDisabled() },
                        _react2.default.createElement(
                            'svg',
                            { fill: '#444444', height: '18', viewBox: '0 0 24 24', width: '18' },
                            _react2.default.createElement('path', { d: 'M15.41 7.41L14 6l-6 6 6 6 1.41-1.41L10.83 12z' }),
                            _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' })
                        )
                    ),
                    this.getPageIndicatorHtml(),
                    _react2.default.createElement(
                        'button',
                        { className: this.getBtnPageRightClass(), onClick: this.onPageRightClick, disabled: this.getIsBtnPageRightDisabled() },
                        _react2.default.createElement(
                            'svg',
                            { fill: '#444444', height: '18', viewBox: '0 0 24 24', width: '18' },
                            _react2.default.createElement('path', { d: 'M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z' }),
                            _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' })
                        )
                    )
                )
            );
        }
    }]);

    return Catalog;
}(_react.Component), _class.propTypes = {
    itemsPerPage: _propTypes2.default.number.isRequired,
    coursemenu: _propTypes2.default.array.isRequired
}, _initialiseProps = function _initialiseProps() {
    var _this2 = this;

    this.componentDidMount = function () {
        _this2.getCatalogSize();
        window.addEventListener('resize', _this2.resizeThrottle);
    };

    this.componentWillUnmount = function () {
        window.removeEventListener('resize', _this2.resizeThrottle);
    };

    this.resizeThrottle = function () {
        if (!_this2.resizeTimeout) {
            var context = _this2;
            _this2.resizeTimeout = setTimeout(function () {
                context.resizeTimeout = null;
                context.getCatalogSize();
            }, context.state.resizeWaitTime);
        }
    };

    this.getCourseList = function () {
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

    this.getCatalogSize = function () {
        // get catalog width
        var courseList = _this2.state.courseList;
        var courseCount = courseList.length;
        var pageCount = courseCount / 8; // 8 per page
        pageCount = Math.ceil(pageCount);

        // get catalog item width
        var catalogPageWidth = window.innerWidth;
        var catalogWidth = pageCount * catalogPageWidth;
        var catalogItemWidth = catalogPageWidth / 4; // 4 per row

        var catalogItems = document.getElementsByClassName('catalog-item-wrapper');
        var catalogItem = catalogItems[0];
        var catalogItemHeight = catalogItem.clientHeight;
        var catalogHeight = catalogItemHeight * 2;

        // set catalog width state to update
        _this2.setState({
            catalogWidth: catalogWidth,
            catalogHeight: catalogHeight,
            catalogItemWidth: catalogItemWidth,
            catalogPageWidth: catalogPageWidth
        });
    };

    this.getPageIndicatorHtml = function () {
        var pageCount = _this2.state.catalogPageCount;
        var htmlArr = [];
        for (var x = 0; x < pageCount; x++) {
            var page = x + 1;
            var currPage = _this2.state.catalogPageNum;
            htmlArr.push(_react2.default.createElement(_PageIndicator2.default, { page: page, key: x, currPage: currPage, onPageIndicatorClick: _this2.handlePageIndicatorClick }));
        }
        return htmlArr;
    };

    this.getBlankItemHtml = function (itemPerPage, itemCount, catalogItemWidth) {
        var htmlArr = [];
        var masterItemPerPage = itemPerPage;
        while (itemPerPage < itemCount) {
            itemPerPage += masterItemPerPage;
        }

        var keyStart = itemCount;

        var additionalItemCount = 0;
        if (itemCount <= itemPerPage) {
            additionalItemCount = itemPerPage - itemCount;
        } else {
            additionalItemCount = itemPerPage % itemCount;
        }
        if (additionalItemCount > 0) {
            for (var x = 0; x < additionalItemCount; x++) {
                var blankObj = {
                    title: '',
                    isCompleted: false,
                    isIncomplete: false,
                    isRequired: false,
                    description: ''
                };
                var key = keyStart + x;
                htmlArr.push(_react2.default.createElement(_CatalogItem2.default, { item: blankObj, key: key, width: catalogItemWidth, itemOrder: key + 1, catalogHeight: _this2.state.catalogHeight }));
            }
        }

        return htmlArr;
    };

    this.getCatalogItemComponents = function () {
        var courseList = _this2.state.courseList,
            catalogItemWidth = _this2.state.catalogItemWidth;

        var itemArr = [];
        courseList.map(function (item, i) {
            itemArr.push(_react2.default.createElement(_CatalogItem2.default, { key: i, item: item, width: catalogItemWidth, itemOrder: i + 1, catalogHeight: _this2.state.catalogHeight }));
        });

        // if any, add blank item tiles
        var blankItemArr = _this2.getBlankItemHtml(_this2.state.itemsPerPage, itemArr.length, catalogItemWidth);
        blankItemArr.map(function (item) {
            itemArr.push(item);
        });

        var htmlArr = [];
        var itemGroup;
        var groupCount = 1;
        var groupWidth = catalogItemWidth * 4;
        while (itemArr.length > 0) {
            itemGroup = _react2.default.createElement(
                'div',
                { className: 'catalog-item-group clear', key: groupCount, style: { width: groupWidth + 'px' } },
                itemArr.map(function (item, index) {

                    while (index < 8) {
                        {
                            return item;
                        }
                    }
                })
            );
            itemArr.splice(0, 8);
            htmlArr.push(itemGroup);
            groupCount++;
        }

        return htmlArr;
    };

    this.getIsBtnPageLeftDisabled = function () {
        var pageWidth = _this2.state.catalogPageWidth;
        var currLeftPos = _this2.state.catalogLeftPos;
        var isBtnDisabled = currLeftPos === 0 ? true : false;

        return isBtnDisabled;
    };

    this.getBtnPageLeftClass = function () {
        var defaultClass = "btn-catalog-page btn-catalog-page-left";
        var isLeftBounds = _this2.getIsBtnPageLeftDisabled();
        var btnClass = isLeftBounds ? defaultClass + ' disable' : defaultClass;

        return btnClass;
    };

    this.getIsBtnPageRightDisabled = function () {
        var catalogWidth = _this2.state.catalogWidth;
        var pageWidth = _this2.state.catalogPageWidth;
        var currLeftPos = _this2.state.catalogLeftPos;
        var isBtnDisabled = currLeftPos - pageWidth <= catalogWidth * -1 ? true : false;

        return isBtnDisabled;
    };

    this.getBtnPageRightClass = function () {
        var defaultClass = "btn-catalog-page btn-catalog-page-right";
        var isRightBounds = _this2.getIsBtnPageRightDisabled();
        var btnClass = isRightBounds ? defaultClass + ' disable' : defaultClass;

        return btnClass;
    };

    this.gotoPage = function (pageNum) {
        var pageWidth = _this2.state.catalogPageWidth;
        var pagePos;
        if (pageNum === 1) {
            pagePos = 0;
        } else {
            pagePos = (pageNum - 1) * (pageWidth * -1);
        }

        _this2.setState({
            catalogPageNum: pageNum,
            catalogLeftPos: pagePos
        });
    };

    this.onPageLeftClick = function () {
        var pageWidth = _this2.state.catalogPageWidth;
        var currLeftPos = _this2.state.catalogLeftPos;
        var newLeftPos = currLeftPos + pageWidth;

        var currPage = _this2.state.catalogPageNum - 1;
        if (newLeftPos <= 0) {
            _this2.setState({
                catalogLeftPos: newLeftPos,
                catalogPageNum: currPage
            });
        }
    };

    this.onPageRightClick = function () {
        var catalogWidth = _this2.state.catalogWidth;
        var pageWidth = _this2.state.catalogPageWidth;
        var currLeftPos = _this2.state.catalogLeftPos;
        var newLeftPos = currLeftPos - pageWidth;

        var currPage = _this2.state.catalogPageNum + 1;
        if (newLeftPos > catalogWidth * -1) {
            _this2.setState({
                catalogLeftPos: newLeftPos,
                catalogPageNum: currPage
            });
        }
    };

    this.handlePageIndicatorClick = function (pageNum) {
        _this2.gotoPage(pageNum);
    };
}, _temp);
exports.default = Catalog;