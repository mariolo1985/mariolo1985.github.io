# MY GOOD THINGS LIST

## Do not calc length for comparsion in for/while loops. Calc length first.

BAD: 
```js
for (var i = 0; i<something.length() ; i++){

}
```

GOOD:

```js
var arrLen = arr.length();
for (var i = 0; i < arrLen; i++){

}
```
