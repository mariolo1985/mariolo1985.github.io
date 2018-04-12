$web = get-spweb "https://www.supplieroasis.com"
$list = $web.lists | ? { $_.title -eq "pxlogging" }
$spQuery = New-Object Microsoft.SharePoint.SPQuery
$spQuery.ViewAttributes = "Scope='Recursive'";
$spQuery.RowLimit = 100
$caml = '<OrderBy Override="TRUE"><FieldRef Name="ID"/></OrderBy>'
$spQuery.Query = $caml
 
do
{
    $listItems = $list.GetItems($spQuery)
    $count = $listItems.Count
    $spQuery.ListItemCollectionPosition = $listItems.ListItemCollectionPosition
    $batch = "<?xml version=`"1.0`" encoding=`"UTF-8`"?><Batch>"
    $j = 0
    for ($j = 0; $j -lt $count; $j++)
    {
        $item = $listItems[$j]
        write-host "`rProcessing ID: $($item.ID) ($($j+1) of $($count))" -nonewline
        $batch += "<Method><SetList Scope=`"Request`">$($list.ID)</SetList><SetVar Name=`"ID`">$($item.ID)</SetVar><SetVar Name=`"Cmd`">Delete</SetVar><SetVar Name=`"owsfileref`">$($item.File.ServerRelativeUrl)</SetVar></Method>"
        if ($i -ge $count) { break }
    }
    $batch += "</Batch>"
 
    write-host
 
    write-host "Sending batch..."
    $result = $web.ProcessBatchData($batch)

}
while ($spQuery.ListItemCollectionPosition -ne $null)
$web.Dispose()