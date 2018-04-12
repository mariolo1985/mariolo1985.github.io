using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;


/******************************************************************
 * 
 *      THIS WILL HANDLE GETTING WORKFLOW ITEM METADATA
 *      
 * ****************************************************************/

namespace TrainingRequestWFProj.TrainingMaterialsWF.HelperClass
{
    public partial class GetItemMetadata
    {
        // ****************************Class Variable******************************



        // **********************Class methods******************************
        /// <summary>
        /// Pulls metadata from listitem that the workflow is running on
        /// </summary>
        public static void GetMetatdata(SPWorkflowActivationProperties WFItemProperties, out string ItemCurrentLifecycle, out string RequestType, out string RequestTitle, out string CurrentStage, out string PlannedTraining, out string Dept)
        {
            // Get Workflow Item Stage
            ItemCurrentLifecycle = string.Empty;
            RequestType = string.Empty;
            RequestTitle = string.Empty;
            CurrentStage = string.Empty;
            PlannedTraining = string.Empty;
            Dept = string.Empty;


            // Get item that the wf is running on
            using (SPSite WorkflowItemSite = new SPSite(WFItemProperties.SiteUrl))
            {
                using (SPWeb WorkflowItemWeb = WorkflowItemSite.OpenWeb())
                {

                    try
                    {
                        int ItemId = WFItemProperties.ItemId;
                        string WorkflowListURL = WFItemProperties.Web.Url + "/" + WFItemProperties.List.RootFolder.Url;
                        SPList WorkflowItemList = WorkflowItemWeb.GetList(WorkflowListURL);

                        SPListItem WorkflowItem = default(SPListItem);
                        WorkflowItem = WorkflowItemList.GetItemById(ItemId);

                        if (WorkflowItem != null)
                        {
                            // Get lifecycle                             
                            ItemCurrentLifecycle = GetMetadataByFieldName(WorkflowItem, Constants.ItemLifecycleColumnName);


                            // Get Request Type                          
                            RequestType = GetMetadataByFieldName(WorkflowItem, Constants.ItemRequestTypeColumnName);


                            // Get Request Title
                            RequestTitle = GetMetadataByFieldName(WorkflowItem, Constants.ItemRequestTitleColumnName);


                            // Get Current Stage #
                            CurrentStage = GetMetadataByFieldName(WorkflowItem, Constants.RequestStageColumnName);

                            // Get Planned Training (t/f)
                            PlannedTraining = GetMetadataByFieldName(WorkflowItem, Constants.PlannedTrainingColumnName);

                            // Get Department
                            Dept = GetMetadataByFieldName(WorkflowItem, Constants.DeptColumnName);
                        }
                    }
                    catch (Exception err)
                    { LogWriting.LogExceptionToSP("GetMetadata", err); }

                }// dispose spsite
            }// dispose spsite

        }// end GetMetatdata

        public static string GetMetadataByFieldName(SPListItem RequestItem, string FieldInternalName)
        {
            string Metadata = string.Empty;

            if (!string.IsNullOrEmpty(RequestItem[FieldInternalName].ToString()))
            {
                Metadata = RequestItem[FieldInternalName].ToString();
            }

            return Metadata;

        }

        /// <summary>
        /// Sets lifecycle, request stage and TaskOwner field
        /// </summary>
        public static void SetItemMetadata(string WebUrl, SPListItem RequestItem, string NewLifecycle, string NewRequestStage, string NewTaskOwner)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite TRSite = new SPSite(WebUrl))
                {
                    using (SPWeb TRWeb = TRSite.OpenWeb())
                    {
                        TRSite.AllowUnsafeUpdates = true;
                        TRWeb.AllowUnsafeUpdates = true;

                        try
                        {
                            RequestItem[Constants.ItemLifecycleColumnName] = NewLifecycle;
                            RequestItem[Constants.RequestStageColumnName] = NewRequestStage;
                            RequestItem[Constants.TaskOwnerStrColumnName] = NewTaskOwner;

                            // Handle task owner people/group field
                            if (!string.IsNullOrEmpty(NewTaskOwner))
                            {
                                SPUser TaskOwnerUser = default(SPUser);
                                try
                                {
                                    TaskOwnerUser = TRWeb.EnsureUser(NewTaskOwner);
                                    RequestItem[Constants.TaskOwnerPeopleColumnName] = TaskOwnerUser;
                                }
                                catch
                                {
                                    SPGroup TaskOwnerGroup = TRWeb.Groups[NewTaskOwner];
                                    RequestItem[Constants.TaskOwnerPeopleColumnName] = TaskOwnerGroup;
                                }

                            }

                            RequestItem.SystemUpdate();

                        }
                        catch (Exception Err)
                        { LogWriting.LogExceptionToSP("SetItemMetadata", Err); }

                        TRWeb.AllowUnsafeUpdates = false;
                        TRSite.AllowUnsafeUpdates = false;
                    }// dispose spweb
                }// dispose spsite
            });

        }// end SetItemMetadata

        /// <summary>
        /// Sets value to the metafield
        /// </summary>
        public static void SetFieldMetadata(SPListItem Item, SPField MetaField, object Value)
        {

            try
            {
                Item[MetaField.InternalName.ToString()] = Value;
                Item.SystemUpdate();
            }
            catch (Exception Err)
            { LogWriting.LogExceptionToSP("SetFieldMetadata - Error setting " + MetaField.InternalName.ToString(), Err); }



        }// end SetFieldMetadata
    }// end class
}
