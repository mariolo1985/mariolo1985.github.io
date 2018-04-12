function redirectTo(type, queryString) {

    if (type.toLowerCase() == "newassign") {
        if (queryString) {
            window.location.href = "Assign.aspx?isdlg=1" + queryString;
        } else {
            window.location.href = "Assign.aspx?isdlg=1";
        }
    } else if (type.toLowerCase() == "newrequest") {

        window.location.href = "NewRequest.aspx?isdlg=1";

    } else if (type.toLowerCase() == "pendingrequest") {

        window.location.href = "Request.aspx?isdlg=1";

    } else if (type.toLowerCase() == "displayrequest") {
        if (queryString) {
            window.location.href = "DisplayRequest.aspx?isdlg=1" + queryString;
        } else {
            window.location.href = "DisplayRequest.aspx?isdlg=1";
        }
    }

}



function getQueryStringByRegEx() {
    var result = {}, queryString = location.search.substring(1),
		re = /([^&=]+)=([^&]*)/g, m;

    while (m = re.exec(queryString)) {
        result[decodeURIComponent(m[1])] = decodeURIComponent(m[2]);
    }
    if (result == undefined) result = "";
    return result;
}