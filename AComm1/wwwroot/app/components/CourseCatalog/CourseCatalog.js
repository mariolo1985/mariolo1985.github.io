import React, { Component } from 'react';
import PropTypes from 'prop-types';
import CourseCatalogItem from '../CourseCatalog/CourseCatalogItem';
import CourseCatalogItemsPerPage from '../CourseCatalog/CourseCatalogItemsPerPage';


class CourseCatalog extends Component {
    constructor(props) {
        super(props);

        // Pages of catalog items
        const courseMenu = this.getCourseMenu();

        this.state = {
            courseMenu: courseMenu,
            pagePosition: 1,
            staticItemsPerPage: this.props.itemsPerPage,
            itemsPerPage: this.props.itemsPerPage
        }

    }

    static propTypes = {
        courseMenu: PropTypes.array.isRequired,
        itemsPerPage: PropTypes.number.isRequired
    }

    static defaultProps = {
        courseMenu: [],
        itemsPerPage: 20
    }

    getCourseMenu = () => {
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

    getBtnSearchIcon = () => {
        const result = (
            <svg fill="#444444" height="24" viewBox="0 0 24 24" width="24" xmlns="http://www.w3.org/2000/svg">
                <path d="M15.5 14h-.79l-.28-.27C15.41 12.59 16 11.11 16 9.5 16 5.91 13.09 3 9.5 3S3 5.91 3 9.5 5.91 16 9.5 16c1.61 0 3.09-.59 4.23-1.57l.27.28v.79l5 4.99L20.49 19l-4.99-5zm-6 0C7.01 14 5 11.99 5 9.5S7.01 5 9.5 5 14 7.01 14 9.5 11.99 14 9.5 14z" />
                <path d="M0 0h24v24H0z" fill="none" />
            </svg>
        );

        return result;
    }

    getCourseMenuHtmlByPagePosition = () => {
        const pagePosition = this.state.pagePosition;
        const itemsPerPage = this.state.itemsPerPage;

        const courseMenu = this.state.courseMenu;
        const itemCount = courseMenu.length;
        const startPosition = pagePosition === 1 ? 1 : (itemsPerPage * (pagePosition - 1)) + 1;
        var stopPosition = itemsPerPage * pagePosition;
        stopPosition = itemCount < stopPosition ? itemCount : stopPosition;


        var htmlArr = [];
        var html = "";


        for (var x = startPosition; x <= stopPosition; x++) {
            var item = courseMenu[x - 1];
            html = (
                <CourseCatalogItem item={item} key={x} />
            );
            htmlArr.push(html);
        }

        return htmlArr;
    }

    getBtnPagePrevHtml = () => {
        const defaultClass = 'btn-coursecatalog-page btn-coursecatalog-page-prev';
        const pagePosition = this.state.pagePosition;

        const newClass = pagePosition === 1 ? `${defaultClass} disabled` : `${defaultClass}`;
        const isDisabled = pagePosition === 1 ? true : false;

        const html = (
            <button className={newClass} onClick={this.onBtnPreviousClick} disabled={isDisabled}>Previous</button>
        );

        return html;
    }

    getBtnPageNextHtml = () => {
        const defaultClass = 'btn-coursecatalog-page btn-coursecatalog-page-next';
        const pagePosition = this.state.pagePosition;
        const courseMenu = this.state.courseMenu,
            itemCount = courseMenu.length;
        const itemsPerPage = this.state.itemsPerPage;
        const maxPages = itemCount === 0 ? 1 : Math.ceil(itemCount / itemsPerPage);

        const newClass = pagePosition === maxPages ? `${defaultClass} disabled` : `${defaultClass}`;
        const isDisabled = pagePosition === maxPages ? true : false;

        const html = (
            <button className={newClass} onClick={this.onBtnNextClick} disabled={isDisabled}>Next</button>
        );

        return html;
    }
    onItemsPerPageChange = (itemsPerPage) => {
        this.setState({
            pagePosition: 1,
            itemsPerPage: itemsPerPage
        })
    }

    onBtnPreviousClick = () => {
        this.setState({
            pagePosition: this.state.pagePosition - 1
        })
    }

    onBtnNextClick = () => {
        this.setState({
            pagePosition: this.state.pagePosition + 1
        })
    }

    render() {
        const courseMenu = this.state.courseMenu;
        const staticItemsPerPage = this.state.staticItemsPerPage;
        const itemsPerPage = this.state.itemsPerPage;
        const courseHeaderTitle = 'Course Catalog';

        return (
            <div className='coursecatalog-content-container'>
                <div className='coursecatalog-content-bg'></div>
                <div className='coursecatalog-content-wrapper'>
                    <div className='coursecatalog-header-wrapper clear'>
                        <div className='coursecatalog-header-title'>
                            {courseHeaderTitle}
                        </div>
                        <div className='coursecatalog-search-wrapper'>
                            <div className='searchbox-pad-helper'>
                                <input className='coursecatalog-search-input'></input>
                            </div>
                            <button className='btn-course-search'>
                                {this.getBtnSearchIcon()}
                            </button>
                        </div>
                    </div>
                    <div className='coursecatalog-course-wrapper'>
                        <div className='coursecatalog-course-pager clear'>
                            <div className='coursecatalog-course-filter'>
                                <span className='coursecatalog-filter-label'>Show</span>
                                <CourseCatalogItemsPerPage courseMenu={courseMenu} itemsPerPage={staticItemsPerPage} handleItemsPerPageChange={this.onItemsPerPageChange} />
                            </div>
                            <div className='coursecatalog-course-paging'>
                                {this.getBtnPagePrevHtml()}
                                <span className='divider' />
                                {this.getBtnPageNextHtml()}
                            </div>
                        </div>
                        <div className='coursecatalog-table'>
                            <div className='coursecatalog-border'></div>

                            <div className='coursecatalog-table-row coursecatalog-table-header'>
                                <div className='coursecatalog-table-description'>Course</div>
                                <div className='coursecatalog-table-status'>Started</div>
                            </div>
                            {
                                this.getCourseMenuHtmlByPagePosition()
                            }

                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

export default CourseCatalog;