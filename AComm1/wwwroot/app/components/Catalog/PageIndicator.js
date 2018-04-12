import React, { Component } from 'react';
import PropTypes from 'prop-types';

class PageIndicator extends Component {
    constructor(props) {
        super(props);

        this.state = {
            pageNumber: this.props.page,
            currPageNumber: this.props.currPage
        }
    }

    componentWillReceiveProps = (nextProps) => {
        this.setState({
            currPageNumber: nextProps.currPage
        });
    }

    static PropTypes = {
        page: PropTypes.number.isRequired,
        currPage: PropTypes.number.isRequired
    }

    getPageNumber = () => {
        const result = this.state.pageNumber;
        return result;
    }

    getCurrPageNumber = () => {
        const result = this.state.currPageNumber;

        return result;
    }

    getIsBtnCurrPage = () => {
        const pageNum = this.getPageNumber();
        const currPageNum = this.getCurrPageNumber();
        const result = pageNum === currPageNum;

        return result;
    }

    getBtnClass = () => {
        const defaultClass = 'btn-catalog-page-indicator';
        const isBtnCurrPage = this.getIsBtnCurrPage();
        const pageClass = isBtnCurrPage ? `${defaultClass} current` : `${defaultClass}`;

        return pageClass;
    }

    onPageIndicatorClick = () => {
        const isCurrPage = this.getIsBtnCurrPage();

        if (!isCurrPage) {
            this.props.onPageIndicatorClick(this.getPageNumber());
        }
    }

    render() {

        return (
            <button className={this.getBtnClass()} page={this.getPageNumber()} onClick={this.onPageIndicatorClick} disabled={this.getIsBtnCurrPage()}>
                {
                    this.getIsBtnCurrPage() ?
                        <svg className='page-current' fill="#444444" height="18" viewBox="0 0 24 24" width="18">
                            <circle cx="12" cy="12" r="10" />
                            <path d="M0 0h24v24H0z" fill="none" />
                        </svg>
                        :
                        <svg className='page-other' fill="#444444" height="18" viewBox="0 0 24 24" width="18">
                            <path d="M0 0h24v24H0z" fill="none" />
                            <path d="M12 2C6.47 2 2 6.47 2 12s4.47 10 10 10 10-4.47 10-10S17.53 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z" />
                        </svg>
                }
            </button>
        )
    }
}

export default PageIndicator;