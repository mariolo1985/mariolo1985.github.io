using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Oasis.Common;
using Partner.Administration;

namespace Partner.MiscWebParts
{
    public class PartnerHelper
    {
        public static String RemoveClaimEncoding(String identity)
        {
            if (identity.Contains("\\"))
            {
                Int32 index = identity.LastIndexOf("\\") + 1;
                identity = identity.Substring(index, identity.Length - index);
            }
            
            if (identity.Contains("|"))
            {
                Int32 index = identity.LastIndexOf("|") + 1;
                identity = identity.Substring(index, identity.Length - index);
            }

            return identity;
        }

        public static Boolean CurrentUserCanOnboard()  // either Site Collection Admin or Partner Onboarding group member...
        {
            Boolean result = false;

            try
            {
                // is current user a site collection admin?
                if (SPContext.Current.Web.CurrentUser.IsSiteAdmin)
                {
                    result = true;
                }

                // is current user a partner onboarding member?
                SPGroup onboardingGroup = SPContext.Current.Web.SiteGroups[SPContext.Current.Web.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerGroup_Onboarding)];
                if (onboardingGroup.ContainsCurrentUser)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.Pages.ChangeEmail.CurrentUserCanOnboard Error", ex, 1234);
            }

            return result;
        }


        public static Boolean CurrentUserIsAdmin()  // either Site Collection Admin, Partner Admin, or PCA
        {
            Boolean result = false;

            try
            {
                // is current user a site collection admin?
                if (SPContext.Current.Web.CurrentUser.IsSiteAdmin)
                {
                    result = true;
                }

                // is current user a partner admin?
                SPGroup partnerAdmins = SPContext.Current.Web.SiteGroups[SPContext.Current.Web.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerGroup_Admins)];
                if (partnerAdmins.ContainsCurrentUser)
                {
                    result = true;
                }

                // is current user a partner PCA?
                SPGroup partnerPCAs = SPContext.Current.Web.SiteGroups[SPContext.Current.Web.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerGroup_PCAs)];
                if (partnerPCAs.ContainsCurrentUser)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.Pages.ChangeEmail.CurrentUserIsAdmin Error", ex, 1234);
            }

            return result;
        }

        public static Boolean CurrentUserIsPartnerAdmin()
        {
            Boolean result = false;

            try
            {
                SPGroup partnerAdmins = null;

                using (SPSite site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    partnerAdmins = site.RootWeb.SiteGroups[site.WebApplication.Farm.GetProperty(PropertyBags.PartnerGroup_Admins)];
                }

                result = partnerAdmins.ContainsCurrentUser;
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.UserDirectory.CurrentUserIsPartnerAdmin Error", ex, 1234);
            }

            return result;
        }

        public static Boolean CurrentUserIsPCA()
        {
            Boolean result = false;

            try
            {
                SPGroup pcas = null;

                using (SPSite site = new SPSite(SPContext.Current.Web.Site.ID))
                {
                    pcas = site.RootWeb.SiteGroups[site.WebApplication.Farm.GetProperty(PropertyBags.PartnerGroup_PCAs)];
                }

                result = pcas.ContainsCurrentUser;
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.UserDirectory.CurrentUserIsPartnerPCA Error", ex, 1234);
            }

            return result;
        }

    }
}
