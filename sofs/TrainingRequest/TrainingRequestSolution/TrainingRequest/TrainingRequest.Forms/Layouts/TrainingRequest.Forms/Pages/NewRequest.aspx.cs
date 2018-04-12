using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

using System.Web.UI.WebControls;
using System.Collections;
using TrainingRequest.Forms.Helpers;
using TrainingRequest.Common.Helpers;
using System.Text;

namespace TrainingRequest.Forms.Layouts.TrainingRequest.Forms.Pages
{
    public partial class NewRequest : LayoutsPageBase
    {
        private Guid rootGuid
        {
            get
            {
                Guid tempRootGuid = default(Guid);

                if (string.IsNullOrEmpty(hf_rootGuid.Value))
                {
                    tempRootGuid = Guid.NewGuid();
                    hf_rootGuid.Value = tempRootGuid.ToString();
                    return tempRootGuid;
                }
                else
                {
                    tempRootGuid = Guid.Parse(hf_rootGuid.Value);
                    return tempRootGuid;
                }

            }
            set { ;}

        }

        // ============================ PAGE EVENTS ============================        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                // Populate lookups
                PopulateControlHelper.PopulateNewRequestControl(ddl_requestType.Items, ListLookupHelper.newRequestLookups.RequestType);
                ddl_requestType.ClearSelection();

