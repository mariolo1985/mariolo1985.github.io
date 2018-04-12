using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using Partner_CreateUserFromMasterList.Helpers;
using Microsoft.SharePoint;
using System.DirectoryServices.AccountManagement;

namespace Partner_CreateUserFromMasterList
{
    public partial class CreateUser : Form
    {
        public enum UserType
        {
            Partner = 1,
            PrimaryContact,
            PCA
        }

        public CreateUser()
        {
            InitializeComponent();
            setOutput("Please enter the root site collection url and the display name of Partner Master list....");
            setOutput(string.Empty);
            txb_UrlListName_OnChange();
        }

        private void txb_SiteUrl_TextChanged(object sender, EventArgs e)
        {
            txb_UrlListName_OnChange();
        }

        private void txb_MasterListName_TextChanged(object sender, EventArgs e)
        {
            txb_UrlListName_OnChange();
        }

        /****************************************************** BUTTON METHODS ******************************************************/

        private void btn_run_Click(object sender, EventArgs e)
        {
            resetProgressbar();// reset on each click
            clearOutput();

            string rootUrl = string.Empty, masterListName = string.Empty;
            rootUrl = txb_SiteUrl.Text;
            masterListName = txb_MasterListName.Text;

            try
            {

                if ((!string.IsNullOrEmpty(rootUrl)) && (!string.IsNullOrEmpty(masterListName)))
                {
                    //SPSecurity.RunWithElevatedPrivileges(delegate()
                    //{
                    using (SPSite elevatedSite = new SPSite(rootUrl))
                    using (SPSite mySite = new SPSite(rootUrl, elevatedSite.SystemAccount.UserToken))
                    using (SPWeb myWeb = mySite.OpenWeb())
                    {
                        mySite.AllowUnsafeUpdates = true;
                        myWeb.AllowUnsafeUpdates = true;

                        try
                        {

                            //// FIX ME 
                            //string userName = "zuomodern";

                            string keyDomain = mySite.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain);
                            string container = mySite.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserContainer);
                            string strMaxPwdDays = mySite.WebApplication.Farm.GetProperty(PropertyBags.Infrastructure_MaxPwdDays);
                            string strPwdNotificationDays = mySite.WebApplication.Farm.GetProperty(PropertyBags.Infrastructure_PwdNotificationDays);

                            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, keyDomain, container, "vkarri", "Password123"))
                            using (UserPrincipal principal = new UserPrincipal(ctx))
                            using (PrincipalSearcher searcher = new PrincipalSearcher(principal))
                            {
                                foreach (var result in searcher.FindAll())
                                {
                                    try
                                    {
                                        using (UserPrincipal user = UserPrincipal.FindByIdentity(ctx, result.SamAccountName))
                                        {
                                            setOutput("User: " + result.SamAccountName);

                                            if ((user != null) && (user.PasswordNotRequired))
                                            {
                                                user.PasswordNotRequired = false;
                                                user.Save();
                                                setOutput(string.Format("Updated {0} PassswordNotRequired = false", result.SamAccountName));

                                                
                                            }
                                        }
                                    }
                                    catch { }

                                }

                            }
                            return;

                            // Get master list
                            SPList masterList = myWeb.Lists[masterListName];

                            setOutput("Found root site and master list");// audit
                            setOutput(string.Empty);

                            // BATCH OUT 
                            SPQuery allMasterItemQuery = new SPQuery();
                            allMasterItemQuery.Query = "<Where><Lt><FieldRef Name='Modified' /><Value IncludeTimeValue='FALSE' Type='DateTime'>2014-04-04</Value></Lt></Where>" + string.Format("<OrderBy><FieldRef Name='{0}' Ascending='True'/></OrderBy>", Constants.column_Title);

                            //allMasterItemQuery.Query = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='True'/></OrderBy>", Constants.column_Title);
                            //allMasterItemQuery.Query = CAMLHelper.GetEqCAMLStr(masterList.Fields[Constants.column_partnerId], "92891");
                            allMasterItemQuery.RowLimit = 500;
                            allMasterItemQuery.ViewAttributes = "Scope='RecursiveAll'";

                            SPListItemCollection allMasterItems = default(SPListItemCollection);
                            do
                            {
                                allMasterItems = masterList.GetItems(allMasterItemQuery);
                                if (allMasterItems.Count == 0)
                                {
                                    setOutput("No partners found.");
                                }
                                foreach (SPListItem partnerItem in allMasterItems)
                                {
                                    incrementProgressbar(masterList.Items.Count);// increment each partner item found

                                    string title = string.Empty, partnerId = string.Empty, partnerSiteId = string.Empty, partnerSiteUrl = string.Empty,
                                        member = string.Empty, PCA = string.Empty, primaryContact = string.Empty;

                                    SPFieldUserValueCollection pcaCollection = default(SPFieldUserValueCollection), primaryContactCollection = default(SPFieldUserValueCollection),
                                        memberCollection = default(SPFieldUserValueCollection);

                                    // get item metadata
                                    title = Convert.ToString(partnerItem[Constants.column_Title]);
                                    setOutput(string.Format("Retrieved partner: {0}", title));


                                    partnerId = Convert.ToString(partnerItem[Constants.column_partnerId]);
                                    partnerSiteId = Convert.ToString(partnerItem[Constants.column_partnerSiteId]);
                                    partnerSiteUrl = Convert.ToString(partnerItem[Constants.column_partnerSiteUrl]);
                                    if (!string.IsNullOrEmpty(partnerSiteUrl))
                                    {
                                        // GET PARTNER SITE INFO
                                        string newUrl = mySite.Url + "/sites/" + partnerId;
                                        if (SiteExist(newUrl))
                                        {
                                            SPSite tempSite = new SPSite(newUrl);
                                            partnerItem[Constants.column_partnerSiteId] = tempSite.ID.ToString();
                                            partnerItem[Constants.column_partnerSiteUrl] = newUrl;
                                            tempSite.Dispose();
                                        }
                                        /***************************************** GET ACCOUNTS TO RE-PROVISION *****************************************/
                                        // PCA
                                        if ((partnerItem[Constants.column_PCA] != null) && (!string.IsNullOrEmpty(partnerItem[Constants.column_PCA].ToString())))
                                        {
                                            PCA = partnerItem[Constants.column_PCA].ToString();
                                            pcaCollection = new SPFieldUserValueCollection(myWeb, PCA);

                                            setOutput(string.Format("Found {0} PCAs", pcaCollection.Count));

                                            SPFieldUserValueCollection userCollection = default(SPFieldUserValueCollection);
                                            userCollection = StartProvisionUser(mySite, pcaCollection, partnerId, title);

                                            if (userCollection != null)
                                            {
                                                partnerItem[Constants.column_PCA] = userCollection;// update field with newly created users 

                                            }
                                            setOutput(string.Empty);
                                        }

                                        // Primary Contact
                                        if ((partnerItem[Constants.column_primaryContact] != null) && (!string.IsNullOrEmpty(partnerItem[Constants.column_primaryContact].ToString())))
                                        {
                                            primaryContact = partnerItem[Constants.column_primaryContact].ToString();
                                            primaryContactCollection = new SPFieldUserValueCollection(myWeb, primaryContact);

                                            setOutput(string.Format("Found {0} primary contact", primaryContactCollection.Count));

                                            SPFieldUserValueCollection userCollection = default(SPFieldUserValueCollection);
                                            userCollection = StartProvisionUser(mySite, primaryContactCollection, partnerId, title);

                                            if (userCollection != null)
                                            {
                                                partnerItem[Constants.column_primaryContact] = userCollection;// update field with newly created users
                                                ProvisionUserHelper.addUsersToRootVisitorsGroup(myWeb, userCollection);
                                            }
                                            setOutput(string.Empty);
                                        }

                                        // Members
                                        if ((partnerItem[Constants.column_Members] != null) && (!string.IsNullOrEmpty(partnerItem[Constants.column_Members].ToString())))
                                        {
                                            member = partnerItem[Constants.column_Members].ToString();
                                            memberCollection = new SPFieldUserValueCollection(myWeb, member);

                                            setOutput(string.Format("Found {0} members", memberCollection.Count));

                                            SPFieldUserValueCollection userCollection = default(SPFieldUserValueCollection);
                                            userCollection = StartProvisionUser(mySite, memberCollection, partnerId, title);

                                            if (userCollection != null)
                                            {
                                                partnerItem[Constants.column_Members] = userCollection;// update field with newly created users 
                                                ProvisionUserHelper.addUsersToRootVisitorsGroup(myWeb, userCollection);
                                            }
                                            setOutput(string.Empty);
                                        }

                                        // --------------------------------FIX ME - FOR DEBUGGING ONLY - stops at first-----------------------
                                        partnerItem.Update();
                                        //return;
                                    }

                                }
                                allMasterItemQuery.ListItemCollectionPosition = allMasterItems.ListItemCollectionPosition;
                            } while (allMasterItemQuery.ListItemCollectionPosition != null);


                        }
                        catch (Exception err)
                        {
                            LogHelper.LogMessage("CreateUser.btn_run_Click.InsideUsing", err.Message, err);
                            setOutput("Error: " + err.Message);
                        }

