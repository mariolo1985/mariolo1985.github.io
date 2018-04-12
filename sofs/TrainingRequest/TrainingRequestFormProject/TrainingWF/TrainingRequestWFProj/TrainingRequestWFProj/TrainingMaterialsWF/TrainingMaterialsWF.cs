using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;

using TrainingRequestWFProj.TrainingMaterialsWF.HelperClass;
using System.Collections.Specialized;


namespace TrainingRequestWFProj.TrainingMaterialsWF
{
    public sealed partial class TrainingMaterialsWF : SequentialWorkflowActivity
    {
        public TrainingMaterialsWF()
        {
            InitializeComponent();
        }

        // ********************** Item properties **********************
        private string ItemReqTitle = string.Empty;
        private string ItemReqDept = string.Empty;
        private string ItemReqLifecycle = string.Empty;
        private string ItemReqType = string.Empty;
        private string ItemReqPlanned = string.Empty;


        // ********************** MANUAL CLASS VARIABLES **********************
        public Boolean ContinueLoop = true;
        public Boolean TaskActive = true;
        public string WFStatuNum = string.Empty;
        public string AssignedTaskOwnerLogin = string.Empty;


        // ********************** Auto Generated CLASS VARIABLES ************************
        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();

        public String LogHistory = default(System.String);
        public String LogHistoryOutcome = default(String);
        public int LogHistoryUserGuid = default(int);

        public Guid TrainingTaskId = default(System.Guid);
        public SPWorkflowTaskProperties TrainingTaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public String TaskContentTypeId = default(System.String);

        public SPWorkflowTaskProperties WFTaskAfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties WFTaskBeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public String CompleteTaskOutcome = default(System.String);

        // *********************** CLASS METHODS **************************

        /// <summary>
        /// Starting point of the workflow
        /// </summary>
        private void WorkflowInitiated_Invoked(object sender, ExternalDataEventArgs e)
        {
            workflowId = workflowProperties.WorkflowId;
            LogHistory = "Workflow Initiated";
            LogHistoryOutcome = "Initiated";
        }//  end workflowinitiated


        /// <summary>
        /// Tells the STAGE loop to continue/stop
        /// </summary>
        private void LoopUntil(object sender, ConditionalEventArgs e)
        {
            e.Result = ContinueLoop;
        }// end loopuntil


