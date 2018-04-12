using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

/**********************************************************************
 * 
 *                      GET CONFIGURATION DATA
 *                      
 * *******************************************************************/


namespace TrainingRequestWFProj.TrainingMaterialsWF.HelperClass
{
    public partial class GetConfigurationData
    {
        // ****************************Class Variable******************************



        // **********************Class methods******************************

        /// <summary>
        /// Returns data required to create tasks and set item's new wf stage #
        /// </summary>
        public static void GetNextConfigurationData(SPWorkflowActivationProperties WorkflowProperties, string RequestType, string PlannedTraining, string CurrentWFStage, string CurrentLifecycle, string Dept, string AssignedTOLogin,
            out string NewWFStage, out string NewLifeCycle, out string TotalStage, out string TaskOwnerLogin, out string TaskContentType, out string RoleType)
        {
            string WebUrl = WorkflowProperties.WebUrl;

            NewWFStage = "999";
            NewLifeCycle = string.Empty;
            TotalStage = "0";
            TaskOwnerLogin = string.Empty;
            TaskContentType = Constants.ReviewTaskContentType;
            RoleType = string.Empty;

            using (SPSite TrainingReqSite = new SPSite(WebUrl))
            {
                using (SPWeb TrainingReqWeb = TrainingReqSite.OpenWeb())
                {
                    // Get configuration list
                    SPList ConfigurationList = default(SPList);

                    try
                    {
                        try
                        {
                            ConfigurationList = TrainingReqWeb.GetList(string.Format("{0}{1}", WebUrl, Constants.ConfigListUrl));
                        }
                        catch { }

                        if (ConfigurationList != null)
                        {
                            // Get total stage counter to ensure we have a config for the request type/planned training
                            // Also use the total to check if we are at the end of the workflow
                            TotalStage = GetTotalStageCount(WebUrl, RequestType, PlannedTraining);
                            int TotalStageCount = int.Parse(TotalStage);
                            if (TotalStageCount > 0)
                            {
                                // Get the next workflow stage # if Total Stage > 0
                                // Sets '999' flag when next stage # > total = End of the line
                                NewWFStage = GetNewWFStageOrder(CurrentLifecycle, CurrentWFStage, TotalStage);
                                if (NewWFStage != "999")
                                {
                                    // Set up query fields
                                    SPField RequestTypeField = default(SPField), PlannedTrainingField = default(SPField), StageOrderField = default(SPField);

                                    RequestTypeField = ConfigurationList.Fields.GetFieldByInternalName(Constants.RequestTypeColumnName);
                                    PlannedTrainingField = ConfigurationList.Fields.GetFieldByInternalName(Constants.PlannedTrainingColumnName);
                                    StageOrderField = ConfigurationList.Fields.GetFieldByInternalName(Constants.RequestStageOrderColumnName);

                                    // Get configuration items
                                    SPQuery ConfigQuery = new SPQuery();
                                    ConfigQuery.Query = BuildConfigurationQueryStr(RequestTypeField, RequestType,
                                        PlannedTrainingField, PlannedTraining,
                                        StageOrderField, NewWFStage.ToString(),
                                        StageOrderField, true);


                                    SPListItemCollection QueryResultItems = default(SPListItemCollection);
                                    QueryResultItems = ConfigurationList.GetItems(ConfigQuery);

                                    // return first result
                                    SPListItem Result = default(SPListItem);
                                    if (QueryResultItems.Count > 0)
                                    {
                                        Result = QueryResultItems[0];

                                        SPField TaskOwnerRoleField = ConfigurationList.Fields[Constants.TaskOwnerRoleColumnName];// Used to get the next task owner's role
                                        RoleType = TaskOwnerRoleField.GetFieldValueAsText(Result[TaskOwnerRoleField.InternalName.ToString()]);// Returns role on config item

                                        // Get the resulting lifecycle to set
                                        if (!string.IsNullOrEmpty(Result[Constants.StageLifecycleColumnName].ToString()))
                                        {
                                            NewLifeCycle = Result[Constants.StageLifecycleColumnName].ToString();
                                        }

                                        // If we were in an "assigning" lifecycle than set the next task owner as the assignee
                                        if (CurrentLifecycle == Constants.AssignTaskLifecycle)
                                        {
                                            TaskOwnerLogin = AssignedTOLogin;

                                            // [Training Manager] at planned training gets an assigned task
                                            if ((PlannedTraining == "TRUE") && (RoleType == Constants.TrainingManagerRoleString))
                                            {
                                                TaskContentType = Constants.AssignTaskContentType;
                                                RoleType = Constants.TrainerRoleString;// Training Manager's duty is to assign the Trainer so set this for proper task description
                                            }
                                        }
                                        else
                                        {

                                            if (RoleType != Constants.TrainerRoleString)
                                            {
                                                // Go to task owner list to get the configured user for the role
                                                TaskOwnerLogin = GetTaskOwnerLoginFromTaskOwnerList(TrainingReqWeb, Dept, RoleType, RequestType);
                                                if (string.IsNullOrEmpty(TaskOwnerLogin))
                                                {
                                                    // We could not find a config for the Role than an assignment task needs to be created
                                                    TaskContentType = Constants.AssignTaskContentType;
                                                    NewLifeCycle = Constants.AssignTaskLifecycle;

                                                    if (RoleType == Constants.TrainingManagerRoleString)
                                                    {
                                                        TaskOwnerLogin = WorkflowProperties.Item.File.Author.LoginName.ToString();
                                                    }
                                                    else
                                                    {
                                                        // Send task to department content manager to assign a user for [Role Type]
                                                        TaskOwnerLogin = GetTaskOwnerLoginFromTaskOwnerList(TrainingReqWeb, Dept, "[Content Manager]", RequestType);
                                                        if (string.IsNullOrEmpty(TaskOwnerLogin))
                                                        {
                                                            // Last resort, assign to creator
                                                            TaskOwnerLogin = WorkflowProperties.Item.File.Author.LoginName.ToString();
                                                        }
                                                    }

                                                }
                                                else
                                                {

                                                    // [Training Manager] should get an assigned task content type
                                                    // [Training Manager] for planned training gets an assigned task
                                                    if ((PlannedTraining == "TRUE") && (RoleType == Constants.TrainingManagerRoleString))
                                                    {
                                                        TaskContentType = Constants.AssignTaskContentType;
                                                        RoleType = Constants.TrainerRoleString;// Training Manager's duty is to assign the Trainer so set this for proper task description
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // Trainer should be returned from the request item
                                                // get trainer metadata to assign task
                                                if (!string.IsNullOrEmpty(WorkflowProperties.Item["Trainer"].ToString()))
                                                {
                                                    SPFieldUserValue TrainerFieldUser = new SPFieldUserValue(WorkflowProperties.Web, WorkflowProperties.Item["Trainer"].ToString());

                                                    if (TrainerFieldUser.User != null)
                                                    {
                                                        TaskOwnerLogin = TrainerFieldUser.User.LoginName;
                                                    }
                                                }

                                            }

                                        }

                                        //if (!string.IsNullOrEmpty(TaskOwnerLogin))
                                        //{
                                        //    TaskOwnerLogin = TrainingReqWeb.EnsureUser(TaskOwnerLogin).LoginName;
                                        //}

                                    }// end return result count

                                }// end stage count
                            }

                        }
                    }
                    catch (Exception Err)
                    {
                        LogWriting.LogExceptionToSP("GetNextConfigurationData", Err);
                    }//to properly dispose sp obj

                }// dispose spweb
            }// dispose spsite
        }// end GetConfigurationData


        /// <summary>
        /// This will query the config list for the Request Type and Planned Training and return the counter of stages
        /// </summary>
        /// <param name="WebUrl"></param>
        /// <param name="RequestType"></param>
        /// <param name="PlannedTraining"></param>
        /// <returns></returns>
        public static string GetTotalStageCount(string WebUrl, string RequestType, string PlannedTraining)
        {
            string TotalStageString = "0";

            using (SPSite TrainingSite = new SPSite(WebUrl))
            {
                using (SPWeb TrainingWeb = TrainingSite.OpenWeb())
                {
                    try
                    {
                        SPList ConfigurationList = TrainingWeb.GetList(TrainingWeb.Url.ToString() + Constants.ConfigListUrl);

                        // Set up config fields
                        SPField RequestTypeField = default(SPField), PlannedTrainingField = default(SPField), StageOrderField = default(SPField);

                        RequestTypeField = ConfigurationList.Fields.GetFieldByInternalName(Constants.RequestTypeColumnName);
                        PlannedTrainingField = ConfigurationList.Fields.GetFieldByInternalName(Constants.PlannedTrainingColumnName);
                        StageOrderField = ConfigurationList.Fields.GetFieldByInternalName(Constants.RequestStageOrderColumnName);

                        // Get a total count of stages
                        SPQuery TotalQuery = new SPQuery();
                        TotalQuery.Query = BuildTotalQueryStr(RequestTypeField, RequestType, PlannedTrainingField, PlannedTraining);

                        SPListItemCollection QueryTotalItems = default(SPListItemCollection);
                        QueryTotalItems = ConfigurationList.GetItems(TotalQuery);
                        TotalStageString = QueryTotalItems.Count.ToString();

                    }
                    catch (Exception Err)
                    {
                        LogWriting.LogExceptionToSP("GetTotalStageCount - Error querying configuration item", Err);
                    }
                }// dispose spweb
            }// dispose spsite

            return TotalStageString;

        }// end GetTotalStageCount


        /// <summary>
        /// Increments WFStage by 1 unless it is at the end
        /// </summary>
        public static string GetNewWFStageOrder(string CurrentLifecycle, string CurrentWFStageOrder, string TotalStagesCount)
        {
            string NewWFStage = "0";


            int NewWFStageCount = Convert.ToInt32(CurrentWFStageOrder);
            if (CurrentLifecycle != Constants.AssignTaskLifecycle)
            {
                NewWFStageCount = Convert.ToInt32(CurrentWFStageOrder) + 1;

            }

            if (NewWFStageCount > Convert.ToInt32(TotalStagesCount))
            {
                NewWFStageCount = 999;
            }

            NewWFStage = NewWFStageCount.ToString();

            return NewWFStage;

        }// end GetNewWFStageOrder


        /// <summary>
        /// Gets a login from the Task Owner List
        /// </summary>
        /// <param name="Dept">Request's dept</param>
        /// <param name="ReviewerType">Role Type</param>
        /// <returns>Login(s)</returns>
        public static string GetTaskOwnerLoginFromTaskOwnerList(SPWeb TRWeb, string Dept, string RoleType, string RequestType)
        {
            string TaskOwnerLogin = string.Empty;


            try
            {
                SPList TaskOwnerList = TRWeb.GetList(TRWeb.Url + Constants.TaskOwnerListUrl);

                SPField DeptField = default(SPField), RoleField = default(SPField), TaskOwnerField = default(SPField), RequestTypeField = default(SPField);
                DeptField = TaskOwnerList.Fields.GetFieldByInternalName(Constants.DeptColumnName);
                RoleField = TaskOwnerList.Fields.GetFieldByInternalName(Constants.RoleCoumnName);
                TaskOwnerField = TaskOwnerList.Fields.GetFieldByInternalName(Constants.TaskOwnerColumnName);
                RequestTypeField = TaskOwnerList.Fields.GetFieldByInternalName(Constants.RequestTypeColumnName);


                SPQuery TaskOwnerQuery = new SPQuery();

                // Roles that begins with an '*' will denote global reviewer
                // Otherwise - task owner is dependent on department and role
                if (RoleType.StartsWith(@"*"))
                {
                    TaskOwnerQuery.Query = BuildEqToQueryStr(RoleField, RoleType);
                }
                else
                {
                    TaskOwnerQuery.Query = BuildConfigurationQueryStr(DeptField, Dept, RoleField, RoleType, RequestTypeField, RequestType, RequestTypeField, true);
                }


                SPListItemCollection OwnerResults = default(SPListItemCollection);
                OwnerResults = TaskOwnerList.GetItems(TaskOwnerQuery);

                if (OwnerResults.Count > 0)
                {
                    SPListItem OwnerResult = OwnerResults[0];

                    if (!string.IsNullOrEmpty(OwnerResult[TaskOwnerField.InternalName.ToString()].ToString()))
                    {
                        string OwnerFieldValue = OwnerResult[TaskOwnerField.InternalName.ToString()].ToString();
                        SPFieldUserValueCollection OwnerValueCollection = new SPFieldUserValueCollection(TRWeb, OwnerFieldValue);
                        foreach (SPFieldUserValue OwnerUser in OwnerValueCollection)
                        {
                            if (OwnerUser.User == null)
                            {
                                // Group 
                                TaskOwnerLogin = OwnerUser.LookupValue;
                            }
                            else
                            {
                                // User
                                TaskOwnerLogin = OwnerUser.User.LoginName.ToString();
                            }
                        }// end for loop
                    }

                }

            }
            catch (Exception Err)
            {
                LogWriting.LogExceptionToSP("GetTaskOwnerLoginFromTaskOwnerList", Err);
            }

            return TaskOwnerLogin;

        }// end GetReviewerLoginFromTaskOwnerList



        /// <summary>
        /// This will take the assigned user login and add it to the Task Owner List
        /// </summary>
        /// <param name="workflowProperties"></param>
        public static void AddTaskOwnerToList(SPWeb TrainingWeb, SPWorkflowActivationProperties workflowProperties, object AssignedUsers, string Role)
        {
            string ItemLC = string.Empty, ItemRequestType = string.Empty, ItemRequestTitle = string.Empty, ItemCS = string.Empty, ItemPT = string.Empty, ItemDept = string.Empty;
            GetItemMetadata.GetMetatdata(workflowProperties, out ItemLC, out ItemRequestType, out ItemRequestTitle, out ItemCS, out ItemPT, out ItemDept);

            TrainingWeb.AllowUnsafeUpdates = true;

            try
            {
                SPList TaskOwnerList = default(SPList);
                TaskOwnerList = TrainingWeb.GetList(TrainingWeb.Url + Constants.TaskOwnerListUrl);


                if (TaskOwnerList != null)
                {
                    string DeptId = string.Empty, RoleId = string.Empty, RequestTypeId = string.Empty;
                    DeptId = GetDeptLookupID(TrainingWeb, ItemDept);
                    RoleId = GetRoleLookupID(TrainingWeb, Role);
                    RequestTypeId = GetRequestTypeLookupID(TrainingWeb, ItemRequestType);


                    SPListItem NewTaskOwnerItem = TaskOwnerList.AddItem();
                    NewTaskOwnerItem[Constants.DeptColumnName] = DeptId;
                    NewTaskOwnerItem[Constants.RoleCoumnName] = RoleId;
                    NewTaskOwnerItem[Constants.RequestTypeColumnName] = RequestTypeId;
                    NewTaskOwnerItem[Constants.TaskOwnerColumnName] = AssignedUsers;

                    NewTaskOwnerItem.Update();
                }
            }
            catch (Exception Err)
            {
                LogWriting.LogExceptionToSP("AddTaskOwnerToList - Unable to add user", Err);
            }

            TrainingWeb.AllowUnsafeUpdates = false;


        }// end AddTaskOwnerToList

        /// <summary>
        /// Returns the item ID of the supplied department
        /// </summary>
        /// <returns>Department ID</returns>
        public static string GetDeptLookupID(SPWeb TrainingWeb, string Department)
        {
            string DeptID = string.Empty;

            try
            {
                SPList DeptList = default(SPList);
                DeptList = TrainingWeb.GetList(TrainingWeb.Url.ToString() + Constants.DeptListUrl);


                if (DeptList != null)
                {
                    SPField DeptField = DeptList.Fields[Constants.DeptColumnName];

                    SPQuery DeptQuery = new SPQuery();
                    DeptQuery.Query = BuildEqToQueryStr(DeptField, Department);

                    SPListItemCollection DeptResults = DeptList.GetItems(DeptQuery);
                    if (DeptResults.Count > 0)
                    {
                        DeptID = DeptResults[0].ID.ToString();

                    }

                }
            }
            catch { }

            return DeptID;
        }


        /// <summary>
        /// Returns the item ID of the supplied Role
        /// </summary>
        /// <returns>Role ID</returns>
        public static string GetRoleLookupID(SPWeb TrainingWeb, string Role)
        {
            string RoleID = string.Empty;

            try
            {
                SPList RoleList = default(SPList);
                RoleList = TrainingWeb.GetList(TrainingWeb.Url.ToString() + Constants.TaskOwnerRolesUrl);

                if (RoleList != null)
                {
                    SPField RoleField = RoleList.Fields[Constants.RoleCoumnName];

                    SPQuery RoleQuery = new SPQuery();
                    RoleQuery.Query = BuildEqToQueryStr(RoleField, Role);

                    SPListItemCollection RoleResults = RoleList.GetItems(RoleQuery);
                    if (RoleResults.Count > 0)
                    {
                        RoleID = RoleResults[0].ID.ToString();

                    }

                }
            }
            catch { }

            return RoleID;
        }// GetRoleLookupId

        /// <summary>
        /// Returns the item ID of the supplied Request Type
        /// </summary>
        /// <returns>Item ID if found</returns>
        public static string GetRequestTypeLookupID(SPWeb TrainingWeb, string RequestType)
        {
            string RequestTypeID = string.Empty;

            SPList RequestTypeList = default(SPList);
            RequestTypeList = TrainingWeb.GetList(TrainingWeb.Url.ToString() + Constants.RequestTypeListUrl);


            if (RequestTypeList != null)
            {
                SPField RequestTypeField = RequestTypeList.Fields.GetFieldByInternalName(Constants.RequestTypeListColumnName);

                SPQuery RequestTypeQuery = new SPQuery();
                RequestTypeQuery.Query = BuildEqToQueryStr(RequestTypeField, RequestType);

                SPListItemCollection RequestTypeResults = RequestTypeList.GetItems(RequestTypeQuery);
                if (RequestTypeResults.Count > 0)
                {
                    RequestTypeID = RequestTypeResults[0].ID.ToString();
                }

            }

            return RequestTypeID;

        }// end getrequesttypelookupid


        /// <summary>
        /// Returns a 3-equals-to query string
        /// </summary>
        /// <returns>CAML query string</returns>
        public static string BuildConfigurationQueryStr(SPField Lookup1Field, string Lookup1Val, SPField Lookup2Field, string Lookup2Val, SPField Lookup3Field, string Lookup3Val, SPField OrderByField, Boolean AscendOrder)
        {
            string QueryStr = string.Empty;

            QueryStr = string.Format("<Where><And><Eq><FieldRef Name='{0}' /><Value Type='{1}'>{2}</Value></Eq><And><Eq><FieldRef Name='{3}' /><Value Type='{4}'>{5}</Value></Eq><Eq><FieldRef Name='{6}' /><Value Type='{7}'>{8}</Value></Eq></And></And></Where><OrderBy><FieldRef Name='{9}' Ascending='{10}' /></OrderBy>",
                Lookup1Field.InternalName.ToString(), Lookup1Field.Type.ToString(), Lookup1Val,
                Lookup2Field.InternalName.ToString(), Lookup2Field.Type.ToString(), Lookup2Val,
                Lookup3Field.InternalName.ToString(), Lookup3Field.Type.ToString(), Lookup3Val,
                OrderByField.InternalName.ToString(), AscendOrder.ToString());

            return QueryStr;
        }// end BuildConfigurationQUeryStr


        /// <summary>
        /// Returns a 2-equals-to query string
        /// </summary>
        /// <returns>CAML query string</returns>
        public static string BuildTotalQueryStr(SPField Lookup1Field, string Lookup1Val, SPField Lookup2Field, string Lookup2Val)
        {
            string QueryStr = string.Empty;

            QueryStr = string.Format("<Where><And><Eq><FieldRef Name='{0}' /><Value Type='{1}'>{2}</Value></Eq><Eq><FieldRef Name='{3}' /><Value Type='{4}'>{5}</Value></Eq></And></Where>",
                Lookup1Field.InternalName.ToString(), Lookup1Field.Type.ToString(), Lookup1Val,
                Lookup2Field.InternalName.ToString(), Lookup2Field.Type.ToString(), Lookup2Val);

            return QueryStr;
        }// BuildTotalQueryStr


        /// <summary>
        /// Returns a 1-equals-to query string
        /// </summary>
        /// <returns>CAML query string</returns>
        public static string BuildEqToQueryStr(SPField Lookup1Field, string Lookup1Val)
        {
            string QueryStr = string.Empty;

            QueryStr = string.Format("<Where><Eq><FieldRef Name='{0}' /><Value Type='{1}'>{2}</Value></Eq></Where>",
                Lookup1Field.InternalName.ToString(), Lookup1Field.Type.ToString(), Lookup1Val);

            return QueryStr;
        }// end BuildEqToQueryStr



    }// end class
}
