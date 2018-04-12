using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint;
using TrainingRequest.Common.Helpers;

namespace TrainingRequest.Forms.Helpers
{
    public static class ItemHelper
    {
        /// <summary>
        /// This will create an SPListItem from the list obj and populate common fields
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filename"></param>
        /// <param name="reqTitle"></param>
        /// <param name="reqType"></param>
        /// <param name="reqDept"></param>
        /// <param name="reqDescrip"></param>
        /// <param name="reqPlannedTraining"></param>
        /// <returns></returns>
        public static SPListItem MakeDefaultItem(SPList list, string filename, string reqTitle, string reqType, string reqDept, string reqDescrip, string reqPlannedTraining)
        {
            SPListItem newItem = default(SPListItem);
            try
            {
                newItem = list.Items.Add();
                newItem[Constants.column_Title] = filename;
                newItem[Constants.column_RequestTitle] = reqTitle;
                newItem[Constants.column_RequestType] = reqType;
                newItem[Constants.column_RequestDepartment] = reqDept;
                newItem[Constants.column_RequestDescription] = reqDescrip;
                newItem[Constants.column_PlannedTraining] = reqPlannedTraining;
            }
            catch (Exception err)
            {
                LogHelper.LogErrorMessage("ItemHelper.MakeDefaultItem", err.Message, err);
            }

            return newItem;

        }// end makedefaultitem
    }// end class
}
