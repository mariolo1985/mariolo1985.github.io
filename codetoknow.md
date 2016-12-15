# MY GOOD THINGS LIST

## HTML
### Redirect tag
```
<meta http-equiv="refresh" content="3;url=http://rambo.overstock.com/styleguide">
```

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

## WEB SERVER

### Deciding on image type
```

    Do you need animation? If so, GIF is the only universal choice.
        GIF limits the color palette to at most 256 colors, which makes it a poor choice for most images. Further, PNG-8 delivers better compression for images with a small palette. As a result, GIF is the right answer only when animation is required.
    Do you need to preserve fine detail with highest resolution? Use PNG.
        PNG does not apply any lossy compression algorithms beyond the choice of the size of the color palette. As a result, it will produce the highest quality image, but at a cost of significantly higher filesize than other formats. Use judiciously.
        If the image asset contains imagery composed of geometric shapes, consider converting it to a vector (SVG) format!
        If the image asset contains text, stop and reconsider. Text in images is not selectable, searchable, or "zoomable". If you need to convey a custom look (for branding or other reasons), use a web font instead.
    Are you optimizing a photo, screenshot, or a similar image asset? Use JPEG.
        JPEG uses a combination of lossy and lossless optimization to reduce filesize of the image asset. Try several JPEG quality levels to find the best quality vs. filesize tradeoff for your asset.

```
### Expiration in .htaccess
```
<IfModule mod_mime.c>
    AddType text/css .css
    AddType application/x-javascript .js
    AddType text/x-component .htc
    AddType text/html .html .htm
    AddType text/richtext .rtf .rtx
    AddType image/svg+xml .svg .svgz
    AddType text/plain .txt
    AddType text/xsd .xsd
    AddType text/xsl .xsl
    AddType text/xml .xml
    AddType video/asf .asf .asx .wax .wmv .wmx
    AddType video/avi .avi
    AddType image/bmp .bmp
    AddType application/java .class
    AddType video/divx .divx
    AddType application/msword .doc .docx
    AddType application/vnd.ms-fontobject .eot
    AddType application/x-msdownload .exe
    AddType image/gif .gif
    AddType application/x-gzip .gz .gzip
    AddType image/x-icon .ico
    AddType image/jpeg .jpg .jpeg .jpe
    AddType application/vnd.ms-access .mdb
    AddType audio/midi .mid .midi
    AddType video/quicktime .mov .qt
    AddType audio/mpeg .mp3 .m4a
    AddType video/mp4 .mp4 .m4v
    AddType video/mpeg .mpeg .mpg .mpe
    AddType application/vnd.ms-project .mpp
    AddType application/x-font-otf .otf
    AddType application/vnd.oasis.opendocument.database .odb
    AddType application/vnd.oasis.opendocument.chart .odc
    AddType application/vnd.oasis.opendocument.formula .odf
    AddType application/vnd.oasis.opendocument.graphics .odg
    AddType application/vnd.oasis.opendocument.presentation .odp
    AddType application/vnd.oasis.opendocument.spreadsheet .ods
    AddType application/vnd.oasis.opendocument.text .odt
    AddType audio/ogg .ogg
    AddType application/pdf .pdf
    AddType image/png .png
    AddType application/vnd.ms-powerpoint .pot .pps .ppt .pptx
    AddType audio/x-realaudio .ra .ram
    AddType application/x-shockwave-flash .swf
    AddType application/x-tar .tar
    AddType image/tiff .tif .tiff
    AddType application/x-font-ttf .ttf .ttc
    AddType audio/wav .wav
    AddType audio/wma .wma
    AddType application/vnd.ms-write .wri
    AddType application/vnd.ms-excel .xla .xls .xlsx .xlt .xlw
    AddType application/zip .zip
</IfModule>
<IfModule mod_expires.c>
    ExpiresActive On
    ExpiresByType text/css A31536000
    ExpiresByType application/x-javascript A31536000
    ExpiresByType text/x-component A31536000
    ExpiresByType text/html A3600
    ExpiresByType text/richtext A3600
    ExpiresByType image/svg+xml A3600
    ExpiresByType text/plain A3600
    ExpiresByType text/xsd A3600
    ExpiresByType text/xsl A3600
    ExpiresByType text/xml A3600
    ExpiresByType video/asf A31536000
    ExpiresByType video/avi A31536000
    ExpiresByType image/bmp A31536000
    ExpiresByType application/java A31536000
    ExpiresByType video/divx A31536000
    ExpiresByType application/msword A31536000
    ExpiresByType application/vnd.ms-fontobject A31536000
    ExpiresByType application/x-msdownload A31536000
    ExpiresByType image/gif A31536000
    ExpiresByType application/x-gzip A31536000
    ExpiresByType image/x-icon A31536000
    ExpiresByType image/jpeg A31536000
    ExpiresByType application/vnd.ms-access A31536000
    ExpiresByType audio/midi A31536000
    ExpiresByType video/quicktime A31536000
    ExpiresByType audio/mpeg A31536000
    ExpiresByType video/mp4 A31536000
    ExpiresByType video/mpeg A31536000
    ExpiresByType application/vnd.ms-project A31536000
    ExpiresByType application/x-font-otf A31536000
    ExpiresByType application/vnd.oasis.opendocument.database A31536000
    ExpiresByType application/vnd.oasis.opendocument.chart A31536000
    ExpiresByType application/vnd.oasis.opendocument.formula A31536000
    ExpiresByType application/vnd.oasis.opendocument.graphics A31536000
    ExpiresByType application/vnd.oasis.opendocument.presentation A31536000
    ExpiresByType application/vnd.oasis.opendocument.spreadsheet A31536000
    ExpiresByType application/vnd.oasis.opendocument.text A31536000
    ExpiresByType audio/ogg A31536000
    ExpiresByType application/pdf A31536000
    ExpiresByType image/png A31536000
    ExpiresByType application/vnd.ms-powerpoint A31536000
    ExpiresByType audio/x-realaudio A31536000
    ExpiresByType image/svg+xml A31536000
    ExpiresByType application/x-shockwave-flash A31536000
    ExpiresByType application/x-tar A31536000
    ExpiresByType image/tiff A31536000
    ExpiresByType application/x-font-ttf A31536000
    ExpiresByType audio/wav A31536000
    ExpiresByType audio/wma A31536000
    ExpiresByType application/vnd.ms-write A31536000
    ExpiresByType application/vnd.ms-excel A31536000
    ExpiresByType application/zip A31536000
</IfModule>
<IfModule mod_deflate.c>
    <IfModule mod_setenvif.c>
        BrowserMatch ^Mozilla/4 gzip-only-text/html
        BrowserMatch ^Mozilla/4\.0[678] no-gzip
        BrowserMatch \bMSIE !no-gzip !gzip-only-text/html
        BrowserMatch \bMSI[E] !no-gzip !gzip-only-text/html
    </IfModule>
    <IfModule mod_headers.c>
        Header append Vary User-Agent env=!dont-vary
    </IfModule>
    <IfModule mod_filter.c>
        AddOutputFilterByType DEFLATE text/css application/x-javascript text/x-component text/html text/richtext image/svg+xml text/plain text/xsd text/xsl text/xml image/x-icon
    </IfModule>
</IfModule>
<FilesMatch "\.(css|js|htc|CSS|JS|HTC)$">
    <IfModule mod_headers.c>
        Header set Pragma "public"
        Header append Cache-Control "public, must-revalidate, proxy-revalidate"
    </IfModule>
    FileETag MTime Size
</FilesMatch>
<FilesMatch "\.(html|htm|rtf|rtx|svg|svgz|txt|xsd|xsl|xml|HTML|HTM|RTF|RTX|SVG|SVGZ|TXT|XSD|XSL|XML)$">
    <IfModule mod_headers.c>
        Header set Pragma "public"
        Header append Cache-Control "public, must-revalidate, proxy-revalidate"
    </IfModule>
    FileETag MTime Size
</FilesMatch>
<FilesMatch "\.(asf|asx|wax|wmv|wmx|avi|bmp|class|divx|doc|docx|eot|exe|gif|gz|gzip|ico|jpg|jpeg|jpe|mdb|mid|midi|mov|qt|mp3|m4a|mp4|m4v|mpeg|mpg|mpe|mpp|otf|odb|odc|odf|odg|odp|ods|odt|ogg|pdf|png|pot|pps|ppt|pptx|ra|ram|svg|svgz|swf|tar|tif|tiff|ttf|ttc|wav|wma|wri|xla|xls|xlsx|xlt|xlw|zip|ASF|ASX|WAX|WMV|WMX|AVI|BMP|CLASS|DIVX|DOC|DOCX|EOT|EXE|GIF|GZ|GZIP|ICO|JPG|JPEG|JPE|MDB|MID|MIDI|MOV|QT|MP3|M4A|MP4|M4V|MPEG|MPG|MPE|MPP|OTF|ODB|ODC|ODF|ODG|ODP|ODS|ODT|OGG|PDF|PNG|POT|PPS|PPT|PPTX|RA|RAM|SVG|SVGZ|SWF|TAR|TIF|TIFF|TTF|TTC|WAV|WMA|WRI|XLA|XLS|XLSX|XLT|XLW|ZIP)$">
    <IfModule mod_headers.c>
        Header set Pragma "public"
        Header append Cache-Control "public, must-revalidate, proxy-revalidate"
    </IfModule>
    FileETag MTime Size
</FilesMatch>

```
