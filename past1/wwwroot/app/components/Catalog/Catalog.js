import React, { Component } from 'react';
import PropTypes from 'prop-types';
import CatalogItem from '../Catalog/CatalogItem';
import PageIndicator from '../Catalog/PageIndicator';


class Catalog extends Component {
    constructor(props) {
        super(props);

        // Pages of catalog items
        const courseList = this.getCourseList();
        const courseItemCount = courseList.length;
        var pageCount = courseItemCount === 0 ? 1 : courseItemCount / 8;
        pageCount = Math.ceil(pageCount);

        const catalogPageWidth = window.innerWidth;
        const catalogWidth = catalogPageWidth * pageCount;
        const itemWidth = catalogPageWidth / 4;

        this.state = {
            courseList: courseList,
            catalogWidth: catalogWidth,
            catalogHeight: 0,
            catalogItemWidth: itemWidth,
            catalogPageWidth: catalogPageWidth,
            catalogLeftPos: 0,// page absolute position
            catalogPageCount: pageCount,
            catalogPageNum: 1,
            resizeWaitTime: 50,
            itemsPerPage: this.props.itemsPerPage
        }

        this.resizeTimeout;
    }

    static propTypes = {
        itemsPerPage: PropTypes.number.isRequired,
        coursemenu: PropTypes.array.isRequired
    }

    componentDidMount = () => {
        this.getCatalogSize();
        window.addEventListener('resize', this.resizeThrottle);
    }

    componentWillUnmount = () => {
        window.removeEventListener('resize', this.resizeThrottle);
    }

    resizeThrottle = () => {
        if (!this.resizeTimeout) {
            var context = this;
            this.resizeTimeout = setTimeout(function () {
                context.resizeTimeout = null;
                context.getCatalogSize();
            }, context.state.resizeWaitTime);
        }
    }

    getCourseList = () => {
        let courseList = [];
        try {
            console.log(this.props);
            for (let coursemenu of this.props.coursemenu) {
                console.log(coursemenu);
                for (let item of coursemenu.ActivityItems) {
                    console.log(item);
                    courseList.push({
                        title: item.Activity.Name,
                        isCompleted: (item.Result && item.Result.CompletionStatus == 3),
                        isIncomplete: (item.Result && item.Result.CompletionStatus != 3),
                        isRequired: item.Required,
                        description: item.Activity.Description ? item.Activity.Description : "",
                        activityId: item.Activity.Id
                    })
                }

            }
        } catch (e) { }
        return courseList;
    }

    getCatalogSize = () => {
        // get catalog width
        const courseList = this.state.courseList;
        const courseCount = courseList.length;
        var pageCount = courseCount / 8;// 8 per page
        pageCount = Math.ceil(pageCount);

        // get catalog item width
        const catalogPageWidth = window.innerWidth;
        const catalogWidth = pageCount * catalogPageWidth;
        const catalogItemWidth = catalogPageWidth / 4;// 4 per row

        const catalogItems = document.getElementsByClassName('catalog-item-wrapper');
        const catalogItem = catalogItems[0];
        const catalogItemHeight = catalogItem.clientHeight;
        const catalogHeight = catalogItemHeight * 2;

        // set catalog width state to update
        this.setState({
            catalogWidth: catalogWidth,
            catalogHeight: catalogHeight,
            catalogItemWidth: catalogItemWidth,
            catalogPageWidth: catalogPageWidth
        });
    }

    getPageIndicatorHtml = () => {
        const pageCount = this.state.catalogPageCount;
        var htmlArr = [];
        for (var x = 0; x < pageCount; x++) {
            var page = x + 1;
            const currPage = this.state.catalogPageNum;
            htmlArr.push(<PageIndicator page={page} key={x} currPage={currPage} onPageIndicatorClick={this.handlePageIndicatorClick} />);
        }
        return htmlArr;
    }

    getBlankItemHtml = (itemPerPage, itemCount, catalogItemWidth) => {
        var htmlArr = [];
        var masterItemPerPage = itemPerPage;
        while (itemPerPage < itemCount) {
            itemPerPage += masterItemPerPage;
        }

        const keyStart = itemCount;

        var additionalItemCount = 0;
        if (itemCount <= itemPerPage) {
            additionalItemCount = itemPerPage - itemCount;
        } else {
            additionalItemCount = itemPerPage % itemCount;
        }
        if (additionalItemCount > 0) {
            for (var x = 0; x < additionalItemCount; x++) {
                const blankObj = {
                    title: '',
                    isCompleted: false,
                    isIncomplete: false,
                    isRequired: false,
                    description: ''
                }
                const key = keyStart + x;
                htmlArr.push(<CatalogItem item={blankObj} key={key} width={catalogItemWidth} itemOrder={key + 1} catalogHeight={this.state.catalogHeight} />)
            }
        }

        return htmlArr;
    }

