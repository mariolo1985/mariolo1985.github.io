using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;



namespace Partner_CreateUserFromMasterList.Helpers
{
    public static class ProvisionUserHelper
    {
        /// <summary>
        /// Use this to start provisioning user
        /// </summary>
        public static SPUser ProvisionUser(SPSite rootSite, SPUser user, string partnerId, string partnerTitle)
        {
            SPUser createdUser = default(SPUser);
            string newLogin = string.Empty;
            string convertLogin = user.LoginName.ToString();

            Boolean isPartner = false;

            try
            {
                // check if partner account
                if (convertLogin.ToLower().Contains(Constants.ad_providerProduction))
                {
                    isPartner = true;
                }

                // delimit out just the username
                if (convertLogin.Contains('|'))
                {
                    string[] split = convertLogin.Split('|');
                    convertLogin = split[split.Length - 1];
                }

                if (convertLogin.Contains('\\'))
                {
                    string[] split = convertLogin.Split('\\');
                    convertLogin = split[1];
                }

                newLogin = convertLogin;

                //isPartner = true;// FIX ME - BEFORE RUNNING FOR PARTNERS
                // final check before creating user
                if (!string.IsNullOrEmpty(newLogin))
                {
                    // create user in ad
                    Boolean isCreated = CreateUser(rootSite, newLogin, user.Name, user.Email, partnerId, partnerTitle, isPartner);
                    if (isCreated)
                    {

                        if (isPartner)
                        {
                            // ensure SharePoint user using trusted claim...
                            string encodedClaimsLogin = LoginEncodedTrustedClaim(rootSite, newLogin);
                            LogHelper.LogMessage("ProvisionUser.EncodeClaims", "Claims: " + encodedClaimsLogin);

                            createdUser = rootSite.RootWeb.EnsureUser(encodedClaimsLogin);

                            LogHelper.LogMessage("ProvisionUser.Ensured", string.Format("UserId: {0}. Username: {1}.", createdUser.ID, createdUser.Name));

                            createdUser.Name = newLogin;
                            createdUser.Email = user.Email;
                            createdUser.Update();
                        }
                        else
                        {
                            // windows account
                            SPClaim encodeWindows = EncodeWindowsClaim(newLogin);
                            LogHelper.LogMessage("ProvisionUser.EncodeWin", "Windows: " + encodeWindows.ToEncodedString());

                            createdUser = GetUserFromClaim(rootSite, encodeWindows);
                            LogHelper.LogMessage("ProvisionUser.Ensured", string.Format("UserId: {0}. Username: {1}.", createdUser.ID, createdUser.Name));

                            createdUser.Name = newLogin;
                            createdUser.Email = user.Email;
                            createdUser.Update();
                        }
                    }
                }

            }
            catch (Exception err)
            {
                LogHelper.LogMessage("ProvisionUserHelper.ProvisionUser(user)", err.Message, err);
            }


            return createdUser;
        }// end provision user

        public static Boolean CreateUser(SPSite site, String loginName, String displayName, String email, String partnerId, String partnerTitle, Boolean isPartner)
        {
            Boolean isProvision = false;

            try
            {
                String domain = site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain);
                String container = site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserContainer);

                PrincipalContext context = default(PrincipalContext);
                if (isPartner)
                {
                    context = new PrincipalContext(ContextType.Domain, domain, container, "vkarri", "Password123");
                    LogHelper.LogMessage("ProvisionUser.Context", "Have context server" + context.ConnectedServer);
                }
                else
                {
                    context = new PrincipalContext(ContextType.Domain, domain, "vkarri", "Password123");
                    LogHelper.LogMessage("ProvisionUser.Context", "Have context server" + context.ConnectedServer);
                }


                // see if user account already exists in Active Directory...
                if (UserPrincipal.FindByIdentity(context, loginName) != null)
                {
                    // User already exists in active directory
                    isProvision = true;
                }
                else
                {
                    String password = "Password123";

                    UserPrincipal userPrincipal = new UserPrincipal(context, loginName, password, true);
                    userPrincipal.DisplayName = LoginEncodedTrustedClaim(site, loginName);
                    userPrincipal.EmailAddress = email;
                    userPrincipal.Save();
                    LogHelper.LogToTextfile("User created: " + userPrincipal.SamAccountName);

                    DirectoryEntry entry = userPrincipal.GetUnderlyingObject() as DirectoryEntry;
                    entry.Properties["company"].Value = String.Format("{0} ({1})", partnerTitle, partnerId);
                    entry.CommitChanges();

                    isProvision = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage("ProvisionUserHelper.CreateUser", ex.Message, ex);
            }

            return isProvision;
        }// end createuser

        public static void addUsersToRootVisitorsGroup(SPWeb web, SPFieldUserValueCollection userCollection)
        {
            try
            {
                foreach (SPFieldUserValue userField in userCollection)
                {
                    web.AssociatedVisitorGroup.AddUser(userField.User);
                }
            }
            catch (Exception err)
            {
                LogHelper.LogMessage("ProvisionUserHelper.addUsersToVisitorsGroup", err.Message, err);
            }
        }// end addusertovisitorsgroup

        public static String LoginEncodedTrustedClaim(SPSite site, String loginName)
        {
            String provider = String.Format("TrustedProvider:{0}", site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_ClaimsProvider));
            SPClaimProviderManager manager = SPClaimProviderManager.Local;
            SPClaim claim = new SPClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname", "devostk\\" + loginName, Microsoft.IdentityModel.Claims.ClaimValueTypes.String, provider);

            return manager.EncodeClaim(claim);
        }

        public static SPClaim EncodeWindowsClaim(String identity)
        {
            return new SPClaim(SPClaimTypes.UserLogonName, identity, "http://www.w3.org/2001/XMLSchema#string", SPOriginalIssuers.Format(SPOriginalIssuerType.Windows));
        }// end encodewindowsclaim

        public static SPUser GetUserFromClaim(SPSite site, SPClaim claim)
        {
            SPUser user = null;

            try
            {
                SPClaimProviderManager mgr = SPClaimProviderManager.Local;
                if (mgr != null)
                {
                    user = site.RootWeb.EnsureUser(claim.ToEncodedString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage("ProvisionUserHelper.GetUserFromClaim", ex.Message, ex);
            }

            return user;
        }// end getuserfromclaim


    }// end provisionuserhelper




}