                        myWeb.AllowUnsafeUpdates = false;
                        mySite.AllowUnsafeUpdates = false;

                    }// dispose mysite/myweb

                    //});
                }
            }
            catch (Exception err)
            {
                LogHelper.LogMessage("CreateUser.btn_run_Click", err.Message, err);
                setOutput("Error: " + err.Message);
            }

        }// end run

        /****************************************************** EVENT METHODS ******************************************************/
        private void txb_UrlListName_OnChange()
        {
            if ((string.IsNullOrEmpty(txb_SiteUrl.Text)) || (string.IsNullOrEmpty(txb_MasterListName.Text)))
            {
                btn_run.Enabled = false;
            }
            else
            {
                btn_run.Enabled = true;
            }

        }// end txb_UrlListName_OnChange

        /****************************************************** MISC METHODS ******************************************************/
        private void setOutput(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                msg = Environment.NewLine;
            }
            else
            {
                msg += Environment.NewLine;
            }

            txb_output.AppendText(msg);
            LogHelper.LogToTextfile(msg);
        }// setoutput

        private void clearOutput()
        {
            txb_output.Text = string.Empty;
        }


        private Boolean SiteExist(string url)
        {
            Boolean exist = false;

            try
            {
                using (SPSite site = new SPSite(url))
                using (SPWeb web = site.OpenWeb())
                {
                    if (web.Exists)
                    {
                        exist = true;
                    }
                }// dispose sp obj

            }
            catch { }


            return exist;

        }// end siteexist
        /****************************************************** PROGRESSBAR METHODS ******************************************************/

        private void resetProgressbar()
        {
            prog_Create.Value = 0;
        }// end resetprogressbar

        private void incrementProgressbar(int numOfSites)
        {

            int incrementBy = (100 / numOfSites);
            prog_Create.Step = incrementBy;
            prog_Create.PerformStep();

        }// end increment progressbar

        /****************************************************** PROVISION METHODS ******************************************************/
        /// <summary>
        /// This will return an collection of users to add to the master list item
        /// </summary>
        private SPFieldUserValueCollection StartProvisionUser(SPSite rootSite, SPFieldUserValueCollection userCollection, string partnerId, string partnerTitle)
        {
            SPFieldUserValueCollection replacementUsers = new SPFieldUserValueCollection();

            foreach (SPFieldUserValue user in userCollection)
            {
                string login = user.User.LoginName, newLogin = string.Empty;
                setOutput(string.Format("Login: {0}", login));

                try
                {
                    SPUser createdUser = ProvisionUserHelper.ProvisionUser(rootSite, user.User, partnerId, partnerTitle);
                    if (createdUser != null)
                    {
                        LogHelper.LogMessage("CreateUser.StartProvisionUser", "UserId: " + createdUser.ID);
                        LogHelper.LogMessage("CreateUser.StartProvisionUser", "Name: " + createdUser.Name);

                        replacementUsers.Add(new SPFieldUserValue(rootSite.OpenWeb(), createdUser.ID, createdUser.Name));

                        setOutput(string.Format("New Login: {0}", createdUser.LoginName));
                    }
                }
                catch (Exception err)
                {
                    LogHelper.LogMessage("CreateUser.StartProvisionUser", err.Message, err);
                }
            }

            return replacementUsers;

        }// end StartProvisionUser
    }
}
