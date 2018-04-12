using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingRequest.Forms
{
    public static class Constants
    {
        /******************************** LIST/LIBRARY URL ******************************************/
        public const string url_NewRequest = "/lists/newrequests";
        public const string url_PendingAssignment = "/lists/pendingassignment";
        public const string url_RequestType = "/lists/RequestType";
        public const string url_Department = "/lists/department";
        public const string url_NewRequestAttachment = "/newrequestattachments";
        public const string url_RequestAttachment = "/requestattachments";
        public const string url_Request = "/lists/requests";

        public const string url_ = "";

        /******************************** LIST/LIBRARY COLUMNS ******************************************/
        public const string column_RequestTitle = "RequestTitle";
        public const string column_RequestType = "RequestType";
        public const string column_RequestDescription = "RequestDescription";
        public const string column_RequestDepartment = "RequestDepartment";
        public const string column_PlannedTraining = "PlannedTraining";
        public const string column_TrainingRequestor = "TrainingRequestor";
        public const string column_RequestTaskOwner = "RequestTaskOwner";
        public const string column_ParentGuid = "requestParentGuid";
        public const string column_ChildGuid = "requestChildGuid";
        public const string column_RequiredAssignment = "requestRequiredAssignment";
        public const string column_RootGuid = "requestRootGuid";
        public const string column_ThisGuid = "requestThisGuid";
        public const string column_LinkToAssignment = "requestLinkToNewAssign";
        public const string column_ChildLink = "requestChildLink";
        public const string column_requestStatus = "requestStatus";

        public const string column_Title = "Title";


        public const string column_ = "";

        /******************************** LOOKUP CONSTANTS ******************************************/

        // ================ New ===============
        // ------ Request Type ------
        public const string newrequest_RequestTypeListUrl = "/lists/RequestType";
        public const string newrequest_RequestTypeColumnName = "Title";

        // ------ Department ------
        public const string newrequest_DeptListUrl = "/lists/department";
        public const string newrequest_DeptColumnName = "Title";

        public const string newrequest_ = "";

        /******************************** PAGE SCRIPT AND STYLE PATHS ******************************************/
        public const string path_abScriptPath = "/_layouts/15/TrainingRequest.Forms/Script/";
        public const string path_abStylePath = "/_layouts/15/TrainingRequest.Forms/CSS/";
        public const string path_relScriptPath = "TrainingRequest.Forms/Script/";
        public const string path_relStylePath = "TrainingRequest.Forms/CSS/";

        public const string path_AssignScript = "AssignScript.js";
        public const string path_AssignStyle = "AssignStyle.css";
        public const string path_NewRequestScript = "NewRequestScript.js";
        public const string path_NewRequestStyle = "NewRequestStyle.css";
        public const string path_RequestScript = "RequestScript.js";
        public const string path_RequestStyle = "RequestStyle.css";
        public const string path_AssignOwnerStyle = "";
        public const string path_AssignOwnerScript = "";

        public const string path_DisplayRequestStyle = "DisplayRequestStyle.css";
        public const string path_DisplayRequestScript = "DisplayRequestScript.js";


        public const string path_ = "";

    }
}
