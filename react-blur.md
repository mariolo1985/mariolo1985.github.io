    ```
    componentDidUpdate = (prevProps, prevState) => {
        // Collapse dropdown on blur
        if (this.state.isCategoryExpanded) {
            document.addEventListener('keydown', this.closeDropdownOnEscape)
            document.addEventListener('click', this.closeDropdown);
        } else {
            document.removeEventListener('keydown', this.closeDropdownOnEscape)
            document.removeEventListener('click', this.closeDropdown);
        }
    }

    componentWillUnmount = () => {
        // Remove dropdown blur events
        document.removeEventListener('keydown', this.closeDropdownOnEscape)
        document.removeEventListener('click', this.closeDropdown);
    }

    closeDropdownOnEscape = (e) => {
        // Key will hide dropdown
        if (e.keyCode === 27) {
            e.preventDefault();
            this.closeDropdown();
        }
    }

    closeDropdown = () => {
        // Set dropdown to collapse
        this.setState({
            isCategoryExpanded: false
        })
    }
    ```
