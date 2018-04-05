import React from 'react';
import ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import Root from './config/Root';
import Login from './components/Login/Login';
import Catalog from './components/Catalog/Catalog';

window.ReactDOM = ReactDOM;
window.React = React;

window.Login = Login;
window.Catalog = Catalog;

// const render = (Component) => {
//     ReactDOM.render(
//         <AppContainer>
//             <Component />
//         </AppContainer>,
//         document.getElementById('root'),
//     );
// };

// render(Root);

// if (module.hot) {
//     module.hot.accept('./config/Root', () => {
//         const newApp = require('./config/Root').default;
//         render(newApp);
//     });
// }