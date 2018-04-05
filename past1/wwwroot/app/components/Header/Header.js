import React, { Component } from 'react';
import PropTypes from 'prop-types';
import Menu from '../Header/Menu';

class Header extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isMenuExpanded: false
        }
    }

    static PropTypes = {
        showMenu: PropTypes.bool.isRequired,
        showTitle: PropTypes.bool.isRequired,
        title: PropTypes.string
    }

    static defaultProps = {
        showMenu: false,
        showTitle: false,
        title: ''
    }

    handleShowMenu = () => {
        const { showMenu } = this.props;
        const result = !showMenu ? Header.defaultProps.showMenu : showMenu;

        return result;
    }

    handleShowTitle = () => {
        const { showTitle } = this.props;
        const result = !showTitle ? Header.defaultProps.showTitle : showTitle;

        return result;
    }

    onBtnMenuClick = () => {
        // toggle menu
        this.setState({
            isMenuExpanded: !this.state.isMenuExpanded
        })
    }

    getTitle = () => {
        const { title } = this.props;
        const result = !title ? Header.defaultProps.title : title;

        return result;
    }

    componentDidUpdate = (prevProps, prevState) => {

        // hide menu on blur
        if (this.state.isMenuExpanded) {
            document.addEventListener('click', this.closeMenu);
        } else {
            document.removeEventListener('click', this.closeMenu);
        }
    }

    componentWillUnmount = () => {
        // hide menu blur events        
        document.removeEventListener('click', this.closeMenu);
    }


    closeMenu = () => {
        // Set dropdown to collapse
        this.setState({
            isMenuExpanded: false
        })
    }

    getMenuList = () => {
        const menuList = [
            {
                linkLabel: 'My Profile',
                linkUrl: './catalog.html'
            },
            {
                linkLabel: 'Course Catalog',
                linkUrl: './coursecatalog.html'
            },
            {
                linkLabel: 'Downlines',
                linkUrl: '#'
            },
            {
                linkLabel: 'Sign Out',
                linkUrl: '#'
            },
        ]

        return menuList;
    }

    render() {
        const showMenu = this.handleShowMenu();
        const showTitle = this.handleShowTitle();
        const username = "James";

        const menuList = this.getMenuList();

        return (
            <div className='nav-container clear'>
                <div className='nav-branding-wrapper clear'>
                    <div className='nav-branding-img-wrapper'>
                        <a href='#'>
                            <img src='./content/images/logo_usana.png' className='nav-branding-img' />
                        </a>
                    </div>
                    {
                        showTitle ?
                            <div className='nav-title-wrapper'>
                                <div className='nav-title'>
                                    {this.getTitle()}
                                </div>
                            </div>
                            : null
                    }
                </div>
                {
                    showMenu ?
                        <div className='nav-menu-wrapper clear'>
                            <div className='nav-user'>
                                <div className='nav-username'>Hello, {username}!</div>
                                <a href='#' className='nav-logout'>Logout</a>
                            </div>

                            <div className='nav-user-img'>
                                <button className='btn-nav-user-img'>
                                    <svg className='nav-user-icon' fill="#FFFFFF" height="24" viewBox="0 0 24 24" width="24" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z" />
                                        <path d="M0 0h24v24H0z" fill="none" />
                                    </svg>
                                </button>
                            </div>

                            <div className='nav-action'>
                                <button className='btn-nav-menu' onClick={this.onBtnMenuClick}>
                                    <svg fill="#444444" height="20" viewBox="0 0 24 24" width="22" >
                                        <path d="M0 0h24v24H0z" fill="none" />
                                        <path d="M3 18h18v-2H3v2zm0-5h18v-2H3v2zm0-7v2h18V6H3z" />
                                    </svg>
                                </button>
                            </div>

                        </div>
                        :
                        null
                }
                {
                    showMenu ?
                        <Menu expand={this.state.isMenuExpanded} menuList={menuList}/>
                        : null
                }
            </div>
        )
    }
}

export default Header;