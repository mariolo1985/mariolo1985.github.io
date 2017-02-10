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
>    - [Danger Light Button](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#danger-light-button)
>    - [Link Button](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#link-button)
>    - [Secondary Button Colors](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors-1)
>
>- Marketing Buttons

# Buttons Overview

## Properties

#### Button Colors
|Name|Class | Background | Color | Border |
|------|------------|-------|-------|----|
|Default Button|os-btn | #F5F6F7 | #545658 | none |
|Primary Button|os-btn btn-primary | #0272A2 | #FFFFFF | none |
|Success Button|os-btn btn-success | #27AE60 | #FFFFFF | none |
|Secondary Default Button|os-btn btn-secondary | #0272A2 | #545658 | 1px solid #CED1D5 |
|Secondary Light Button|os-btn btn-secondary-light | #27AE60 | #545658 | 1px solid #CED1D5 |
|Danger Light Button|os-btn btn-danger-light | #FFFFFF | #AC1B25 | 1px solid #CED1D5 |
|Link Button| os-btn btn-link | #FFFFFF | #0272A2 | none |

#### Button Size
|Name|Class | Height | Mobile Width (@544px) | Font-Size|
|----|---|------------|---|------|
|Default Size| os-btn | 34px | 50% | 13px|
|Default Extra Small|os-btn btn-xs | 22px | 15% | 11px|
|Default Small|os-btn btn-sm | 30px | 20% | 12px|
|Default Large|os-btn btn-lg | 44px | 100% | 16px|

#### React Props

| Attribute | Type | Default Value | Required | Description |
|------|----|---|---|---|
|children| node | N/A |No | What will be displayed inside the button|
|disabled| bool | false | No | Disables the button if set to true|
|size|string|btn-md| No | Preset size of the button|
|type|string|btn-primary| No | Handles the type or color style of the button|
|className|string|N/A|No|Appends onto the default button classes|


# Primary

#### React Component Options

**Props**

| Attribute | Type | Default Value | Required | Description |
|------|----|---|---|---|
|children| node | N/A |No | What will be displayed inside the button|
|disabled| bool | false | No | Disables the button if set to true|
|size|string|btn-md| No | Preset size of the button|
|type|string|btn-primary| No | Handles the type or color style of the button|
|className|string|N/A|No|Appends onto the default button classes|

#### Code

**React Code**

```js
    import {Button} from 'component-library';

    <Button size="btn-lg" type="btn-success" className="btn-unique" onClick={this.testClick}>
        Hello World
    </Button>

    <Button size="btn-lg">Default Button</Button>
    <Button>Default Button</Button>
    <Button size="btn-sm">Default</Button>
    <Button size="btn-xs">Default</Button>

    <Button size="btn-lg" type="btn-primary">Primary Button</Button>
    <Button type="btn-primary">Primary Button</Button>
    <Button size="btn-sm" type="btn-primary">Primary</Button>
    <Button size="btn-xs" type="btn-primary">Primary</Button>

    <Button size="btn-lg" type="btn-success">Success Button</Button>
    <Button size="btn-md" type="btn-success">Success Button</Button>
    <Button size="btn-sm" type="btn-success">Success</Button>
    <Button size="btn-xs" type="btn-success">Success</Button>
```


## Default Button

These buttons will be classed as ```os-btn```

**HTML**
```html
    <button class='os-btn btn-lg'>Large Button</button>
    <button class='os-btn'>Medium Button</button>
    <button class='os-btn btn-sm'>Small</button>
    <button class='os-btn btn-xs'>Extra</button>
```

**LESS**
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
[[primary color chart](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors)]
[[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)]

## Primary Button


These buttons will be classed as ```os-btn btn-primary```

**HTML**
```html
    <button class='os-btn btn-primary btn-lg'>Large Button</button>
    <button class='os-btn btn-primary'>Medium Button</button>
    <button class='os-btn btn-primary btn-sm'>Small</button>
    <button class='os-btn btn-primary btn-xs'>Extra</button>
```

**LESS**
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
[[primary color chart](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors)]
[[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)]

## Success Button

These buttons will be classed as ```os-btn btn-success```

**HTML**
```html
    <button class='os-btn btn-success btn-lg'>Large Button</button>
    <button class='os-btn btn-success'>Medium Button</button>
    <button class='os-btn btn-success btn-sm'>Small</button>
    <button class='os-btn btn-success btn-xs'>Extra</button>
```

**LESS**
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
[[primary color chart](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors)]
[[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)]

# Secondary

## Secondary Default Button

This button will be classed ```os-btn btn-secondary```

**HTML**
```html
    <button class='os-btn btn-secondary btn-lg'>Large Button</button>
    <button class='os-btn btn-secondary'>Medium Button</button>
    <button class='os-btn btn-secondary btn-sm'>Small</button>
    <button class='os-btn btn-secondary btn-xs'>Extra</button>
```

**LESS**
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
[[secondary color chart](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors-1)]
[[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)]


## Secondary Light Button

This button will be classed ```os-btn btn-secondary-light```

**HTML**
```html
    <button class='os-btn btn-secondary-light btn-lg'>Large Button</button>
    <button class='os-btn btn-secondary-light'>Medium Button</button>
    <button class='os-btn btn-secondary-light btn-sm'>Small</button>
    <button class='os-btn btn-secondary-light btn-xs'>Extra</button>
```

**LESS**
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
[[secondary color chart](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors-1)]
[[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)]

## Danger Light Button

This button will be classed ```os-btn btn-danger-light```

**HTML**
```html
    <button class='os-btn btn-danger-light btn-lg'>Destructive Button</button>
    <button class='os-btn btn-danger-light'>Destructive Button</button>
```

**LESS**
```css
    .os-btn{
        &.btn-danger-light {
            background-color: @sg_colorBtnDangerLightBtn;
            color: @sg_colorBtnDangerLightText;
            border: 1px solid @sg_colorBtnDangerLightBrd;
        }
    }
```
[[secondary color chart](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors-1)]
[[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)]

## Link Button

This button will be classed ```os-btn btn-link```

**HTML**
```html
    <button class='os-btn btn-link btn-lg'>Link Button</button>
    <button class='os-btn btn-link'>Link Button</button>
    <button class='os-btn btn-link btn-sm'>Link</button>
    <button class='os-btn btn-link btn-xs'>Link</button>
```

**LESS**
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
[[secondary color chart](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#button-colors-1)]
[[back to top](https://github.com/mariolo1985/mariolo1985.github.io/blob/master/componentlibrary/buttons-guideline.md#buttons-guidelines)]