    getCatalogItemComponents = () => {
        const courseList = this.state.courseList,
            catalogItemWidth = this.state.catalogItemWidth;

        var itemArr = [];
        courseList.map((item, i) => {
            itemArr.push(<CatalogItem key={i} item={item} width={catalogItemWidth} itemOrder={i + 1} catalogHeight={this.state.catalogHeight} />);
        });

        // if any, add blank item tiles
        var blankItemArr = this.getBlankItemHtml(this.state.itemsPerPage, itemArr.length, catalogItemWidth);
        blankItemArr.map((item) => {
            itemArr.push(item);
        });

        var htmlArr = [];
        var itemGroup;
        var groupCount = 1;
        const groupWidth = catalogItemWidth * 4;
        while (itemArr.length > 0) {
            itemGroup = (
                <div className='catalog-item-group clear' key={groupCount} style={{ width: groupWidth + 'px' }}>
                    {
                        itemArr.map((item, index) => {

                            while (index < 8) {
                                { return (item) }
                            }
                        })
                    }
                </div>
            )
            itemArr.splice(0, 8);
            htmlArr.push(itemGroup);
            groupCount++;
        }

        return htmlArr;

    }

    getIsBtnPageLeftDisabled = () => {
        const pageWidth = this.state.catalogPageWidth;
        const currLeftPos = this.state.catalogLeftPos;
        const isBtnDisabled = currLeftPos === 0 ? true : false;

        return isBtnDisabled;
    }

    getBtnPageLeftClass = () => {
        const defaultClass = "btn-catalog-page btn-catalog-page-left";
        const isLeftBounds = this.getIsBtnPageLeftDisabled();
        const btnClass = isLeftBounds ? `${defaultClass} disable` : defaultClass;

        return btnClass;
    }

    getIsBtnPageRightDisabled = () => {
        const catalogWidth = this.state.catalogWidth;
        const pageWidth = this.state.catalogPageWidth;
        const currLeftPos = this.state.catalogLeftPos;
        const isBtnDisabled = currLeftPos - pageWidth <= (catalogWidth * -1) ? true : false;

        return isBtnDisabled;
    }

    getBtnPageRightClass = () => {
        const defaultClass = "btn-catalog-page btn-catalog-page-right";
        const isRightBounds = this.getIsBtnPageRightDisabled();
        const btnClass = isRightBounds ? `${defaultClass} disable` : defaultClass;

        return btnClass;
    }

    gotoPage = (pageNum) => {
        const pageWidth = this.state.catalogPageWidth;
        var pagePos;
        if (pageNum === 1) {
            pagePos = 0;
        } else {
            pagePos = (pageNum - 1) * (pageWidth * -1);
        }

        this.setState({
            catalogPageNum: pageNum,
            catalogLeftPos: pagePos
        });
    }

    onPageLeftClick = () => {
        const pageWidth = this.state.catalogPageWidth;
        const currLeftPos = this.state.catalogLeftPos;
        const newLeftPos = currLeftPos + pageWidth;

        const currPage = this.state.catalogPageNum - 1;
        if (newLeftPos <= 0) {
            this.setState({
                catalogLeftPos: newLeftPos,
                catalogPageNum: currPage
            })
        }

    }

    onPageRightClick = () => {
        const catalogWidth = this.state.catalogWidth;
        const pageWidth = this.state.catalogPageWidth;
        const currLeftPos = this.state.catalogLeftPos;
        const newLeftPos = currLeftPos - pageWidth;

        const currPage = this.state.catalogPageNum + 1;
        if (newLeftPos > (catalogWidth * -1)) {
            this.setState({
                catalogLeftPos: newLeftPos,
                catalogPageNum: currPage
            })
        }
    }

    handlePageIndicatorClick = (pageNum) => {
        this.gotoPage(pageNum);
    }

    render() {
        const catalogWidth = this.state.catalogWidth,
            catalogHeight = this.state.catalogHeight,
            catalogLeftPos = this.state.catalogLeftPos;

        return (
            <div className='catalog-content-container'>
                <div className='catalog-parent'>
                    <div className='catalog-container clear' style={{ width: catalogWidth + 'px', height: catalogHeight + 'px', left: catalogLeftPos + 'px' }}>
                        {
                            // includes blank items
                            this.getCatalogItemComponents()
                        }
                    </div>
                </div>

                <div className='catalog-paging-wrapper'>
                    <button className={this.getBtnPageLeftClass()} onClick={this.onPageLeftClick} disabled={this.getIsBtnPageLeftDisabled()}>
                        <svg fill="#444444" height="18" viewBox="0 0 24 24" width="18">
                            <path d="M15.41 7.41L14 6l-6 6 6 6 1.41-1.41L10.83 12z" />
                            <path d="M0 0h24v24H0z" fill="none" />
                        </svg>
                    </button>
                    {
                        this.getPageIndicatorHtml()
                    }

                    <button className={this.getBtnPageRightClass()} onClick={this.onPageRightClick} disabled={this.getIsBtnPageRightDisabled()}>
                        <svg fill="#444444" height="18" viewBox="0 0 24 24" width="18">
                            <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" />
                            <path d="M0 0h24v24H0z" fill="none" />
                        </svg>
                    </button>
                </div>
            </div >
        )
    }
}

export default Catalog;