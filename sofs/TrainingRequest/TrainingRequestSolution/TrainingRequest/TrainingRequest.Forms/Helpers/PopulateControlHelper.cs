using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.UI.WebControls;
using TrainingRequest.Common.Helpers;

namespace TrainingRequest.Forms.Helpers
{
    public static class PopulateControlHelper
    {

        public static void PopulateNewRequestControl(ListItemCollection ControlCollection, ListLookupHelper.newRequestLookups lookup)
        {
            try
            {
                // Clear lookup collection
                ControlCollection.Clear();

                // Set first item as blank
                ControlCollection.Add(new ListItem(string.Empty, string.Empty));

                ListItemCollection lookupCollection = ListLookupHelper.GetNewRequestLookup(lookup);
                if (lookupCollection.Count > 0)
                {
                    foreach (ListItem item in lookupCollection)
                    {
                        ControlCollection.Add(item);
                    }
                    //ControlCollection[0].Selected = true;
                }
            }
            catch (Exception err)
            {
                LogHelper.LogErrorMessage("PopulateControlHelper.PopulateNewRequestControl()", err.Message, err);
            }
        } //end PopulateNewRequestControl

    }// end populatecontrolhelper
}
