using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.UI;
using System.Web.UI.WebControls;


namespace TrainingRequest.Common.Helpers
{
    public static class WebControlHelper
    {
        // ============================ MISC CONTROL HELPER ============================
        public static ListItemCollection sortListItemCollection(ListItemCollection collection)
        {
            if (collection.Count > 0)
            {
                List<ListItem> tempCollection = new List<ListItem>();
                foreach (ListItem item in collection)
                {
                    tempCollection.Add(item);
                }

                IEnumerable<ListItem> sortCollection = from item in tempCollection orderby item.Value select item;
                collection.Clear();
                collection.AddRange(sortCollection.ToArray());                
            }

            return collection;

        }// end sortlistitemcollection
    }// end class
}
