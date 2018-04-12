using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace TrainingRequest.Common.Helpers
{
    public static class PropertyBagHelper
    {
        /// <summary>
        /// Gets the site property
        /// </summary>
        public static String GetProperty(this SPSite site, String key)
        {
            String value = String.Empty;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    if (site.RootWeb.AllProperties.ContainsKey(key))
                    {
                        value = site.RootWeb.AllProperties[key].ToString();
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorMessage("PropertyBagHelper.GetProperty(site)", ex.Message, ex);
            }

            return value;
        }

        /// <summary>
        /// Sets the site property
        /// </summary>
        public static void SetProperty(this SPSite site, String key, String value)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    if (site.RootWeb.AllProperties.ContainsKey(key))
                    {
                        site.RootWeb.AllProperties[key] = value;
                        site.RootWeb.Update();
                    }
                    else
                    {
                        site.RootWeb.AllProperties.Add(key, value);
                        site.RootWeb.Update();
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorMessage("PropertyBagHelper.SetProperty(site)", ex.Message, ex);
            }
        }

        // ==========================================================
        //                      Gets farm property bag
        // ==========================================================

        /// <summary>
        /// Gets the farm property
        /// </summary>
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
                LogHelper.LogErrorMessage("PropertyBagHelper.GetProperty(farm)", ex.Message, ex);
            }

            return value;
        }

        /// <summary>
        /// Sets the farm property
        /// </summary>
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
                LogHelper.LogErrorMessage("PropertyBagHelper.SetProperty(farm)", ex.Message, ex);
            }
        }

    }
}
