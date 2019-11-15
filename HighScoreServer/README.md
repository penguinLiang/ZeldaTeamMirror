Dead simple HTTP-based high score server:

`GET /`:

Returns the top 25 scores posted as CSV text in the following format:  
`Initials,Score`

For example:
```
FTW,1337
LOL,1234567
```

`POST /`:

Body:
```
XXX,123456
```

Adds a score using the same format as above with a single line


