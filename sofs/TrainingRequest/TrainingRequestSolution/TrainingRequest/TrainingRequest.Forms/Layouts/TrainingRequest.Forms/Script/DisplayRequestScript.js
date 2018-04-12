var $j = jQuery.noConflict();
$j(document).ready(function () {
    $j('#div_menu').tabs();
});

function addListeners() {
    document.getElementById('btn_addDept').addEventListener("click", function () { addClicked('add'); }, false);
    document.getElementById('btn_removeDept').addEventListener("click", function () { addClicked('remove'); }, false);
    document.getElementById(requestTypeDdlId).addEventListener("click", function () { togglePlannedTraining(requestTypeDdlId); }, false);
}


// ==============================================  TOGGLE SHOW/HIDE OF ELEMENTS ============================================== 

// TOGGLES PLANNED TRAINING DIV IF TRAINING REQUEST OR NOT
function togglePlannedTraining(requestTypeDdlId) {
    var dropRequestType = document.getElementById(requestTypeDdlId);
    var selected = dropRequestType.options[dropRequestType.selectedIndex].value;

    if (selected == "Training Request") {
        showPlannedTraining();
    } else {
        hidePlannedTraining();
    }
}

// SHOWS THE PLANNED TRAINING DIV
function hidePlannedTraining() {
    var divPT = document.getElementById('div_plannedTraining');
    divPT.style.display = "none";
}

// HIDES THE PLANNED TRAINING DIV
function showPlannedTraining() {
    var divPT = document.getElementById('div_plannedTraining');
    divPT.style.display = "block";
}

function showConfirm() {
    var divConfirm = document.getElementById("div_confirm");
    divConfirm.style.display = "block";
}

function hideConfirm() {
    var divConfirm = document.getElementById("div_confirm");
    divConfirm.style.display = "none";
}

function showSubmitMessage(outcome) {
    var divSuccess = document.getElementById("div_success");
    var divError = document.getElementById("div_error");
    var divReq = document.getElementById("div_request");

    showConfirm();
    if (outcome == "success") {
        divSuccess.style.display = "block";
        divError.style.display = "none";
        divReq.style.display = "none";

    } else if (outcome = "fail") {
        divSuccess.style.display = "none";
        divError.style.display = "block";
    } else {
        hideConfirm();
        divSuccess.style.display = "none";
        divError.style.display = "none";
    }

}


function showSupportDocPanel() {
    document.getElementById(supportDocsPnId).style.display = "block";
}

function hideSupportDocPanel() {
    document.getElementById(supportDocsPnId).style.display = "none";
}


function showProgress() {
    document.getElementById(progressId).style.display = "block";
}

function hideProgress() {
    document.getElementById(progressId).style.display = "none";
}
//==============================================  BUTTON EVENTS ============================================== 

// THIS WILL HANDLE SELECTING AND DESELECTING DEPARTMENT BUTTON CLICKS
function addClicked(cmd) {

    if (cmd.toLowerCase() == "add") {
        startAddingListitem(availableLsbId, selectedLsbId);
    } else {
        startAddingListitem(selectedLsbId, availableLsbId);
    }
}

// COPIES AN OPTION FROM ONE LIST BOX TO ANOTHER
function startAddingListitem(sourceId, destId) {
    var sourceList = document.getElementById(sourceId);
    var destList = document.getElementById(destId);

    for (var i = 0; i < sourceList.options.length; i++) {
        if (sourceList.options[i].selected) {
            var selectedText = sourceList.options[i].text;
            var selectedValue = sourceList.options[i].value;

            if (selectedValue != "") {
                // remove from source
                sourceList.remove(i);

                // add to destination
                addListItem(selectedText, selectedValue, destList);
            }
        }
    }

}

function addListItem(text, value, destList) {
    destList.add(new Option(text, value));
}


// FOR THE CLOSE BUTTON EVENT
function goHome() {
    window.location.href = 'http://vm:36047/SitePages/DevHome.aspx';
}

function closeThis() {
    window.frameElement.commitPopup();
}


