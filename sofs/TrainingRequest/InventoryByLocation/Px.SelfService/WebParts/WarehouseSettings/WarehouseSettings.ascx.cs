using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Px.Common;
using Microsoft.SharePoint;

// proof of concept
using System.Text;
using System.Web.UI.HtmlControls;


namespace Px.SelfService.WebParts.WarehouseSettings
{
    [ToolboxItemAttribute(false)]
    public partial class WarehouseSettings : WebPart
    {
        public WarehouseSettings()
        {
        }// end warehouse

        // -------------------------------------------------------
        //                  PAGE EVENTS
        // -------------------------------------------------------
        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);
                InitializeControl();

                // disable validation controls if page is not in display mode...
                if (WebPartManager.GetCurrentWebPartManager(Page).DisplayMode != WebPartManager.BrowseDisplayMode)
                {
                    foreach (var validator in Page.GetValidators(null))
                    {
                        if ((validator as BaseValidator) != null)
                        {
                            (validator as BaseValidator).Enabled = false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "WarehouseSettings.OnInit", err.Message, err.StackTrace, "WebPart", "Error");
            }
        }// end oninit

        protected void Page_Load(object sender, EventArgs e)
        {
            ChromeType = PartChromeType.None;

            try
            {
                // FIXME - TEST DATA
                addTable();

            }
            catch (Exception err)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "WarehouseSettings.Page_Load", err.Message, err.StackTrace, "WebPart", "Error");
            }


        }// end page_load

        // -------------------------------------------------------
        //                  CONTROL EVENTS
        // -------------------------------------------------------
        protected void btnEditWarehouseClick(object sender, EventArgs e)
        {

        }// end btneditwarehouseclick

        protected void btnAddWarehouseClick(object sender, EventArgs e)
        {


        }// end btnaddwarehouseclick
        // -------------------------------------------------------
        //                  MARKUP EVENTS
        // -------------------------------------------------------
        private string buildWarehouseHeader()
        {
            StringBuilder headerRow = new StringBuilder();
            string headerTag = "<th>{0}</th>";

            try
            {
                headerRow.Append(string.Format(headerTag, "Warehouse ID"));
                headerRow.Append(string.Format(headerTag, "Warehouse Name"));
                headerRow.Append(string.Format(headerTag, "Warehouse Address"));
                headerRow.Append(string.Format(headerTag, "Contact Name"));
                headerRow.Append(string.Format(headerTag, "Phone"));
                headerRow.Append(string.Format(headerTag, "Email Address"));
                headerRow.Append(string.Format(headerTag, ""));
            }
            catch (Exception err)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "WarehouseSettings.buildWarehouseHeader", err.Message, err.StackTrace, "WebPart", "Error");
            }

            return headerRow.ToString();
        }// end buildwarehousetblheader

        private string buildWarehouseRow(string wId, string wName, string wAddress, string wContactName, string wPhone, string wEmailAddress, string rowNum)
        {
            StringBuilder row = new StringBuilder();
            string dTag = "<td>{0}</td>", 
                //btnTag = "<asp:Button ID='btnEditWarehouse{0}' CommandName='CmdEditWarehouse{0}' runat='server' Text='Edit Warehouse' OnClick='btnEditWarehouseClick' class='settingsButton' />";
                btnTag = "<input type='button' ID='btnEditWarehouse{0}' Runat='server' Value='Edit Warehouse' OnClick='javascript:OpenPopUpPage(\"EditWarehouse.aspx?sid={0}&wid={0}\",null,500,700)' class='settingsButton' />";

            try
            {
                row.Append(string.Format(dTag, wId));
                row.Append(string.Format(dTag, wName));
                row.Append(string.Format(dTag, wAddress));
                row.Append(string.Format(dTag, wContactName));
                row.Append(string.Format(dTag, wPhone));
                row.Append(string.Format(dTag, wEmailAddress));
                row.Append(string.Format(dTag, string.Format(btnTag, rowNum)));// add edit button                
            }
            catch (Exception err)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "WarehouseSettings.buildWarehouseRow", err.Message, err.StackTrace, "WebPart", "Error");
            }

            return row.ToString();
        }// end buildwarehouserow


        private void addTable()
        {
            StringBuilder warehouseMarkup = new StringBuilder(), contentMarkup = new StringBuilder();
            string tblWrapper = "<table ID='tblWarehouse' runat='server' Class='settingsTable'>{0}</table>", rowTag = "<tr>{0}</tr>", dTag = "<td>{0}</td>";

            try
            {
                // build header row
                string headerRow = buildWarehouseHeader();
                warehouseMarkup.Append(headerRow);// add header row markup

                // build data row
                for (int x = 1; x < 4; x++)
                {
                    string warehouseRow = string.Empty;
                    warehouseRow = buildWarehouseRow("Id " + x, "Name " + x, "Address " + x, "Contact " + x, "Phone " + x, "Email " + x, x.ToString());
                    warehouseMarkup.Append(string.Format(rowTag, warehouseRow));

                }

                // add warehousemarkup to table
                ltr_table.Text = string.Format(tblWrapper, warehouseMarkup.ToString());

            }
            catch (Exception err)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "Warehouse.addTable", err.Message, err.StackTrace, "WebPart", "Error");
            }

        }

    }
}
