# Buttons Guidelines

Visual reference available at [Styleguide - Buttons Page](https://www.overstock.com/styleguide/buttons.html)

# Primary Buttons

#### Button Colors
|Class | Background | Color | Border |
|------|------------|-------|--------|
|os-btn | #F5F6F7 | #545658 | none |
|os-btn btn-primary | #0272A2 | #FFFFFF | none |
|os-btn btn-success | #27AE60 | #FFFFFF | none |

#### Button Size
|Class | Height | Font-Size|
|------|------------|-------|
|os-btn | 34px | 13px|
|os-btn btn-xs | 22px | 11px|
|os-btn btn-sm | 30px | 12px|
|os-btn btn-lg | 44px | 16px|

## Default Button

These buttons will be classed as ```os-btn```

#### React Options



#### Code

```html
    <button class='os-btn btn-lg'>Large Button</button>
    <button class='os-btn'>Medium Button</button>
    <button class='os-btn btn-sm'>Small</button>
    <button class='os-btn btn-xs'>Extra</button>
```

```css
.os-btn {
    position: relative;
    background-color: @sg_colorBtn;
    color: @sg_colorBtnText;
    font-family: @default-font;
    font-size: 13px;
    font-weight: 300;
    letter-spacing: 1px;
    height: 34px;
    border: none;
    border-radius: 3px;
    padding: 0px 15px;
    cursor: pointer;
    outline: none;

    // SIZES
    &.btn-xs {
        font-size: 11px;
        height: 22px;
        padding: 0px 10px;
    }
    &.btn-sm {
        font-size: 12px;
        height: 30px;
    }
    &.btn-lg {
        font-size: 16px;
        height: 44px;
        padding: 0px 20px;
    }
}
```


## Primary

These buttons will be classed as ```os-btn btn-primary```

#### React Options



#### Code

```html
    <button class='os-btn btn-primary btn-lg'>Large Button</button>
    <button class='os-btn btn-primary'>Medium Button</button>
    <button class='os-btn btn-primary btn-sm'>Small</button>
    <button class='os-btn btn-primary btn-xs'>Extra</button>
```

```css
.os-btn {
    // PRIMARY BUTTON
    &.btn-primary {
        background-color: @sg_colorBtnPrimary;
        color: @sg_colorBtnPrimaryText;
        &:hover,
        &.hover {
            background-color: @sg_colorBtnPrimaryHover;
            box-shadow: none;
        }
        &:active {
            background-color: @sg_colorBtnPrimaryPress;
        }
    }
}
```

## Success

These buttons will be classed as ```os-btn btn-success```

#### React Options



#### Code

```html
    <button class='os-btn btn-success btn-lg'>Large Button</button>
    <button class='os-btn btn-success'>Medium Button</button>
    <button class='os-btn btn-success btn-sm'>Small</button>
    <button class='os-btn btn-success btn-xs'>Extra</button>
```

```css
.os-btn {
    // SUCCESS
    &.btn-success {
        background-color: @sg_colorBtnSuccess;
        color: @sg_colorWhite;
        &:hover,
        &.hover {
            background-color: @sg_colorBtnSuccessHover;
            box-shadow: none;
        }
        &:active {
            background-color: @sg_colorBtnSuccessPress;
        }
    }
}
```

# Secondary Buttons