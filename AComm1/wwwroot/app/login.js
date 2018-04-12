import React from 'react';
import { render } from 'react-dom';
import Header from '../build/Header/Header';
import Login from '../build/Login/Login';

window.onload = function () {
    render(
        (
            <Header showMenu={false} showTitle={false} />
        ),
        document.getElementById('main-header')
    );

    render(
        (
            <Login />
        ),
        document.getElementById('main-login')
    );
}