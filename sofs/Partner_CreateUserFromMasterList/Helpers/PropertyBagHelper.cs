using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace Partner_CreateUserFromMasterList.Helpers
{
    public static class PropertyBagHelper
    {
        public static String GetProperty(this SPFarm farm, String key)
        {
            String value = String.Empty;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    if (farm.Properties.ContainsKey(key))
                    {
                        value = farm.Properties[key].ToString();
                    }
                });
            }
            catch (Exception ex)
            {
                string msg = string.Format("Unable to get key: {0} - {1}", key, ex.Message);
                LogHelper.LogMessage("CreateUser.PropertyBagHelper.GetProperty", msg, ex);
            }

            return value;
        }

        public static void SetProperty(this SPFarm farm, String key, String value)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    if (farm.Properties.ContainsKey(key))
                    {
                        farm.Properties[key] = value;
                        farm.Update();
                    }
                    else
                    {
                        farm.Properties.Add(key, value);
                        farm.Update();
                    }
                });
            }
            catch (Exception ex)
            {
                string msg = string.Format("Unable to get key: {0} - {1}", key, ex.Message);
                LogHelper.LogMessage("CreateUser.PropertyBagHelper.SetProperty", msg, ex);
            }
        }
    }
}
