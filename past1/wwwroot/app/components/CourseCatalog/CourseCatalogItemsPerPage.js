import React, { Component } from 'react';

class CourseCatalogItemsPerPage extends Component {

    onSelectChange = () => {
        const courseCatalogSelectElements = document.getElementsByClassName('coursecatalog-course-page-select');
        const selectElement = courseCatalogSelectElements.length === 0 ? null : courseCatalogSelectElements[0];

        var selectValue;
        if (selectElement === null) {
            selectValue = this.getItemsPerPageCount()
        } else {

            selectValue = selectElement.value;
        }

        this.props.handleItemsPerPageChange(selectValue);
    }

    getItemsPerPageCount = () => {
        const { itemsPerPage } = this.props;
        const result = !itemsPerPage ? 1 : itemsPerPage;

        return result;
    }

    getCourseMenu = () => {
        const { courseMenu } = this.props;
        const result = !courseMenu ? [] : courseMenu;

        return result;
    }

    getOptionsHtml = () => {
        const courseMenu = this.getCourseMenu(),
            itemCount = courseMenu.length;
        const itemsPerPage = this.getItemsPerPageCount();
        var optionCount = Math.ceil(itemCount / itemsPerPage);
        optionCount = optionCount <= 0 ? 1 : optionCount;

        var htmlArr = [];
        var html = "";

        for (var x = 1; x <= optionCount; x++) {
            html = (
                <option key={x} value={x * itemsPerPage}>{x * itemsPerPage} per page</option>
            );
            htmlArr.push(html);
        }

        return htmlArr;

    }

    render() {

        return (
            <select className='coursecatalog-course-page-select' onChange={this.onSelectChange}>
                {this.getOptionsHtml()}
            </select>
        )
    }
}

export default CourseCatalogItemsPerPage;