using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using Microsoft.Office.InfoPath;
using System.Xml;
using System.Xml.XPath;


namespace TRCore.HelperClass
{
    public partial class SecurityHelper
    {

        /// <summary>
        /// Gets the admin group from the dept list
        /// </summary>
        /// <param name="Dept"></param>
        /// <param name="webUrl"></param>
        /// <returns>User login or Group name</returns>
        public static string GetAdminGroup(string webUrl, string Dept)
        {
            string AdminPrincipalStr = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite TrainingSite = new SPSite(webUrl))
                {
                    using (SPWeb TrainingWeb = TrainingSite.OpenWeb())
                    {
                        try
                        {
                            // Get dept list
                            SPList DeptList = TrainingWeb.GetList(TrainingWeb.Url + Constants.DepartmentListURL);
                            SPField DeptField = DeptList.Fields.GetFieldByInternalName(Constants.DepartmentValueColumnName);

                            // Build query 
                            SPQuery DeptQuery = new SPQuery();
                            DeptQuery.Query = CAMLBuilderHelper.GetContainsCAMLStr(DeptField, Dept);

                            SPListItemCollection DeptResults = default(SPListItemCollection);
                            DeptResults = DeptList.GetItems(DeptQuery);

                            if (DeptResults.Count > 0)
                            {
                                // get first result
                                SPListItem DeptResult = DeptResults[0];
                                if (!string.IsNullOrEmpty(DeptResult[Constants.DepartmentAdminColumnName].ToString()))
                                {
                                    // Get Admin string
                                    SPFieldUserValue AdminFieldUser = new SPFieldUserValue(TrainingWeb, DeptResult[Constants.DepartmentAdminColumnName].ToString());
                                    if (AdminFieldUser.User != null)
                                    {
                                        // Is an spuser value
                                        AdminPrincipalStr = AdminFieldUser.User.LoginName;
                                    }
                                    else
                                    {
                                        // Is an spgroup value
                                        AdminPrincipalStr = AdminFieldUser.LookupValue;
                                    }
                                }
                            }
                        }
                        catch { }// ensure sp obj disposes


                    }// dispose spweb
                }// dispose spsite

            });


            return AdminPrincipalStr;
        }// end GetAdminGroup

        /// <summary>
        /// This will check if the current user ID matches the task owner ID
        /// </summary>
        /// <param name="Form"></param>
        /// <returns></returns>
        public static Boolean IsTaskOwner(XmlFormHostItem Form, string CurrentUserId)
        {
            Boolean UserIsTaskOwner = false;
            XmlNamespaceManager NS = Form.NamespaceManager;
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), TaskOwnerIDNode = default(XPathNavigator);

            try
            {
                TaskOwnerIDNode = root.SelectSingleNode(Constants.TaskOwnerIdXPath, NS);
                if (!string.IsNullOrEmpty(TaskOwnerIDNode.Value))
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite TrainingSite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb TrainingWeb = TrainingSite.OpenWeb())
                            {

                                SPUser TaskUser = default(SPUser);
                                SPGroup TaskGroup = default(SPGroup);

                                string[] TaskOwnerIdArray = TaskOwnerIDNode.Value.Split(';');
                                foreach (string TaskOwnerId in TaskOwnerIdArray)
                                {
                                    TaskUser = default(SPUser);
                                    try
                                    {
                                        TaskUser = TrainingWeb.EnsureUser(TaskOwnerId.Trim());
                                        if (TaskUser.LoginName.ToLower().Trim() == CurrentUserId.ToLower().Trim())
                                        {
                                            UserIsTaskOwner = true;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        TaskGroup = TrainingWeb.Groups[TaskOwnerId.Trim()];
                                        foreach (SPUser GroupUser in TaskGroup.Users)
                                        {
                                            if (GroupUser.LoginName.ToLower().Trim() == CurrentUserId.ToLower().Trim())
                                            {
                                                UserIsTaskOwner = true;
                                                break;
                                            }
                                        }// end loop
                                    }
                                }// end array loop
                            }// dispose trainingweb
                        }// dispose trainingsite
                    });
                }

            }
            catch (Exception Err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]",
                //    "IsTaskOwner", Err.Message.ToString(), Err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "IsTaskOwner", Err.Message.ToString(), Err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, Err);
            }


            return UserIsTaskOwner;

        }// end IsTaskOwner

        /// <summary>
        /// Query the dept list for admin and check if current user is the admin
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="WebUrl"></param>
        /// <returns></returns>
        public static Boolean IsAdminGroup(XmlFormHostItem Form, string CurrentUserId, string WebUrl)
        {
            Boolean UserIsAdminGroup = false;
            XmlNamespaceManager NS = Form.NamespaceManager;
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), DeptGroupNode = default(XPathNavigator);

            try
            {
                DeptGroupNode = root.SelectSingleNode(Constants.DeptXPath, NS);
                if (!string.IsNullOrEmpty(DeptGroupNode.Value))
                {
                    string AdminGroupString = GetAdminGroup(WebUrl, DeptGroupNode.Value);
                    if (!string.IsNullOrEmpty(AdminGroupString))
                    {
                        SPSecurity.RunWithElevatedPrivileges(delegate()
                        {
                            using (SPSite TrainingSite = new SPSite(WebUrl))
                            {
                                using (SPWeb TrainingWeb = TrainingSite.OpenWeb())
                                {
                                    try
                                    {
                                        // check if spuser or spgroup
                                        SPGroup AdminGroup = default(SPGroup);
                                        try
                                        {
                                            AdminGroup = TrainingWeb.Groups[AdminGroupString];
                                            foreach (SPUser Admin in AdminGroup.Users)
                                            {
                                                if (Admin.LoginName.ToLower().Trim() == CurrentUserId.ToLower().Trim())
                                                {
                                                    UserIsAdminGroup = true;
                                                    break;
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            SPUser AdminUser = TrainingWeb.EnsureUser(AdminGroupString);
                                            if ((AdminUser != null) && (AdminUser.LoginName.ToLower().ToString() == CurrentUserId.ToLower().ToString()))
                                            {
                                                UserIsAdminGroup = true;
                                            }
                                        }

                                        //SPGroup AdminGroup = default(SPGroup);
                                        //if (AdminGroupString.Contains(";"))
                                        //{
                                        //    string[] AdminGroupArray = AdminGroupString.Split(';');

                                        //    foreach (string GroupStr in AdminGroupArray)
                                        //    {
                                        //        AdminGroup = TrainingWeb.Groups[GroupStr.Trim()];
                                        //        UserIsAdminGroup = AdminGroup.ContainsCurrentUser;

                                        //        if (UserIsAdminGroup)
                                        //        {
                                        //            break;
                                        //        }
                                        //    }// end foreach
                                        //}
                                        //else
                                        //{
                                        //    AdminGroup = TrainingWeb.Groups[AdminGroupString.Trim()];
                                        //    UserIsAdminGroup = AdminGroup.ContainsCurrentUser;
                                        //}
                                    }
                                    catch (Exception Err)
                                    {
                                        //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]",
                                        //    "IsAdminGroup - Group Check block", Err.Message.ToString(), Err.StackTrace.ToString()), Form);
                                        string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "IsAdminGroup - Group Check block", Err.Message.ToString(), Err.StackTrace.ToString());
                                        LoggingHelper.LogExceptionToSP(CustomMsg, Err);
                                    }
                                }// dispose Trainingweb
                            }// dispose trainingsite
                        });
                    }

                }

            }
            catch (Exception Err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]",
                //    "IsAdminGroup", Err.Message.ToString(), Err.StackTrace.ToString()), Form);

                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "IsAdminGroup", Err.Message.ToString(), Err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, Err);

            }

            return UserIsAdminGroup;

        }// End IsAdminGroup
    }// end class
}
