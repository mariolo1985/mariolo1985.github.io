using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

using TrainingRequest.Common.Helpers;

namespace TrainingRequest.Forms.Layouts.TrainingRequest.Forms.Pages
{
    public partial class Assign : LayoutsPageBase
    {
        // ============================ PAGE EVENTS ============================      
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
                        Guid listGuid = new Guid(listId);
                        int itemGuid = int.Parse(itemId);
                        SPList myList = trainingWeb.Lists[listGuid];
                        SPListItem myItem = myList.GetItemById(itemGuid);

                        if (myItem != null)
                        {
                            txb_requestTitle.Text = myItem[Constants.column_RequestTitle].ToString();
                            txb_requestDescription.Text = myItem[Constants.column_RequestDescription].ToString();
                            txb_requestType.Text = myItem[Constants.column_RequestType].ToString();
                            txb_plannedTraining.Text = myItem[Constants.column_PlannedTraining].ToString();
                            txb_requestDept.Text = myItem[Constants.column_RequestDepartment].ToString();

                        }

                    }// dispose sp obj
                });

            }
            catch (Exception err)
            {
                LogHelper.LogErrorMessage("Assign.renderRequestInfo", err.Message, err);
            }
        }// end renderRequestInfo
    }
}
