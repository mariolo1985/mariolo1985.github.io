using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

using TrainingRequest.Common.Helpers;
using TrainingRequest.Forms.Helpers;


namespace TrainingRequest.Forms.Layouts.TrainingRequest.Forms.Pages
{
    public partial class DisplayRequest : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                // get querystrings
                string listId = string.Empty, itemId = string.Empty, sourceId = string.Empty;

                listId = (Request.QueryString["List"] != null) ? Request.QueryString["List"].ToString() : string.Empty;
                itemId = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"].ToString() : string.Empty;
                sourceId = (Request.QueryString["Source"] != null) ? Request.QueryString["Source"].ToString() : string.Empty;

                if ((!string.IsNullOrEmpty(listId)) && (!string.IsNullOrEmpty(itemId)))
                {
                    renderRequestInfo(listId, itemId);
                    LoadAttachment();
                }
            }

        }// end page_load



        // ============================  ============================     
        protected void renderRequestInfo(string listId, string itemId)
        {
            try
            {
                string webUrl = SPContext.Current.Web.Url;

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite trainingSite = new SPSite(webUrl))
                    using (SPWeb trainingWeb = trainingSite.OpenWeb())
                    {
                        try
                        {
                            Guid listGuid = new Guid(listId);
                            int itemGuid = int.Parse(itemId);
                            SPList myList = trainingWeb.Lists[listGuid];
                            SPListItem myItem = myList.GetItemById(itemGuid);

                            if (myItem != null)
                            {
                                lbl_requestor.InnerText = myItem[Constants.column_TrainingRequestor].ToString();
                                lbl_requestTitle.InnerText = myItem[Constants.column_RequestTitle].ToString();
                                lbl_requestType.InnerText = myItem[Constants.column_RequestType].ToString();
                                lbl_plannedTraining.InnerText = myItem[Constants.column_PlannedTraining].ToString();
                                lbl_dept.InnerText = myItem[Constants.column_RequestDepartment].ToString();
                                lbl_description.InnerHtml = myItem[Constants.column_RequestDescription].ToString();

                                hf_thisGuid.Value = myItem[Constants.column_ThisGuid].ToString();
                                hf_parentGuid.Value = myItem[Constants.column_RootGuid].ToString();

                            }
                        }
                        catch { }

                    }// dispose sp obj
                });

            }
            catch (Exception err)
            {

                LogHelper.LogErrorMessage("DisplayRequest.renderRequestInfo", err.Message, err);
            }
        }// end renderRequestInfo

        // ============================ PAGE EVENTS ============================  



        // ============================ ATTACHMENT EVENTS ============================  
        protected void LoadAttachment()
        {
            string rootId = string.Empty, thisId = string.Empty,
                rootAttachmentMarkup = string.Empty, thisAttachmentMarkup = string.Empty;

            try
            {
                rootId = (!string.IsNullOrEmpty(hf_parentGuid.Value)) ? hf_parentGuid.Value : string.Empty;
                thisId = (!string.IsNullOrEmpty(hf_thisGuid.Value)) ? hf_thisGuid.Value : string.Empty;

                if (!string.IsNullOrEmpty(rootId))
                {
                    rootAttachmentMarkup = AttachmentHelper.GetAttachmentMarkup(AttachmentHelper.uploadTo.NewRequest, rootId);
                    ltr_rootDocuments.Text = rootAttachmentMarkup;
                }

                if (!string.IsNullOrEmpty(thisId))
                {
                    thisAttachmentMarkup = AttachmentHelper.GetAttachmentMarkup(AttachmentHelper.uploadTo.Request, thisId);
                    ltr_Materials.Text = thisAttachmentMarkup;
                }

            }
            catch (Exception err)
            {
                LogHelper.LogErrorMessage("DisplayRequest.LoadAttachment", err.Message, err);
            }

        }// end loadattachment


        // ============================ BUTTON EVENTS ============================        

        protected void UploadAttachment(object sender, EventArgs e)
        {

        }// end uploadattachment


        protected void save(object sender, EventArgs e)
        {

        }// end save
    }
}
