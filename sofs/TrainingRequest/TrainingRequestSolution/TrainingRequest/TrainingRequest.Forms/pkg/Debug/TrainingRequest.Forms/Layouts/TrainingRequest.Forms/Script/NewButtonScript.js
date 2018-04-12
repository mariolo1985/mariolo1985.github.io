//==============================================  BUTTON EVENTS ============================================== 

// TO OPEN A NEW REQUEST IN DIALOG
function openNewRequestDialog() {
    var options = {
        title: "",
        width: 700,
        height: 650,
        autoSize: true,
        url: "/_layouts/15/trainingrequest.forms/pages/newrequest.aspx?isdlg=1"
    };

    SP.UI.ModalDialog.showModalDialog(options);
}