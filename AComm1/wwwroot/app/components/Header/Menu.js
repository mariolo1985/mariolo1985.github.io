import React, { Component } from 'react';
import PropTypes from 'prop-types';
import MenuItem from '../Header/MenuItem';

class Menu extends Component {

    static propTypes = {
        expand: PropTypes.bool.isRequired,
        menuList: PropTypes.array.isRequired
    }
    getMenuList = () => {
        const { menuList } = this.props;
        const result = !menuList ? [] : menuList;

        return result;
    }

    getNavMenuClass = () => {
        const defaultClass = 'nav-menu';
        const isExpand = this.props.expand;
        const result = isExpand ? `${defaultClass} expand` : `${defaultClass} collapse`;

        return result;
    }

    render() {
        const menuList = this.getMenuList();
        return (
            <div className={this.getNavMenuClass()}>
                <ul className='nav-menu-list'>

                    {
                        menuList.map((item, key) => {
                            return (
                                <MenuItem key={key} linkLabel={item.linkLabel} linkUrl={item.linkUrl} />
                            )
                        })
                    }

                </ul>
            </div>
        )
    }

}

export default Menu;