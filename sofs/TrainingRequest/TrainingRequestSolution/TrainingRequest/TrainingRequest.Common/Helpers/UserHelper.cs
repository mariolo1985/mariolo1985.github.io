using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;

namespace TrainingRequest.Common.Helpers
{
    public static class UserHelper
    {
        public static string GetCurrentUserLoginName()
        {
            string currentUser = SPContext.Current.Web.CurrentUser.LoginName;
            if (SPClaimProviderManager.IsClaimsUser())
            {
                SPClaimProviderManager mgr = SPClaimProviderManager.Local;
                if (mgr != null)
                {
                    currentUser = mgr.DecodeClaim(SPContext.Current.Web.CurrentUser.LoginName).Value;

                }
            }

            if (!string.IsNullOrEmpty(currentUser) && currentUser.Contains("\\"))
            {
                string[] userArray = currentUser.Split('\\');
                currentUser = userArray[1];
            }


            return currentUser;
        }

    }// end class
}
