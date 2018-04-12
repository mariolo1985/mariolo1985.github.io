import React from 'react';
import { render } from 'react-dom';
import Header from '../build/Header/Header';
import CourseCatalog from '../build/CourseCatalog/CourseCatalog';

window.onload = function () {
    render(
        (
            <Header showMenu={true} showTitle={true} title='Course Catalog' />
        ),
        document.getElementById('main-header')
    );

    render(
        (
            <CourseCatalog coursemenu={[]} itemsPerPage={20} />
        ),
        document.getElementById('main-catalog')
    );
}