                PopulateControlHelper.PopulateNewRequestControl(lsb_SourceDept.Items, ListLookupHelper.newRequestLookups.Department);
                lsb_SourceDept.ClearSelection();
            }
        }// end page_load


        // ============================ CONTROL EVENTS ============================

        /// <summary>
        /// Select/Remove button click
        /// </summary>
        protected void btn_DeptClick(object sender, CommandEventArgs e)
        {

            try
            {
                string cmd = e.CommandName;
                ListBox sourceLsb = default(ListBox), destLsb = default(ListBox); // source and destination
                switch (cmd.ToLower())
                {
                    case "add":
                        sourceLsb = lsb_SourceDept;
                        destLsb = lsb_SelectedDept;
                        break;

                    case "remove":
                        sourceLsb = lsb_SelectedDept;
                        destLsb = lsb_SourceDept;
                        break;

                }

                // check if anything is selected
                if (sourceLsb.SelectedItem == null)
                {
                    return;// dev exit
                }

                ListItem selectedItem = sourceLsb.SelectedItem;
                if (!string.IsNullOrEmpty(selectedItem.Value))
                {
                    selectedItem.Selected = false;
                    // remove from source
                    sourceLsb.Items.Remove(selectedItem);


                    // add selected
                    ListItemCollection curSelectedCollection = new ListItemCollection();
                    foreach (ListItem tempItem in destLsb.Items)
                    {
                        curSelectedCollection.Add(tempItem);
                    }
                    curSelectedCollection.Add(selectedItem);
                    curSelectedCollection = WebControlHelper.sortListItemCollection(curSelectedCollection);

                    if (curSelectedCollection.Count > 0)
                    {
                        destLsb.Items.Clear(); // clear destination listbox                   
                        if (curSelectedCollection.FindByValue(string.Empty) == null)
                        {
                            // always have an empty listitemto default selected
                            destLsb.Items.Add(new ListItem(string.Empty, string.Empty));
                        }
                        // add to listbox
                        foreach (ListItem item in curSelectedCollection)
                        {
                            item.Selected = false;
                            destLsb.Items.Add(item);
                        }
                    }
                    // make sure there are no values selected
                    sourceLsb.ClearSelection();
                    destLsb.ClearSelection();
                }
            }
            catch (Exception err)
            {
                LogHelper.LogErrorMessage("NewRequest.btn_DeptClick", err.Message, err);
            }
        } // end btn_adddeptclick

        // For selection listboxes
        protected void OnSelectedChange(object sender, EventArgs e)
        {
            ListBox lsb = (ListBox)sender;
            string lsbId = lsb.ID;

            if (lsbId.ToLower().Contains("source"))
            {
                if (string.IsNullOrEmpty(lsb.SelectedValue))
                {
                    //disable add btn

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "disableBtn('btn_addDept'");

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "enableBtn('btn_addDept'");
                }

            }
            else if (lsbId.ToLower().Contains("select"))
            {
                if (string.IsNullOrEmpty(lsb.SelectedValue))
                {
                    //disable remove btn
                }
                else
                {

                }
            }

        }

        /// <summary>
        /// Uploads the selected file
        /// </summary>
        protected void UploadAttachment(object sender, EventArgs e)
        {
            try
            {

                AttachmentHelper.UploadAttachment(file_supportingDocs, AttachmentHelper.uploadTo.NewRequest, rootGuid.ToString());
                loadAttachments();
            }
            catch (Exception err)
            {
                LogHelper.LogErrorMessage("NewRequest.UploadAttachment", err.Message, err);
            }

        }// end uploadattachment

        protected void loadAttachments()
        {
            string attachmentMarkup = AttachmentHelper.GetAttachmentMarkup(AttachmentHelper.uploadTo.NewRequest, rootGuid.ToString());
            ltr_attachmentLinks.Text = attachmentMarkup;

            if (!string.IsNullOrEmpty(attachmentMarkup))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script type='text/javascript'>showSupportDocPanel();</script>");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script type='text/javascript'>hideSupportDocPanel();</script>");

            }

        }
        // ============================ SUBMIT EVENTS ============================

        protected string generateFilename(string requestTitle, string dept)
        {
            string fn = string.Empty;

            fn = string.Format("{0}_{1}_{2}", requestTitle, dept, DateTime.Now.ToString("MMddyyyyhhmmss"));

            return fn;
        }

        /// <summary>
        /// Creates an item per dept in New Request and Pending Assignment list
        /// </summary>
        protected void submitRequest(object sender, EventArgs e)
        {
            string webUrl = string.Empty, requestTitle = string.Empty, requestType = string.Empty,
                plannedTraining = string.Empty, requestDescription = string.Empty, requestorLogin = string.Empty;
            ArrayList selectedDept = new ArrayList();

            try
            {
                webUrl = SPContext.Current.Web.Url;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    // submit to pending assignment library
                    using (SPSite TrainingSite = new SPSite(webUrl))
                    using (SPWeb TrainingWeb = TrainingSite.OpenWeb())
                    {

                        SPList requestList = default(SPList);
                        try
                        {
                            requestList = TrainingWeb.GetList(Constants.url_Request);
                        }
                        catch { }

                        if (requestList != null)
                        {
                            TrainingSite.AllowUnsafeUpdates = true;
                            TrainingWeb.AllowUnsafeUpdates = true;


                            // GET REQUEST INFO                            
                            requestTitle = txb_requestTitle.Text;
                            requestType = ddl_requestType.SelectedValue;
                            plannedTraining = (rad_plannedTrainingY.Checked) ? rad_plannedTrainingY.Text : rad_plannedTrainingN.Text;
                            requestDescription = txb_requestDescription.Text;
                            requestorLogin = UserHelper.GetCurrentUserLoginName();


                            if (lsb_SelectedDept.Items.Count > 0)
                            {
                                // Get all selected dept
                                foreach (ListItem item in lsb_SelectedDept.Items)
                                {
                                    if (!string.IsNullOrEmpty(item.Value))
                                        selectedDept.Add(item.Value);
                                }
                            }

                            // CREATE REQUEST PER DEPT
                            try
                            {

                                foreach (string dept in selectedDept)
                                {
                                    Guid rtGuid = Guid.NewGuid();// this root request
                                    if (rootGuid != Guid.Empty)
                                    {
                                        rtGuid = rootGuid;
                                    }
                                    Guid parentGuid = Guid.NewGuid();// child request split per dept
                                    Guid childGuid = Guid.NewGuid(); // pending assignment task per child request

                                    requestorLogin = (requestorLogin.Contains(@"|")) ? requestorLogin.Substring(requestorLogin.LastIndexOf(@"|") + 1) : requestorLogin;

                                    //// CREATE PENDING ASSIGNMENT TASK
                                    //SPListItem pendingRequestItem = ItemHelper.MakeDefaultItem(pendingList, generateFilename(requestTitle, dept),
                                    //    requestTitle, requestType, dept, requestDescription, plannedTraining);
                                    //if (pendingRequestItem != null)
                                    //{
                                    //    pendingRequestItem[Constants.column_TrainingRequestor] = TrainingWeb.EnsureUser(requestorLogin).ID;
                                    //    pendingRequestItem[Constants.column_ParentGuid] = parentGuid.ToString();
                                    //    pendingRequestItem[Constants.column_ThisGuid] = childGuid.ToString();
                                    //    pendingRequestItem[Constants.column_RootGuid] = rtGuid.ToString();
                                    //    pendingRequestItem.Update();
                                    //}

                                    // CREATE CHILD REQUEST
                                    SPListItem newRequestItem = ItemHelper.MakeDefaultItem(requestList, generateFilename(requestTitle, dept),
                                        requestTitle, requestType, dept, requestDescription, plannedTraining);
                                    if (newRequestItem != null)
                                    {
                                        newRequestItem[Constants.column_TrainingRequestor] = TrainingWeb.EnsureUser(requestorLogin).ID;
                                        newRequestItem[Constants.column_ThisGuid] = parentGuid.ToString();
                                        newRequestItem[Constants.column_RootGuid] = rtGuid.ToString();
                                        newRequestItem[Constants.column_requestStatus] = "New";
                                        newRequestItem.Update();
                                    }

                                }
                                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script type='text/javascript'>showSubmitMessage('success');</script>");                                

                            }
                            catch (Exception err)
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script type='text/javascript'>showSubmitMessage('fail');</script>");
                                LogHelper.LogErrorMessage("NewRequest.submitRequest", err.Message, err);
                            }

                        }

                        TrainingSite.AllowUnsafeUpdates = false;
                        TrainingWeb.AllowUnsafeUpdates = false;

                    }// dispose sp object

                });

            }
            catch (Exception err)
            {
                LogHelper.LogErrorMessage("NewRequest.submitRequest", err.Message, err);
            }

        }// end submitrequest

    }
}
