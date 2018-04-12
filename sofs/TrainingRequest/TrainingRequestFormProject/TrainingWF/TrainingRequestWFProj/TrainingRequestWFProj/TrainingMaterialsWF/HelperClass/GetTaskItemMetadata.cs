using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

using System.Collections;

namespace TrainingRequestWFProj.TrainingMaterialsWF.HelperClass
{
    public partial class GetTaskItemMetadata
    {
        /// <summary>
        /// This will query an item by list and item id
        /// </summary>
        public static SPListItem GetTaskItem(SPList List, string ItemId)
        {
            SPListItem Item = default(SPListItem);
            if (List != null)
            {
                int id = default(int);
                int.TryParse(ItemId, out id);
                if (id != 0)
                {
                    Item = List.GetItemById(id);
                }
            }

            return Item;
        }// end gettaskitem


        public static Boolean SetItemMetadata(SPWeb trainingWeb, string listGuid, string itemId, object metadata, Boolean isAssignTaskType)
        {
            Boolean isUpdated = false;
            try
            {
                if ((!string.IsNullOrEmpty(listGuid)) && (!string.IsNullOrEmpty(itemId)))
                {
                    Guid itemListGuid = new Guid(listGuid);
                    SPList itemList = trainingWeb.Lists[itemListGuid];
                    int id = 0;
                    int.TryParse(itemId, out id);// make sure itemid is a valid int

                    if (id != 0)
                    {
                        SPListItem item = itemList.GetItemById(id);
                        if (item != null)
                        {
                            // TODO - SCOPE HASHTABLE TO IF-STATEMENT SO WE DO NOT WRITE 2 SEPARATE LINES TO CREATE AND ALTER
                            if (isAssignTaskType)
                            {
                                // Assign task type
                                // set the assigntaskowner field
                                Hashtable updateMetadata = new Hashtable();
                                updateMetadata.Add(Constants.AssignedTaskOwnerColumnName, metadata);                

                                SPWorkflowTask.AlterTask(item, updateMetadata, false);                        
                                isUpdated = true;
                            }
                            else
                            {
                                // Review task type
                                Hashtable updateMetadata = new Hashtable();
                                updateMetadata.Add(item.Fields["Status"].InternalName.ToString(), metadata);

                                SPWorkflowTask.AlterTask(item, updateMetadata, false);          
                                isUpdated = true;
                            }
                        }

                    }
                }
            }
            catch (Exception err)
            {
                LogWriting.LogExceptionToSP("SetItemMetadata() - ", err);
            }
            
            return isUpdated;

        }// end setitemmetadata

    }// end class
}
