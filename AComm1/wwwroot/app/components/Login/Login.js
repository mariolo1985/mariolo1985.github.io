import React, { Component } from 'react';
import Cookies from 'universal-cookie';
import * as Constants from '../consts';

const cookies = new Cookies();

class Login extends Component {

    constructor() {
        super();
        this.state = { loggingIn: false };
    }

    onBtnLoginClick = () =>{
        console.log('Login clicked');
    }

    render() {

        let loginText = this.state.loggingIn ? "please wait.." : "LOGIN";

        return (
            <div className='login-container'>
                <div className='input-wrapper'>
                    <div className='input-row'>
                        <input type='text' placeholder='user id' name="Email" className='login-userid' />
                    </div>
                    <div className='input-row'>
                        <input type='password' placeholder='password' name="Password" className='login-userpw' />
                    </div>
                    <div className='input-row'>
                        <input type='checkbox' className='login-remember-userid' id='login-remember-userid' />
                        <label htmlFor='login-remember-userid' className='login-remember-userid-label'>Remember my user ID</label>
                    </div>
                    <div className='input-row'>
                        <button onClick={this.onBtnLoginClick} disabled={this.state.loggingIn} type="submit" className='btn btn-login btn-primary'>{loginText}</button>
                    </div>
                </div>
            </div>
        )
    }
}

export default Login