        /// <summary>
        /// Creates a task by specifying a content type
        /// </summary>
        private void CreateTaskByContentType_MethodInvoking(object sender, EventArgs e)
        {
            // Get Workflow item metadata to create task
            string ItemLifecycle = string.Empty, ItemRequestType = string.Empty, ItemRequestTitle = string.Empty, ItemCurrentStage = string.Empty, ItemPlannedTraining = string.Empty, ItemDept = string.Empty;

            GetItemMetadata.GetMetatdata(workflowProperties, out ItemLifecycle, out ItemRequestType, out ItemRequestTitle, out ItemCurrentStage, out ItemPlannedTraining, out ItemDept);

            // Set variables to use in later WF events
            ItemReqTitle = ItemRequestTitle;
            ItemReqDept = ItemDept;
            ItemReqLifecycle = ItemLifecycle;
            ItemReqType = ItemRequestType;
            ItemReqPlanned = ItemPlannedTraining;


            if ((!string.IsNullOrEmpty(ItemRequestType)) && (!string.IsNullOrEmpty(ItemPlannedTraining)) && (!string.IsNullOrEmpty(ItemCurrentStage)) && (!string.IsNullOrEmpty(ItemDept)))
            {

                string NewWFStage = string.Empty, NewLifeCycle = string.Empty, TotalStage = string.Empty, TaskOwnerLogin = string.Empty, CreateContentType = string.Empty, RoleType = string.Empty;
                try
                {
                    // Get Config data to create task
                    GetConfigurationData.GetNextConfigurationData(workflowProperties, ItemRequestType, ItemPlannedTraining, ItemCurrentStage, ItemLifecycle, ItemDept, AssignedTaskOwnerLogin,
                        out NewWFStage, out NewLifeCycle, out TotalStage, out TaskOwnerLogin, out CreateContentType, out RoleType);
                }
                catch (Exception err)
                {
                    LogWriting.LogExceptionToSP("CreateTaskByContentType - Unable to get next configuration data", err);
                    return;
                }

                // Get the task content type
                SPList WFTaskList = workflowProperties.TaskList;
                SPContentType TaskContentType = default(SPContentType);

                try
                {
                    TaskContentType = WFTaskList.ContentTypes[CreateContentType];
                }
                catch { }


                try
                {
                    if ((NewWFStage != "999") && (TaskContentType != null))
                    {
                        // Create a task if we have not hit the end of the workflow
                        TrainingTaskId = Guid.NewGuid();

                        string TaskDescription = string.Empty, TaskTitle = string.Empty;

                        // Check if task is to assign a role or to review the request
                        if (CreateContentType == Constants.AssignTaskContentType)
                        {
                            TaskDescription = string.Format("Please assign a {0} for request {1}", RoleType, ItemRequestTitle);
                            TaskTitle = string.Format("{0} for {1}", NewLifeCycle, ItemRequestTitle);

                            SPField RoleTypeField = TaskContentType.Fields["RoleType"];
                            TrainingTaskProperties.ExtendedProperties[RoleTypeField.Id] = RoleType;

                        }
                        else
                        {
                            TaskDescription = string.Format("Please review request {0}", ItemRequestTitle);
                            TaskTitle = NewLifeCycle;
                        }

                        TaskActive = true;// Activate our task loop
                        TaskContentTypeId = TaskContentType.Id.ToString(); // Setting the task content type
                        TrainingTaskProperties.Description = TaskDescription;
                        TrainingTaskProperties.Title = TaskTitle;


                        TrainingTaskProperties.AssignedTo = TaskOwnerLogin;
                        TrainingTaskProperties.SendEmailNotification = false;
                    }
                }
                catch (Exception Err)
                {
                    LogWriting.LogExceptionToSP("CreateTaskByContentType() - Error creating task", Err);
                }

                // Update current item lifecycle and request stage
                GetItemMetadata.SetItemMetadata(workflowProperties.WebUrl, workflowProperties.Item, NewLifeCycle, NewWFStage, TaskOwnerLogin);

                LogHistory = string.Format("CREATION: A task type of [{0}] was created and to be completed by {1}.", CreateContentType, TaskOwnerLogin);
                LogHistoryOutcome = "Task Created";
            }

        }

