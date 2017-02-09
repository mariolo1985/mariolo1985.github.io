# Buttons Guidelines

Visual reference available at [Styleguide - Buttons Page](https://www.overstock.com/styleguide/buttons.html)

**Table of Contents:**
>- Primary
>    - [Default Button](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#default-button)
>    - [Primary Button](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#primary-button)
>    - [Success Button](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#success-button)
>    - [Default Button Colors](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors)
>    - [Default Button Sizes](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-size)
>
>- Secondary 
>    - [Secondary Default Button](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#secondary-default-button)
>    - [Secondary Light Button](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#secondary-light-button)
>    - [Danger Light](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#danger-light-button)
>    - [Link Button](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#link-button)
>    - [Secondary Button Colors](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors-1)
>
>- Marketing Buttons

    
# Primary

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
[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)

## Primary Button


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
[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)

## Success Button

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
        color: @sg_colorBtnSuccessText;
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
[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)

# Secondary

#### Button Colors
|Class | Background | Color | Border |
|------|------------|-------|--------|
|os-btn btn-secondary | #0272A2 | ##545658 | 1px solid #CED1D5 |
|os-btn btn-secondary-light | #27AE60 | #545658 | 1px solid #CED1D5 |
|os-btn btn-danger-light | #FFFFFF | #AC1B25 | 1px solid #CED1D5 |
|os-btn btn-link | #FFFFFF | #0272A2 | none |



## Secondary Default Button

This button will be classed ```os-btn btn-secondary```

#### Code 

```html
    <button class='os-btn btn-secondary btn-lg'>Large Button</button>
    <button class='os-btn btn-secondary'>Medium Button</button>
    <button class='os-btn btn-secondary btn-sm'>Small</button>
    <button class='os-btn btn-secondary btn-xs'>Extra</button>
```

```css
.os-btn{
    // SECONDARY
    &.btn-secondary {
        background-color: @sg_colorBtnSecondary;
        color: @sg_colorBtnSecondaryText;
        border: 1px solid @sg_colorBtnSecondaryBrd;
        &:hover,
        &.hover {
            background-color: @sg_colorBtnSecondaryHover;
            box-shadow: none;
        }
        &:active,
        &.os-btn-icon:active {
            background-color: @sg_colorBtnSecondaryPress;
        }
    }
}
```
[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)


## Secondary Light Button

This button will be classed ```os-btn btn-secondary-light```

#### Code 

```html
    <button class='os-btn btn-secondary-light btn-lg'>Large Button</button>
    <button class='os-btn btn-secondary-light'>Medium Button</button>
    <button class='os-btn btn-secondary-light btn-sm'>Small</button>
    <button class='os-btn btn-secondary-light btn-xs'>Extra</button>
```

```css
.os-btn{
    // SECONDARY LIGHT
    &.btn-secondary-light {
        background-color: @sg_colorBtnSecondaryLight;
        color: @sg_colorBtnSecondaryLightText;
        border: 1px solid @sg_colorBtnSecondaryLightBrd;
    }
}
```
[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)

## Danger Light Button

This button will be classed ```os-btn btn-danger-light```

#### Code 

```html
    <button class='os-btn btn-danger-light btn-lg'>Destructive Button</button>
    <button class='os-btn btn-danger-light'>Destructive Button</button>
```

```css
.os-btn{
    &.btn-danger-light {
        background-color: @sg_colorBtnDangerLightBtn;
        color: @sg_colorBtnDangerLightText;
        border: 1px solid @sg_colorBtnDangerLightBrd;
    }
}
```
[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)

## Link Button

This button will be classed ```os-btn btn-link```

#### Code 

```html
    <button class='os-btn btn-link btn-lg'>Link Button</button>
    <button class='os-btn btn-link'>Link Button</button>
    <button class='os-btn btn-link btn-sm'>Link</button>
    <button class='os-btn btn-link btn-xs'>Link</button>
```

```css
.os-btn{
    // LINK
    &.btn-link {
        background-color: @sg_colorWhite;
        color: @sg_colorTextLink;
        height: auto;
        padding: 0;
        box-shadow: none;
    }
}
```
[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)
