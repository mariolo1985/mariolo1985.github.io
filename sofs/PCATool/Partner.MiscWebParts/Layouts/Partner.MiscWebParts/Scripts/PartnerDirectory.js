var $j = jQuery.noConflict();

// If the ENTER key is pressed than we will trigger btn_pdSearch click event
function enterPress(e) {
    e = e || window.event;
    var kCode = e.keyCode ? e.keyCode : e.which;
    if ((kCode == 13) || (kCode == 36)) {
        document.getElementById("btn_pdSearch").click();
    }
}


function PartnerDirectoryDialogCallback(dialogResult, returnValue) {
    if (dialogResult) { location.reload(true) }
}
