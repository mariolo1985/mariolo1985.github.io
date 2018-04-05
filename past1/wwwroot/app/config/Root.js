import React from 'react';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import Login from '../components/Login/Login';
import Header from '../components/Header/Header';
import Catalog from '../components/Catalog/Catalog';

const Root = () => {
  return (
    <Router>
      <div>
        <Header />
        <Route exact path="/" component={Catalog} />
        <Route exact path="/login" component={Login} />
      </div>
    </Router>
  );
};

export default Root;