        /// <summary>
        /// Event triggers when task is created 
        /// 
        /// 1) Set task item permission
        /// 2)
        /// </summary>
        private void OnTaskCreatedEvent(object sender, ExternalDataEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite TrainingSite = new SPSite(workflowProperties.WebUrl))
                {
                    using (SPWeb TrainingWeb = TrainingSite.OpenWeb())
                    {
                        TrainingSite.AllowUnsafeUpdates = true;
                        TrainingWeb.AllowUnsafeUpdates = true;

                        try
                        {
                            // get training task list
                            SPList TrainingTaskList = TrainingWeb.GetList(TrainingWeb.Url.ToString() + Constants.TaskListUrl);
                            SPListItem CreatedTask = TrainingTaskList.GetItemById(WFTaskAfterProperties.TaskItemId);
                            SPListItem RequestItem = workflowProperties.Item;

                            // <<<<<<<<<<<<<<< TODO - HANDLE MULTIPLE USERS/GROUPS >>>>>>>>>>>>> 

                            // <Grant contribute to assigned user/group on task item>
                            string AssigneeAccount = TrainingTaskProperties.AssignedTo;
                            SPPrincipal TaskOwnerPrincipal = default(SPPrincipal);
                            SPRoleDefinition ContributeDef = TrainingWeb.RoleDefinitions["Contribute"]; // Contribute permission

                            // Build email body
                            string itemUrl = string.Empty, taskUrl = string.Empty, body = string.Empty, subject = string.Empty;
                            subject = string.Format("Please review the request for {0} and take action on the task assigned to you", ItemReqTitle);
                            itemUrl = string.Format("{0}/{1}?Source={0}&DefaultItemOpen=1", TrainingWeb.Url.ToString(), RequestItem.File.Url.ToString());
                            taskUrl = string.Format("{0}{1}/dispform.aspx?ID={2}", TrainingWeb.Url.ToString(), Constants.TaskListUrl, CreatedTask.ID.ToString());
                            body = EmailHelper.BuildBodyHTML(ItemReqTitle, ItemReqDept, ItemReqLifecycle, ItemReqType, ItemReqPlanned, itemUrl, taskUrl);

                            // User or group?
                            try
                            {
                                SPUser Assignee = TrainingWeb.EnsureUser(AssigneeAccount);
                                TaskOwnerPrincipal = (SPPrincipal)Assignee;

                                if (!string.IsNullOrEmpty(Assignee.Email))
                                {
                                    EmailHelper.SendNotification(TrainingWeb, Assignee.Email, subject, body);// Sends task email
                                }
                            }
                            catch
                            {
                                SPGroup AssigneeGroup = TrainingWeb.Groups[AssigneeAccount];
                                TaskOwnerPrincipal = (SPPrincipal)AssigneeGroup;

                                string address = string.Empty;
                                foreach (SPUser aUser in AssigneeGroup.Users)
                                {
                                    if (!string.IsNullOrEmpty(aUser.Email))
                                    {
                                        address += aUser.Email + ",";
                                    }
                                }// end for loop

                                if (!string.IsNullOrEmpty(address))
                                {
                                    address = address.Remove(address.LastIndexOf(","));
                                    EmailHelper.SendNotification(TrainingWeb, address, subject, body);// Sends task email
                                }
                            }


                            if (TaskOwnerPrincipal != null)
                            {
                                CreatedTask.BreakRoleInheritance(false);
                                while (CreatedTask.RoleAssignments.Count > 0)
                                {
                                    CreatedTask.RoleAssignments.Remove(0);
                                }

                                SPRoleAssignment AssigneeRole = new SPRoleAssignment(TaskOwnerPrincipal);
                                AssigneeRole.RoleDefinitionBindings.Add(ContributeDef);
                                CreatedTask.RoleAssignments.Add(AssigneeRole);
                            }

                            // <Grant contribute to admin group on task item>
                            string ItemDept = GetItemMetadata.GetMetadataByFieldName(workflowProperties.Item, Constants.ItemDeptColumnName);
                            string DeptItemId = GetConfigurationData.GetDeptLookupID(TrainingWeb, ItemDept);
                            if (!string.IsNullOrEmpty(DeptItemId))
                            {
                                SPList DeptList = TrainingWeb.GetList(TrainingWeb.Url + Constants.DeptListUrl);
                                SPListItem DeptItem = DeptList.GetItemById(Convert.ToInt32(DeptItemId));

                                SPFieldUserValue AdminFieldUser = default(SPFieldUserValue);
                                try
                                {
                                    AdminFieldUser = new SPFieldUserValue(TrainingWeb, DeptItem[Constants.DeptAdminColumnName].ToString());
                                }
                                catch { }

                                if (AdminFieldUser != null)
                                {
                                    SPPrincipal AdminPrincipal = default(SPPrincipal);
                                    if (AdminFieldUser.User != null)
                                    {
                                        // Is an spuser
                                        AdminPrincipal = (SPPrincipal)AdminFieldUser.User;
                                    }
                                    else
                                    {
                                        // Is an spgroup
                                        SPGroup AdminGroup = TrainingWeb.Groups[AdminFieldUser.LookupValue];
                                        AdminPrincipal = (SPPrincipal)AdminGroup;
                                    }

                                    SPRoleAssignment AdminRA = new SPRoleAssignment(AdminPrincipal);
                                    AdminRA.RoleDefinitionBindings.Add(ContributeDef);
                                    CreatedTask.RoleAssignments.Add(AdminRA);
                                }

                            }

                        }
                        catch (Exception Err)
                        {
                            LogWriting.LogExceptionToSP("OnTaskCreatedEvent - Error setting permissions", Err);
                        }

                        TrainingWeb.AllowUnsafeUpdates = false;
                        TrainingSite.AllowUnsafeUpdates = false;

                    }// dispose web
                }// dispose site
            });

        }// end OnTaskCreatedEvent


        /// <summary>
        /// Task changed event
        /// 
        /// 1) Checks to see if task is reviewed or owner assigned
        /// </summary>
        private void OnTaskChanged(object sender, ExternalDataEventArgs e)
        {

            string webUrl = workflowProperties.WebUrl.ToString();
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (SPSite TrainingSite = new SPSite(webUrl))
                using (SPWeb TrainingWeb = TrainingSite.OpenWeb())
                {
                    TrainingSite.AllowUnsafeUpdates = true;
                    TrainingWeb.AllowUnsafeUpdates = true;

                    try
                    {
                        // Get item content type                        
                        SPList wfTaskList = TrainingWeb.GetList(workflowProperties.TaskListUrl);
                        SPListItem TaskItem = wfTaskList.GetItemById(WFTaskAfterProperties.TaskItemId);
                        SPContentType TaskItemContentType = TaskItem.ContentType;

                        // Determine if assigning or reviewing a task
                        if (TaskItemContentType.Name == Constants.AssignTaskContentType)
                        {
                            // check if a user has been assigned
                            SPField AssignTaskOwnerField = TaskItemContentType.Fields[Constants.AssignedTaskOwnerColumnName];
                            SPField RoleTypeField = TaskItemContentType.Fields[Constants.RoleTypeColumnName];

                            // Complete task if user assigned
                            if (!string.IsNullOrEmpty(TaskItem[AssignTaskOwnerField.InternalName.ToString()].ToString()))
                            {
                                string AssignTaskOwnerFieldValue = TaskItem[AssignTaskOwnerField.InternalName.ToString()].ToString();
                                SPFieldUserValueCollection AssignTaskOwnerCollection = new SPFieldUserValueCollection(workflowProperties.Web, AssignTaskOwnerFieldValue);


                                string RoleType = TaskItem[RoleTypeField.InternalName.ToString()].ToString();
                                if (RoleType == Constants.TrainerRoleString)
                                {
                                    // handle 'Trainer' assignement by setting assigned user to item Trainer field
                                    SPListItem RequestItem = workflowProperties.Item;
                                    SPField TrainerField = RequestItem.Fields["Trainer"];
                                    string WebUrl = workflowProperties.WebUrl.ToString();

                                    GetItemMetadata.SetFieldMetadata(RequestItem, TrainerField, AssignTaskOwnerCollection);
                                }
                                else
                                {
                                    // We will add the assigned owner to the Task Owner List by department and role
                                    GetConfigurationData.AddTaskOwnerToList(TrainingWeb, workflowProperties, AssignTaskOwnerCollection, RoleType);
                                }


                                //ArrayList AllTaskOwnerArray = new ArrayList();
                                foreach (SPFieldUserValue TaskOwnerUser in AssignTaskOwnerCollection)
                                {
                                    if (TaskOwnerUser.User == null)
                                    {
                                        // Group
                                        AssignedTaskOwnerLogin = TaskOwnerUser.LookupValue;
                                        //AllTaskOwnerArray.Add(TaskOwnerUser.LookupValue);
                                    }
                                    else
                                    {
                                        // User
                                        AssignedTaskOwnerLogin = TaskOwnerUser.User.LoginName;
                                        //AllTaskOwnerArray.Add(TaskOwnerUser.User.LoginName);
                                    }

                                }// end loop

                                TaskActive = false;

                                string ModifiedByString = TaskItem["Modified By"].ToString();
                                ModifiedByString = ModifiedByString.Substring(ModifiedByString.IndexOf(@"#") + 1);
                                LogHistory = string.Format("COMPLETED: The {0} role has been assigned to {1} by {2}", RoleType, AssignedTaskOwnerLogin, ModifiedByString);
                                LogHistoryOutcome = "Role Assigned";

                                LogHistoryUserGuid = TrainingWeb.EnsureUser(ModifiedByString).ID;// the user account to log in wf history
                                // Inherit permissions from library when completed
                                TaskItem.ResetRoleInheritance();
                            }

                        }
                        else
                        {
                            // check if status is reviewed/approved
                            string Status = string.Empty;
                            SPContentType TaskContent = workflowProperties.TaskList.ContentTypes["ReviewTask"];
                            SPField StatusField = TaskItemContentType.Fields["Status"];

                            if (!string.IsNullOrEmpty(WFTaskAfterProperties.ExtendedProperties[StatusField.Id].ToString()))
                            {
                                Status = TaskItem[StatusField.InternalName.ToString()].ToString();

                                if (Status == "Reviewed")
                                {
                                    TaskActive = false;// stops task loop                                    

                                    string ModifiedByString = TaskItem["Modified By"].ToString();
                                    ModifiedByString = ModifiedByString.Substring(ModifiedByString.IndexOf(@"#") + 1);
                                    LogHistory = string.Format("COMPLETED: The task titled \"{1}\" has been reviewed by {0}", ModifiedByString, TaskItem.Title);
                                    LogHistoryOutcome = "Task Reviewed";

                                    LogHistoryUserGuid = TrainingWeb.EnsureUser(ModifiedByString).ID;
                                    TaskItem.ResetRoleInheritance();// Inherit permissions from library
                                }
                                else
                                {
                                    TaskActive = true;
                                }

                            }
                        }

                    }
                    catch (Exception Err)
                    {
                        LogWriting.LogExceptionToSP("OnTaskChanged - Verifying the task item", Err);
                    }

                    TrainingWeb.AllowUnsafeUpdates = false;
                    TrainingSite.AllowUnsafeUpdates = false;

                }// dispose SP obj
            });



        }// end OnTaskChanged

        /// <summary>
        /// -Completes the task
        /// -Checks if we should end the stage loop
        /// </summary>
        private void CompleteTask_MethodInvoking(object sender, EventArgs e)
        {
            CompleteTaskOutcome = "Completed";


            // Check if next stage is the end so we can stop the task_changed loop
            string ItemRequestType = string.Empty, ItemPlannedTraining = string.Empty, NewWFStage = string.Empty, TotalStage = string.Empty,
                CurrentLifecyle = string.Empty, CurrentWFStageOrder = string.Empty;

            // Gets info to build Total Stages
            CurrentLifecyle = GetItemMetadata.GetMetadataByFieldName(workflowProperties.Item, Constants.ItemLifecycleColumnName);
            CurrentWFStageOrder = GetItemMetadata.GetMetadataByFieldName(workflowProperties.Item, Constants.RequestStageColumnName);
            ItemRequestType = GetItemMetadata.GetMetadataByFieldName(workflowProperties.Item, Constants.ItemRequestTypeColumnName);
            ItemPlannedTraining = GetItemMetadata.GetMetadataByFieldName(workflowProperties.Item, Constants.PlannedTrainingColumnName);

            TotalStage = GetConfigurationData.GetTotalStageCount(workflowProperties.WebUrl, ItemRequestType, ItemPlannedTraining);
            NewWFStage = GetConfigurationData.GetNewWFStageOrder(CurrentLifecyle, CurrentWFStageOrder, TotalStage);


            //NewWFStage = GetConfigurationData.GetNewWFStageOrder(
            if (NewWFStage == "999")
            {

                // Update current item lifecycle and request stage
                GetItemMetadata.SetItemMetadata(workflowProperties.WebUrl, workflowProperties.Item, "Completed", NewWFStage, string.Empty);

                // end workflow
                ContinueLoop = false;
            }
            //}
        }// end completetask_method

        /// <summary>
        /// Tells workflow when the taskisactive loop to end loop
        /// </summary>
        private void TaskIsActive(object sender, ConditionalEventArgs e)
        {
            e.Result = TaskActive;
        }
    }
}
