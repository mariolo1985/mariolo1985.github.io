import React, { Component } from 'react';
import PropTypes from 'prop-types';


class CatalogItem extends Component {
    constructor(props) {
        super(props);

        this.state = {
            offsetLeft: 0,
            descLeft: 0,
            itemOrder:this.props.itemOrder,
            width: this.props.width,
            catalogHeight: this.props.catalogHeight
        }
    }

    static propTypes = {
        item: PropTypes.shape({
            title: PropTypes.string.isRequired,
            description: PropTypes.string.isRequired,
            isCompleted: PropTypes.bool.isRequired,
            isIncomplete: PropTypes.bool.isRequired,
            isRequired: PropTypes.bool.isRequired,
        }),
        itemOrder: PropTypes.number.isRequired,        
        width: PropTypes.number.isRequired,
        catalogHeight: PropTypes.number.isRequired
    }


    componentDidUpdate = () => {
        const descLeft = this.getDescriptionPosition(this.props.width, this.props.itemOrder, this.catalogItemWrapper.offsetLeft);

        if (this.state.descLeft != descLeft) {
            this.setState({
                descLeft: descLeft,
                width: this.props.width,
                catalogHeight: this.props.catalogHeight
            })
        }
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

    getIsItemRequired = () => {
        const { isRequired } = this.props.item;
        const result = !isRequired ? false : isRequired;

        return result;
    }

    getIsItemNotStarted = () => {
        const isCompleted = this.getIsItemCompleted();
        const isIncomplete = this.getIsItemInComplete();

        // Has not been start if not completed and not incomplete
        const result = ((isCompleted === false) && (isIncomplete === false)) ? true : false;
        return result;
    }

    getDescriptionPosition = (width, itemOrder, offsetLeft) => {
        const column = itemOrder % 4;
        const groupOrder = 0;

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
    }

    isBlank = () => {
        const title = this.getItemTitle(),
            description = this.getItemDescription();

        const isBlank = ((title === '') && (description === '')) ? true : false;
        return isBlank;
    }

    handleLaunchCourse = () => {
        const { activityId } = this.props.item;
        window.open('/core/user_activity_info.aspx?start=true&id=' + activityId)
    }

    render() {
        const isNotStarted = this.getIsItemNotStarted();
        const isCompleted = this.getIsItemCompleted();
        const isIncomplete = this.getIsItemInComplete();
        const isRequired = this.getIsItemRequired();
        const title = this.getItemTitle();
        const description = this.getItemDescription();
        const isBlank = this.isBlank();

        const width = this.state.width;
        const itemOrder = this.state.itemOrder;
        const descHeight = this.state.catalogHeight;
        const descLeft = this.state.descLeft;
        
        return (
            <div className='catalog-item-wrapper' style={{ width: width + 'px' }} itemorder={itemOrder} ref={(catalogItemWrapper) => { this.catalogItemWrapper = catalogItemWrapper }}>
                <div className='catalog-item-content clear'>
                    {!isBlank ?
                        <div>
                            <div className='catalog-item-status-wrapper'>
                                {
                                    isNotStarted ?
                                        <div className='catalog-item-status status-notstarted'>
                                        </div>
                                        :
                                        isCompleted ?
                                            <div className='catalog-item-status status-completed'>
                                                <svg fill="#000000" opacity='.8' height="21" viewBox="0 0 24 24" width="21" xmlns="http://www.w3.org/2000/svg">
                                                    <path d="M0 0h24v24H0z" fill="none" />
                                                    <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z" />
                                                </svg>
                                            </div>
                                            :
                                            isIncomplete ?
                                                <div className='catalog-item-status status-incomplete'>
                                                    <div className='status-incomplete-front'>
                                                    </div>
                                                    <div className='status-incomplete-back'>
                                                    </div>
                                                </div> : null
                                }
                            </div>
                            <div className='catalog-item-title-wrapper'>
                                <div className='catalog-item-title'>
                                    {title}
                                </div>
                            </div>
                            {isRequired ?
                                <div className='catalog-item-required-wrapper'>
                                    <div className='catalog-item-required'>
                                        <svg fill="#D60000" height="36" viewBox="0 0 24 24" width="36" xmlns="http://www.w3.org/2000/svg">
                                            <path d="M0 0h24v24H0z" fill="none" />
                                            <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z" />
                                        </svg>
                                    </div>
                                </div>
                                : null
                            }
                            <div className='catalog-item-action-wrapper'>
                                <button className='btn-launch-catalog' onClick={this.handleLaunchCourse}>Launch Course</button>
                            </div>
                        </div>
                        : null
                    }
                </div>
                {!isBlank ?
                    <div className='catalog-item-description' style={{ height: descHeight + 'px', width: width + 'px', left: descLeft + 'px' }}>
                        <div className='catalog-description-content'>
                            <strong>{title}</strong>
                            {description}
                        </div>
                    </div>
                    : null
                }

            </div >
        )
    }
}

export default CatalogItem;