# MY GOOD THINGS LIST

## JAVASCRIPT
### Do not calc length for comparsion in for/while loops. Calc length first.

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

## DATABASES
### Index columns

Indexing a column that will be queried on will speed up queries ~DUH~

### Handling duplication during insert

Define how to handle inserting duplicate key to avoid extra queries and to update/fail gracefully.

IE: Querying to see if element id exist in table. If not, add element id.

BAD: 
```
$RESULT = $conn->query('SELECT * FROM ELEMENT_TABLE WHERE ELEMENT_ID = 'THIS_ID';')
```
```
$INSERTQUERY = $RESULT->NUM_ROWS === 0 ? 'INSERT INTO ELEMENT_TABLE (ELEMENT_ID) VALUES ('THIS_ID')' : NULL;
```

GOOD:
```
$INSERTQUERY = 'INSERT INTO ELEMENT_TABLE (ELEMENT_ID) VALUES ('THIS_ID') ON DUPLICATE KEY UPDATE ELEMENT_ID = 'NEW_THIS_ID';
```
