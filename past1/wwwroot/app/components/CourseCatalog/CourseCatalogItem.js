import React, { Component } from 'react';
import PropTypes from 'prop-types';

class CourseCatalogItem extends Component {


    static propTypes = {
        item: PropTypes.shape({
            title: PropTypes.string.isRequired,
            description: PropTypes.string.isRequired,
            isCompleted: PropTypes.bool.isRequired,
            isIncomplete: PropTypes.bool.isRequired
        })
    }

    getItemTitle = () => {
        const { title } = this.props.item;
        const result = !title ? "" : title;

        return result;
    }

    getItemDescription = () => {
        const { description } = this.props.item;
        const result = !description ? "" : description;

        return result;
    }

    // Get item statuses
    getIsItemCompleted = () => {
        const { isCompleted } = this.props.item;
        const result = !isCompleted ? false : isCompleted;

        return result;
    }

    getIsItemInComplete = () => {
        const { isIncomplete } = this.props.item;
        const result = !isIncomplete ? false : isIncomplete;

        return result;
    }

    getIsItemNotStarted = () => {
        const isCompleted = this.getIsItemCompleted();
        const isIncomplete = this.getIsItemInComplete();

        // Has not been start if not completed and not incomplete
        const result = ((isCompleted === false) && (isIncomplete === false)) ? true : false;
        return result;
    }

    getIsItemHighlighted = () => {
        const result = true; // to do - when is an item hightlighted?

        return result;
    }

    // Course Icons
    getCourseIconClass = () => {
        const defaultClass = 'course-icon';
        const isHighlighted = this.getIsItemHighlighted();
        const result = isHighlighted ? `${defaultClass} highlight` : defaultClass;

        return result;
    }
    getCourseIcon1 = () => {
        const icon = (
            <svg fill="#444444" height="36" viewBox="0 0 24 24" width="36" xmlns="http://www.w3.org/2000/svg">
                <path d="M0 0h24v24H0z" fill="none" />
                <path d="M19 3h-4.18C14.4 1.84 13.3 1 12 1c-1.3 0-2.4.84-2.82 2H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm-7 0c.55 0 1 .45 1 1s-.45 1-1 1-1-.45-1-1 .45-1 1-1zm2 14H7v-2h7v2zm3-4H7v-2h10v2zm0-4H7V7h10v2z" />
            </svg>
        );

        return icon;
    }
    getCourseIcon2 = () => {
        const icon = (
            <svg fill="#444444" height="36" viewBox="0 0 24 24" width="36" xmlns="http://www.w3.org/2000/svg">
                <path d="M0 0h24v24H0V0z" fill="none" />
                <path d="M23 11.01L18 11c-.55 0-1 .45-1 1v9c0 .55.45 1 1 1h5c.55 0 1-.45 1-1v-9c0-.55-.45-.99-1-.99zM23 20h-5v-7h5v7zM20 2H2C.89 2 0 2.89 0 4v12c0 1.1.89 2 2 2h7v2H7v2h8v-2h-2v-2h2v-2H2V4h18v5h2V4c0-1.11-.9-2-2-2zm-8.03 7L11 6l-.97 3H7l2.47 1.76-.94 2.91 2.47-1.8 2.47 1.8-.94-2.91L15 9h-3.03z" />
            </svg>
        );

        return icon;
    }
    getCourseIcon3 = () => {
        const icon = (
            <svg fill="#444444" height="36" viewBox="0 0 24 24" width="36" xmlns="http://www.w3.org/2000/svg">
                <path d="M0 0h24v24H0z" fill="none" />
                <path d="M11.99 2C6.47 2 2 6.48 2 12s4.47 10 9.99 10C17.52 22 22 17.52 22 12S17.52 2 11.99 2zm6.93 6h-2.95c-.32-1.25-.78-2.45-1.38-3.56 1.84.63 3.37 1.91 4.33 3.56zM12 4.04c.83 1.2 1.48 2.53 1.91 3.96h-3.82c.43-1.43 1.08-2.76 1.91-3.96zM4.26 14C4.1 13.36 4 12.69 4 12s.1-1.36.26-2h3.38c-.08.66-.14 1.32-.14 2 0 .68.06 1.34.14 2H4.26zm.82 2h2.95c.32 1.25.78 2.45 1.38 3.56-1.84-.63-3.37-1.9-4.33-3.56zm2.95-8H5.08c.96-1.66 2.49-2.93 4.33-3.56C8.81 5.55 8.35 6.75 8.03 8zM12 19.96c-.83-1.2-1.48-2.53-1.91-3.96h3.82c-.43 1.43-1.08 2.76-1.91 3.96zM14.34 14H9.66c-.09-.66-.16-1.32-.16-2 0-.68.07-1.35.16-2h4.68c.09.65.16 1.32.16 2 0 .68-.07 1.34-.16 2zm.25 5.56c.6-1.11 1.06-2.31 1.38-3.56h2.95c-.96 1.65-2.49 2.93-4.33 3.56zM16.36 14c.08-.66.14-1.32.14-2 0-.68-.06-1.34-.14-2h3.38c.16.64.26 1.31.26 2s-.1 1.36-.26 2h-3.38z" />
            </svg>
        );

        return icon;
    }

    // Status Icons
    getCompletedStatusIcon = () => {
        const result = (
            <div className='coursecatalog-item-status status-completed'>
            </div>
        );
        return result;
    }

    getIncompleteStatusIcon = () => {
        const result = (
            <div className='coursecatalog-item-status status-incomplete'>
                <div className='status-incomplete-front'>
                </div>
                <div className='status-incomplete-back'>
                </div>
            </div>
        );

        return result;
    }

    getNotStartedStatusIcon = () => {
        const result = (
            <div className='coursecatalog-item-status status-notstarted'>
            </div>
        );

        return result;
    }

    render() {

        const title = this.getItemTitle();
        const description = this.getItemDescription();

        const isNotStarted = this.getIsItemNotStarted();
        const isCompleted = this.getIsItemCompleted();
        const isInComplete = this.getIsItemInComplete();

        const courseIcon1 = this.getCourseIcon1();
        const courseIcon2 = this.getCourseIcon2();
        const courseIcon3 = this.getCourseIcon3();

        return (
            <div className='coursecatalog-table-row coursecatalog-item'>
                <div className='coursecatalog-table-description'>
                    <span className={this.getCourseIconClass()}>
                        {courseIcon1}
                    </span>
                    <span className='course-title'>
                        {title}
                    </span>
                    <span className='course-title-divider'>:</span>
                    <span className='course-description'>
                        {description}
                    </span>
                </div>
                <div className='coursecatalog-table-status'>
                    <span class='course-status'>
                        {
                            isNotStarted ?
                                this.getNotStartedStatusIcon()
                                : isCompleted ? this.getCompletedStatusIcon() :
                                    isInComplete ? this.getIncompleteStatusIcon() : null
                        }
                    </span>
                </div>
            </div>

        )
    }
}

export default CourseCatalogItem;