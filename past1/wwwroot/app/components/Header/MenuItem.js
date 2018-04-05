import React, { Component } from 'react';
import PropTypes from 'prop-types';

class MenuItem extends Component {
    static propTypes = {
        linkLabel: PropTypes.string.isRequired,
        linkUrl: PropTypes.string.isRequired
    }

    getLinkLabel = () => {
        const { linkLabel } = this.props;
        const result = !linkLabel ? '' : linkLabel;

        return result;
    }

    getLinkUrl = () => {
        const { linkUrl } = this.props;
        const result = !linkUrl ? '#' : linkUrl;

        return result;
    }
    render() {
        const linkLabel = this.getLinkLabel(),
            linkUrl = this.getLinkUrl();
        return (
            <li className='nav-menu-listitem'>
                <a href={linkUrl} className='nav-menu-link'>{linkLabel}</a>
            </li>
        );
    }
}

export default MenuItem;