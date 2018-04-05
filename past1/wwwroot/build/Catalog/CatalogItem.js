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

var CatalogItem = (_temp = _class = function (_Component) {
    _inherits(CatalogItem, _Component);

    function CatalogItem(props) {
        _classCallCheck(this, CatalogItem);

        var _this = _possibleConstructorReturn(this, (CatalogItem.__proto__ || Object.getPrototypeOf(CatalogItem)).call(this, props));

        _this.componentDidUpdate = function () {
            var descLeft = _this.getDescriptionPosition(_this.props.width, _this.props.itemOrder, _this.catalogItemWrapper.offsetLeft);

            if (_this.state.descLeft != descLeft) {
                _this.setState({
                    descLeft: descLeft,
                    width: _this.props.width,
                    catalogHeight: _this.props.catalogHeight
                });
            }
        };

        _this.getItemTitle = function () {
            var title = _this.props.item.title;

            var result = !title ? "" : title;

            return result;
        };

        _this.getItemDescription = function () {
            var description = _this.props.item.description;

            var result = !description ? "" : description;

            return result;
        };

        _this.getIsItemCompleted = function () {
            var isCompleted = _this.props.item.isCompleted;

            var result = !isCompleted ? false : isCompleted;

            return result;
        };

        _this.getIsItemInComplete = function () {
            var isIncomplete = _this.props.item.isIncomplete;

            var result = !isIncomplete ? false : isIncomplete;

            return result;
        };

        _this.getIsItemRequired = function () {
            var isRequired = _this.props.item.isRequired;

            var result = !isRequired ? false : isRequired;

            return result;
        };

        _this.getIsItemNotStarted = function () {
            var isCompleted = _this.getIsItemCompleted();
            var isIncomplete = _this.getIsItemInComplete();

            // Has not been start if not completed and not incomplete
            var result = isCompleted === false && isIncomplete === false ? true : false;
            return result;
        };

        _this.getDescriptionPosition = function (width, itemOrder, offsetLeft) {
            var column = itemOrder % 4;
            var groupOrder = 0;

            var newLeft = 0;
            switch (column) {
                case 0:
                    // place in column 3
                    newLeft = offsetLeft - width;
                    break;
                case 1:
                    // place in column 2
                    newLeft = offsetLeft + width;
                    break;
                case 2:
                    // place in column 3
                    newLeft = offsetLeft + width;
                    break;
                case 3:
                    // place in column 2
                    newLeft = offsetLeft - width;
                    break;
            }

            return newLeft;
        };

        _this.isBlank = function () {
            var title = _this.getItemTitle(),
                description = _this.getItemDescription();

            var isBlank = title === '' && description === '' ? true : false;
            return isBlank;
        };

        _this.handleLaunchCourse = function () {
            var activityId = _this.props.item.activityId;

            window.open('/core/user_activity_info.aspx?start=true&id=' + activityId);
        };

        _this.state = {
            offsetLeft: 0,
            descLeft: 0,
            itemOrder: _this.props.itemOrder,
            width: _this.props.width,
            catalogHeight: _this.props.catalogHeight
        };
        return _this;
    }

    _createClass(CatalogItem, [{
        key: 'render',
        value: function render() {
            var _this2 = this;

            var isNotStarted = this.getIsItemNotStarted();
            var isCompleted = this.getIsItemCompleted();
            var isIncomplete = this.getIsItemInComplete();
            var isRequired = this.getIsItemRequired();
            var title = this.getItemTitle();
            var description = this.getItemDescription();
            var isBlank = this.isBlank();

            var width = this.state.width;
            var itemOrder = this.state.itemOrder;
            var descHeight = this.state.catalogHeight;
            var descLeft = this.state.descLeft;

            return _react2.default.createElement(
                'div',
                { className: 'catalog-item-wrapper', style: { width: width + 'px' }, itemorder: itemOrder, ref: function ref(catalogItemWrapper) {
                        _this2.catalogItemWrapper = catalogItemWrapper;
                    } },
                _react2.default.createElement(
                    'div',
                    { className: 'catalog-item-content clear' },
                    !isBlank ? _react2.default.createElement(
                        'div',
                        null,
                        _react2.default.createElement(
                            'div',
                            { className: 'catalog-item-status-wrapper' },
                            isNotStarted ? _react2.default.createElement('div', { className: 'catalog-item-status status-notstarted' }) : isCompleted ? _react2.default.createElement(
                                'div',
                                { className: 'catalog-item-status status-completed' },
                                _react2.default.createElement(
                                    'svg',
                                    { fill: '#000000', opacity: '.8', height: '21', viewBox: '0 0 24 24', width: '21', xmlns: 'http://www.w3.org/2000/svg' },
                                    _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' }),
                                    _react2.default.createElement('path', { d: 'M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z' })
                                )
                            ) : isIncomplete ? _react2.default.createElement(
                                'div',
                                { className: 'catalog-item-status status-incomplete' },
                                _react2.default.createElement('div', { className: 'status-incomplete-front' }),
                                _react2.default.createElement('div', { className: 'status-incomplete-back' })
                            ) : null
                        ),
                        _react2.default.createElement(
                            'div',
                            { className: 'catalog-item-title-wrapper' },
                            _react2.default.createElement(
                                'div',
                                { className: 'catalog-item-title' },
                                title
                            )
                        ),
                        isRequired ? _react2.default.createElement(
                            'div',
                            { className: 'catalog-item-required-wrapper' },
                            _react2.default.createElement(
                                'div',
                                { className: 'catalog-item-required' },
                                _react2.default.createElement(
                                    'svg',
                                    { fill: '#D60000', height: '36', viewBox: '0 0 24 24', width: '36', xmlns: 'http://www.w3.org/2000/svg' },
                                    _react2.default.createElement('path', { d: 'M0 0h24v24H0z', fill: 'none' }),
                                    _react2.default.createElement('path', { d: 'M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z' })
                                )
                            )
                        ) : null,
                        _react2.default.createElement(
                            'div',
                            { className: 'catalog-item-action-wrapper' },
                            _react2.default.createElement(
                                'button',
                                { className: 'btn-launch-catalog', onClick: this.handleLaunchCourse },
                                'Launch Course'
                            )
                        )
                    ) : null
                ),
                !isBlank ? _react2.default.createElement(
                    'div',
                    { className: 'catalog-item-description', style: { height: descHeight + 'px', width: width + 'px', left: descLeft + 'px' } },
                    _react2.default.createElement(
                        'div',
                        { className: 'catalog-description-content' },
                        _react2.default.createElement(
                            'strong',
                            null,
                            title
                        ),
                        description
                    )
                ) : null
            );
        }
    }]);

    return CatalogItem;
}(_react.Component), _class.propTypes = {
    item: _propTypes2.default.shape({
        title: _propTypes2.default.string.isRequired,
        description: _propTypes2.default.string.isRequired,
        isCompleted: _propTypes2.default.bool.isRequired,
        isIncomplete: _propTypes2.default.bool.isRequired,
        isRequired: _propTypes2.default.bool.isRequired
    }),
    itemOrder: _propTypes2.default.number.isRequired,
    width: _propTypes2.default.number.isRequired,
    catalogHeight: _propTypes2.default.number.isRequired
}, _temp);
exports.default = CatalogItem;