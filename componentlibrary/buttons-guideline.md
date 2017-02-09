# Buttons Guidelines

Visual reference available at [Styleguide - Buttons Page](https://www.overstock.com/styleguide/buttons.html)

## Primary Buttons

|Class | Background | Color | Border | Height | Font-Size|
|------|------------|-------|--------|--------|----------|
|os-btn | #F5F6F7 | ##545658 | none | 34px | 13px|

### Default

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
    }
```