using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;


using TrainingRequestWFProj.TrainingMaterialsWF.HelperClass;
using System.Web.UI.HtmlControls;

using Microsoft.SharePoint.Workflow;
using System.Collections;


namespace TrainingRequestWFProj.Layouts.TrainingRequestWF.Pages
{
    // $$$$$$$$$$$$$$$$$$$$$$$ TO DO $$$$$$$$$$$$$$$$$$$$$$$$$$$$
    // 1. Remove hardcoded task types
    //

    public partial class TrainingTask : LayoutsPageBase
    {
        // =================================== CLASS VARIABLE ==============================
        private static string TaskType;
        private static string TaskListId;
        private static string TaskItemId;

        private static string RequestFormUrl = default(string);

        // =================================== CLASS EVENTS ==============================
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string webUrl = SPContext.Current.Web.Url.ToString(), taskType = string.Empty;
                clearPageMessage();// Start with a clear message box

                // CHECK TASK TYPE
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.qsTaskType].ToString()))
                {
                    taskType = Request.QueryString[Constants.qsTaskType].ToString();// Task Type 
                    TaskType = taskType;// global var = local var

                    setPageTitle(taskType);// Sets the header
                    showTaskSection(taskType);// Displays div/sections per task type
                }
                else
                {
                    setPageMessage("Unable to retrieve Task Type", true);// ERROR MESSAGE
                }

                // CHECK FOR TASK LIST                
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.qsList].ToString()))
                {
                    TaskListId = Request.QueryString[Constants.qsList].ToString();
                }
                else
                {
                    setPageMessage("Unable to retrieve List Id.", true);// ERROR MESSAGE
                }

                // CHECK FOR TASK ITEM ID
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.qsId].ToString()))
                {
                    TaskItemId = Request.QueryString[Constants.qsId].ToString();
                }
                else
                {
                    setPageMessage("Unable to retrieve Item Id", true);// ERROR MESSAGE
                }

                // IF WE PASSED OUR TASK VALIDATION
                if (string.IsNullOrEmpty(divMessage.InnerHtml))
                {
                    // Get task page info
                    SPSecurity.RunWithElevatedPrivileges(() =>
                    {
                        using (SPSite TrainingSite = new SPSite(webUrl))
                        using (SPWeb TrainingWeb = TrainingSite.OpenWeb())
                        {
                            SPList TaskList = default(SPList);
                            try
                            {
                                Guid TaskListGuid = new Guid(TaskListId);
                                TaskList = TrainingWeb.Lists[TaskListGuid];

                                if (TaskList != null)
                                {
                                    SPListItem TaskItem = GetTaskItemMetadata.GetTaskItem(TaskList, TaskItemId);
                                    object taskOutcome = TaskItem[Constants.WorkflowOutcomeColumnName];

                                    if ((taskOutcome != null) && (!string.IsNullOrEmpty(taskOutcome.ToString())) && (taskOutcome.ToString().ToLower().Contains("completed")))
                                    {
                                        // a completed task is being opened
                                        resetTaskSection();
                                        // display message
                                        setPageMessage("This task has been completed.", false);
                                        showElement(divCloseBtn);
                                        showElement(divTaskInfo);
                                        divHeader.InnerHtml = string.Empty;
                                        return;// do not build task body
                                    }
                                    // IF WE HAVE TASK ITEM AND THE RELATED ITEM REF
                                    if ((TaskItem != null) && (!string.IsNullOrEmpty(TaskItem[Constants.RelatedContentColumnName].ToString())))
                                    {
                                        SPFieldUrlValue RequestItemLink = new SPFieldUrlValue(TaskItem[Constants.RelatedContentColumnName].ToString());
                                        if (!string.IsNullOrEmpty(RequestItemLink.Url.ToString()))
                                        {
                                            string itemUrl = RequestItemLink.Url.ToString();
                                            RequestFormUrl = itemUrl;

                                            SPListItem RequestForm = TrainingWeb.GetListItem(itemUrl);
                                            string taskSection = "<table id=\"tblTaskSection\">{0}</table>", taskRow = string.Empty, taskContent = string.Empty;

                                            taskRow = "<tr><th>{0}</th><td>{1}</td></tr>";

                                            // Set hyperlink
                                            string itemHyperlinkTag = string.Format("<a href='{0}'>Link to request</a>", itemUrl + "?source=" + webUrl + "&DefaultItemOpen=1");
                                            taskContent += string.Format(taskRow, "Request:", itemHyperlinkTag);
                                            // Get request title
                                            if (!string.IsNullOrEmpty(RequestForm[Constants.ItemRequestTitleColumnName].ToString()))
                                            {
                                                taskContent += string.Format(taskRow, "Request Title:", RequestForm[Constants.ItemRequestTitleColumnName].ToString());
                                            }
                                            // Get request type
                                            if (!string.IsNullOrEmpty(RequestForm[Constants.ItemRequestTypeColumnName].ToString()))
                                            {
                                                taskContent += string.Format(taskRow, "Request Type:", RequestForm[Constants.ItemRequestTypeColumnName].ToString());
                                            }
                                            // Get Planned Training
                                            if (!string.IsNullOrEmpty(RequestForm[Constants.PlannedTrainingColumnName].ToString()))
                                            {
                                                taskContent += string.Format(taskRow, "Planned Training:", RequestForm[Constants.PlannedTrainingColumnName].ToString());
                                            }
                                            // Get Dept
                                            if (!string.IsNullOrEmpty(RequestForm[Constants.ItemDeptColumnName].ToString()))
                                            {
                                                taskContent += string.Format(taskRow, "Department:", RequestForm[Constants.ItemDeptColumnName].ToString());
                                            }
                                            // Set role type if task type == assign
                                            if ((taskType == "assign") && (!string.IsNullOrEmpty(TaskItem[Constants.RoleTypeColumnName].ToString())))
                                            {
                                                taskContent += string.Format(taskRow, "Assign for Role:", TaskItem[Constants.RoleTypeColumnName].ToString());

                                                // handle assigning a training mgr role
                                                string roleType = TaskItem[Constants.RoleTypeColumnName].ToString();
                                                if ((roleType == Constants.TrainingManagerRoleString) || (roleType == Constants.TrainerRoleString))
                                                {
                                                    showTaskSection("trainingmgr");
                                                }
                                            }


                                            // Set task content section if we have any
                                            if (!string.IsNullOrEmpty(taskContent))
                                            {
                                                divGeneral.InnerHtml = string.Format(taskSection, taskContent);
                                            }

                                        }
                                    }
                                }
                            }
                            catch (Exception err)
                            {
                                LogWriting.LogExceptionToSP("Error: Page_Load - Setting task page info", err);
                            }

                        }// dispose sp obj
                    });// end RWEP
                }

            }

        }// end pageload



        // SAVE BUTTON EVENT
        protected void btn_Assign_Click(object sender, EventArgs e)
        {
            InProgress();

            // 1. Retrieve task metadata
            string taskType = string.Empty, taskListId = string.Empty, taskItemId = string.Empty;
            taskType = TaskType;
            taskListId = TaskListId;
            taskItemId = TaskItemId;

            string webUrl = SPContext.Current.Web.Url.ToString();
            if (!string.IsNullOrEmpty(webUrl))
            {
                using (SPSite trainingSite = new SPSite(webUrl))
                using (SPWeb trainingWeb = trainingSite.OpenWeb())
                {
                    trainingSite.AllowUnsafeUpdates = true;
                    trainingWeb.AllowUnsafeUpdates = true;

                    try
                    {
                        // 2. Get people editor
                        PeopleEditor assignPicker = default(PeopleEditor);
                        switch (taskType)
                        {
                            case "assign":
                                if (divAssignTask.Attributes["class"].ToLower().Contains("show"))
                                {
                                    assignPicker = peAssignTo;
                                }
                                else
                                {
                                    // this task is to assign a training mgr
                                    assignPicker = peAssignTrainer;
                                }
                                break;
                            case "trainingmgr":
                                assignPicker = peAssignTrainer;
                                break;
                        }

                        if (assignPicker != null)
                        {
                            if (assignPicker.IsValid)
                            {
                                SPFieldUserValueCollection userObjCollection = new SPFieldUserValueCollection();
                                // get accounts update the task item
                                foreach (string acct in assignPicker.Accounts)
                                {
                                    // check if user or group
                                    SPPrincipal tempPrin = default(SPPrincipal);
                                    try
                                    {
                                        SPUser tempUser = trainingWeb.EnsureUser(acct);
                                        tempPrin = tempUser;
                                    }
                                    catch
                                    {
                                        SPGroup tempGroup = trainingWeb.Groups[acct];
                                        tempPrin = tempGroup;
                                    }

                                    if (tempPrin != null)
                                    {
                                        SPFieldUserValue tempFieldUser = new SPFieldUserValue(trainingWeb, tempPrin.ID, tempPrin.LoginName);
                                        userObjCollection.Add(tempFieldUser);
                                    }

                                }// end foreach
                                if (userObjCollection.Count > 0)
                                {
                                    if (GetTaskItemMetadata.SetItemMetadata(trainingWeb, taskListId, taskItemId, userObjCollection, true))
                                    {
                                        btn_Cancel_Click(sender, e);// close the dialog
                                    }
                                    else
                                    {
                                        // throw message to user ?
                                    }
                                }
                            }
                            else
                            {
                                // the picker is not valid
                                // do not pass Go
                            }
                        }

                    }
                    catch (Exception err)
                    {
                        LogWriting.LogExceptionToSP("btn_Assign_Click() - ", err);
                    }

                    trainingWeb.AllowUnsafeUpdates = false;
                    trainingSite.AllowUnsafeUpdates = false;

                }// dispose sp obj
            }

            CompleteProgress();

        }// end btn_save_click

        // REVIEWED BUTTON
        protected void btn_Reviewed_Click(object sender, EventArgs e)
        {
            // 1. Retrieve task metadata
            string taskType = string.Empty, taskListId = string.Empty, taskItemId = string.Empty;
            taskType = TaskType;
            taskListId = TaskListId;
            taskItemId = TaskItemId;

            string webUrl = SPContext.Current.Web.Url.ToString();
            if (!string.IsNullOrEmpty(webUrl))
            {
                using (SPSite trainingSite = new SPSite(webUrl))
                using (SPWeb trainingWeb = trainingSite.OpenWeb())
                {
                    trainingSite.AllowUnsafeUpdates = true;
                    trainingWeb.AllowUnsafeUpdates = true;

                    try
                    {
                        if (GetTaskItemMetadata.SetItemMetadata(trainingWeb, taskListId, taskItemId, "Reviewed", false))
                        {
                            btn_Cancel_Click(sender, e);// close the dialog
                        }

                    }
                    catch (Exception err)
                    {
                        LogWriting.LogExceptionToSP("btn_Reviewed_Click() - ", err);
                    }
                    trainingWeb.AllowUnsafeUpdates = false;
                    trainingSite.AllowUnsafeUpdates = false;

                }// dispose sp obj

            }
        }// end btn_reviewed_click

        // CANCEL/CLOSE BUTTON
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            // close dialog...
            Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            Context.Response.Flush();
            Context.Response.End();
        }// end btn_cancel_click

        // =================================== CLASS METHODS ==============================     

        /// <summary>
        /// Clears the message section
        /// </summary>
        private void clearPageMessage()
        {
            setPageMessage(string.Empty, false);
        }// end clearPageMessage

        /// <summary>
        /// Sets text in the message section
        /// </summary>
        /// <param name="msg">Text</param>
        /// <param name="append">Keep previous text (T/F)</param>
        private void setPageMessage(string msg, Boolean append)
        {
            hideElement(divErrorSection);
            if (!string.IsNullOrEmpty(msg))
            {
                if (append)
                {
                    divMessage.InnerHtml += "<div>" + msg + "</div>";
                }
                else
                {
                    divMessage.InnerHtml = msg;
                }
                showElement(divErrorSection);
            }
            else
            {
                divMessage.InnerHtml = msg;
            }
        }// end SetPageMessage

        private void InProgress()
        {
            hideElement(divTaskInfo);// hides all sections
            showElement(divInProgress);
        }
        private void CompleteProgress()
        {
            hideElement(divInProgress);
            showElement(divTaskInfo);
        }
        // =================================== ELEMENT SETTER HELPER ==============================
        /// <summary>
        /// Sets the dialog header and page header
        /// </summary>        
        private void setPageTitle(string taskType)
        {
            string title = string.Empty;
            divHeader.InnerHtml = title;

            // set page title
            switch (taskType)
            {
                case "assign":
                    title = "Please assign a task owner for the role below:";
                    break;
                case "review":
                    title = "Please review the training request item and mark this task as \"Review\":";
                    break;
                case "trainer":
                    title = "Please complete the training task below:";
                    break;
                case "trainingmgr":
                    title = "Please assign a trainer for the request below:";
                    break;
                default:
                    // show nothing
                    break;
            }

            divHeader.InnerHtml = title;
            litPageTitle.Text = "Task";
        }// end setpagetitle

        private void setTaskSectionBody(string taskType, string bodyString)
        {
            switch (taskType)
            {
                case "assign":
                    divAssignTaskBody.InnerHtml = bodyString;
                    break;
                case "review":
                    divReviewTask.InnerHtml = bodyString;
                    break;
                case "trainer":
                    divTrainer.InnerHtml = bodyString;
                    break;
                case "trainingmgr":
                    divTMgrBody.InnerHtml = bodyString;
                    break;
                default:
                    // do nothing
                    break;
            }

        }// end settasksectionbody
        // =================================== STYLE HELPER ==============================
        private void hideElement(HtmlGenericControl Element)
        {
            Element.Attributes["class"] = "divParentHide";
        }// end hideelement

        private void showElement(HtmlGenericControl Element)
        {
            Element.Attributes["class"] = "divParentShow";
        }// end showelement

        // hides everything
        private void resetTaskSection()
        {
            hideElement(divTaskInfo);
            hideElement(divAssignTask);
            hideElement(divReviewTask);
            hideElement(divTrainer);
            hideElement(divTrainingMgr);

            hideElement(divAssignButtons);
            hideElement(divReviewButtons);

            hideElement(divErrorSection);
            hideElement(divCloseBtn);

            hideElement(divInProgress);
        }

        private void showTaskSection(string taskType)
        {

            // hide all task sections to start fresh
            resetTaskSection();
            showElement(divTaskInfo);
            // show sections per task type
            switch (taskType)
            {
                case "assign":
                    showElement(divAssignTask);
                    showElement(divAssignButtons);

                    break;
                case "review":
                    showElement(divReviewTask);
                    showElement(divReviewButtons);

                    break;
                case "trainer":
                    showElement(divTrainer);
                    showElement(divReviewButtons);

                    break;
                case "trainingmgr":
                    showElement(divTrainingMgr);
                    showElement(divAssignButtons);

                    break;
                default:
                    // show nothing
                    break;
            }
        }// end showtasksection


    }
}
