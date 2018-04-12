using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using Microsoft.Office.InfoPath;
using Microsoft.SharePoint;
using System.Collections;

namespace TRCore.HelperClass
{
    public static class AttachmentHelper
    {


        /// <summary>
        /// This will return an SPFolder in the materials/attachment library that is created for the [formname]
        /// </summary>
        /// <param name="FormName"></param>
        /// <param name="WebURL"></param>
        /// <returns></returns>
        public static SPFolder GetAttachmentSPFolder(string FormName, SPWeb TRWeb, XmlFormHostItem Form)
        {

            SPFolder AttachmentFolder = default(SPFolder);

            try
            {
                SPList AttachmentList = TRWeb.GetList(TRWeb.Url.ToString() + Constants.AttachmentLibURL); // Gets attachment library for reference info
                SPField TitleField = AttachmentList.Fields.GetFieldByInternalName("Title"); // Get reference to Title field on attachment library

                // Query attachment library for folder named FormName
                SPQuery FolderQuery = new SPQuery();
                FolderQuery.RowLimit = 1;
                FolderQuery.ViewAttributes = "Scope='RecursiveAll'";
                FolderQuery.Query = CAMLBuilderHelper.GetEqFolderQueryStr(TitleField, FormName);

                SPListItemCollection FolderQueryResults = AttachmentList.GetItems(FolderQuery);

                // check if folder exist
                string folderURL = string.Format("{0}/{1}/{2}", TRWeb.Url.ToString(), AttachmentList.RootFolder.Url, FormName),
                    LibraryURL = string.Format("{0}/{1}", TRWeb.Url.ToString(), AttachmentList.RootFolder.Url);

                // Check if folder exist or not
                if (FolderQueryResults.Count > 0)
                {
                    AttachmentFolder = TRWeb.GetFolder(folderURL);
                }
                else
                {
                    // Does not exist - Create folder and set metadata
                    SPListItem AttachmentLibItem = default(SPListItem);

                    try
                    {
                        // Creates folder as formname
                        AttachmentLibItem = AttachmentList.Items.Add(LibraryURL, SPFileSystemObjectType.Folder, FormName);
                    }
                    catch { }

                    // Set folder title
                    if (AttachmentLibItem != null)
                    {
                        AttachmentLibItem[TitleField.InternalName.ToString()] = FormName;
                        AttachmentLibItem.Update();

                        // Get folder to return
                        AttachmentFolder = TRWeb.GetFolder(folderURL);
                    }
                }



            }
            catch (Exception err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "GetAttachmentSPFolder", err.Message.ToString(), err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "GetAttachmentSPFolder", err.Message.ToString(), err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, err);
            }

            return AttachmentFolder;

        }// End GetAttachmentSPFolder

        /// <summary>
        /// This will upload the attachment and create a folder in the doc lib is it does not exist
        /// </summary>
        /// <param name="FormName"></param>
        /// <param name="AttachmentObjStr"></param>
        /// <param name="WebURL"></param>
        /// <returns>True: Upload successful. False: Upload unsuccessful</returns>
        public static Boolean UploadAttachment(string FormName, string AttachmentObjStr, string WebURL, XmlFormHostItem Form)
        {

            Boolean IsSuccessful = false;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite TRSite = new SPSite(WebURL))
                    {

                        using (SPWeb TRWeb = TRSite.OpenWeb())
                        {

                            TRSite.AllowUnsafeUpdates = true;
                            TRWeb.AllowUnsafeUpdates = true;

                            try
                            {
                                string AttachmentFilename = string.Empty;

                                // Decode attachment string to byte array                            
                                AttachmentDecoder ADecoder = new AttachmentDecoder(AttachmentObjStr);


                                byte[] DecodedAttachment = ADecoder.GetDecodedAttachment();

                                if (DecodedAttachment != null)
                                {
                                    AttachmentFilename = ADecoder.FileName;

                                    // Upload to materials document library
                                    SPFolder FormMaterialsFolder = GetAttachmentSPFolder(FormName, TRWeb, Form);
                                    if (FormMaterialsFolder != null)
                                    {
                                        Hashtable MaterialMetadata = new Hashtable();
                                        MaterialMetadata.Add(Constants.ALFormNameFieldName, FormName);

                                        SPFile MaterialFile = FormMaterialsFolder.Files.Add(AttachmentFilename, DecodedAttachment, MaterialMetadata, true);
                                        MaterialFile.Update();

                                        // Publish as major version
                                        MaterialFile.Publish(string.Empty);

                                        //// Set attachment metadata
                                        //SPListItem MaterialItem = MaterialFile.Item;
                                        //MaterialItem.Properties["FormName"] = FormName;
                                        //MaterialItem.Update();

                                        IsSuccessful = true;
                                    }
                                }

                            }
                            catch (Exception err)
                            {
                                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "UploadAttachment - Inside Using", err.Message.ToString(), err.StackTrace.ToString()), Form);
                                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "UploadAttachment - Inside Using", err.Message.ToString(), err.StackTrace.ToString());
                                LoggingHelper.LogExceptionToSP(CustomMsg, err);
                            }


                            TRWeb.AllowUnsafeUpdates = false;
                            TRSite.AllowUnsafeUpdates = false;

                        }// end TRWeb
                    }// end TRSite
                });
            }
            catch (Exception err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "UploadAttachment - Outside Using", err.Message.ToString(), err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "UploadAttachment - Outside Using", err.Message.ToString(), err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, err);

            }

            return IsSuccessful;
        }// End uploadattachment

        /// <summary>
        /// Clears all attachment links
        /// </summary>
        public static void ClearAttachmentSection(XmlFormHostItem Form)
        {

            XPathNavigator root = Form.MainDataSource.CreateNavigator(), FirstNode = default(XPathNavigator), LastNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;

            try
            {
                string FirstNodeXPath = string.Format("{0}[1]", Constants.AttachmentGroupXPath), LastNodeXPath = string.Format("{0}[last()]", Constants.AttachmentGroupXPath);
                FirstNode = root.SelectSingleNode(FirstNodeXPath, NS);
                LastNode = root.SelectSingleNode(LastNodeXPath, NS);

                if ((FirstNode != null) && (LastNode != null))
                {
                    FirstNode.DeleteRange(LastNode);
                }
            }
            catch (Exception err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "ClearAttachmentSection", err.Message.ToString(), err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "ClearAttachmentSection", err.Message.ToString(), err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, err);

            }

        }// end ClearAttachmentSection


        public static void LoadAttachments(XmlFormHostItem Form, string FormName, string WebURL)
        {
            if (string.IsNullOrEmpty(FormName))
            {
                // Exit if form name is not supplied
                return;
            }

            XPathNavigator root = Form.MainDataSource.CreateNavigator();
            XmlNamespaceManager NS = Form.NamespaceManager;

            using (SPSite TRSite = new SPSite(WebURL))
            {
                using (SPWeb TRWeb = TRSite.OpenWeb())
                {

                    try
                    {
                        // get materials list
                        SPList MaterialsList = TRWeb.GetList(TRWeb.Url.ToString() + Constants.AttachmentLibURL);
                        if (MaterialsList != null)
                        {
                            SPField FormNameField = MaterialsList.Fields.GetFieldByInternalName(Constants.ALFormNameFieldName);

                            // build query to get material attachments via formname field and value
                            SPQuery FormMaterialQuery = new SPQuery();
                            FormMaterialQuery.Query = CAMLBuilderHelper.GetEqCAMLStr(FormNameField, FormName);
                            FormMaterialQuery.ViewAttributes = "Scope='RecursiveAll'";
                            FormMaterialQuery.RowLimit = 200;

                            SPListItemCollection FormMaterialResults = default(SPListItemCollection);
                            do
                            {
                                // Hit query
                                FormMaterialResults = MaterialsList.GetItems(FormMaterialQuery);

                                // iterate each result to build the attachment section
                                foreach (SPListItem FormMaterial in FormMaterialResults)
                                {
                                    string Filename = string.Empty, FileURL = string.Empty, VersionNum = string.Empty;

                                    // Get filename
                                    if (!string.IsNullOrEmpty(FormMaterial.File.Name))
                                    {
                                        Filename = FormMaterial.File.Name;
                                    }

                                    // Get fileurl
                                    if (!string.IsNullOrEmpty(FormMaterial.Url))
                                    {
                                        FileURL = string.Format("{0}/{1}", TRWeb.Url, FormMaterial.Url);
                                    }

                                    // Get version #
                                    if (!string.IsNullOrEmpty(FormMaterial.File.UIVersionLabel))
                                    {
                                        VersionNum = FormMaterial.File.UIVersionLabel;
                                    }

                                    // Build a row in the attachment table
                                    List<RepeaterNodeHandler> ChildNodeHolder = new List<RepeaterNodeHandler>();

                                    RepeaterNodeHandler FileNameNodeHandler = new RepeaterNodeHandler(Constants.AttachmentNodeName, Filename);
                                    ChildNodeHolder.Add(FileNameNodeHandler);

                                    RepeaterNodeHandler FileUrlNodeHandler = new RepeaterNodeHandler(Constants.AttachmentUrlNodeName, FileURL);
                                    ChildNodeHolder.Add(FileUrlNodeHandler);

                                    RepeaterNodeHandler FileVersionNodeHandler = new RepeaterNodeHandler(Constants.AttachmentVersionNodeName, VersionNum);
                                    ChildNodeHolder.Add(FileVersionNodeHandler);

                                    RepeaterNodeHandler FileRemoveNodeHandler = new RepeaterNodeHandler(Constants.AttachmentRemoveNodeName, "FALSE");
                                    ChildNodeHolder.Add(FileRemoveNodeHandler);

                                    FormHelpers.PopulateRepeatingSectionChilds(Constants.AttachmentParentXPath, Constants.AttachmentGroupName, ChildNodeHolder, Form);


                                }// end foreach

                                // Check if result is at the end of the 'line'
                                FormMaterialQuery.ListItemCollectionPosition = FormMaterialResults.ListItemCollectionPosition;
                            } while (FormMaterialQuery.ListItemCollectionPosition != null);
                        }
                    }
                    catch (Exception Err)
                    {
                        //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "LoadAttachments", Err.Message.ToString(), Err.StackTrace.ToString()), Form);
                        string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "LoadAttachments", Err.Message.ToString(), Err.StackTrace.ToString());
                        LoggingHelper.LogExceptionToSP(CustomMsg, Err);
                    }

                }// dispose TRWeb
            }// dispose TRSite

        }// end LoadAttachments


        /// <summary>
        /// Deletes the selected attachments/materials
        /// </summary>
        /// <param name="WebUrl"></param>
        /// <param name="Form"></param>
        public static void DeleteAttachment(string WebUrl, XmlFormHostItem Form)
        {
            XPathNavigator root = Form.MainDataSource.CreateNavigator();
            XmlNamespaceManager NS = Form.NamespaceManager;
            XPathNodeIterator AttachmentGroup = root.Select(Constants.AttachmentGroupXPath, NS);
            ArrayList AttachmentsToRemove = new ArrayList();

            try
            {
                // loop the group for a 'checked' remove
                while (AttachmentGroup.MoveNext())
                {
                    XPathNavigator ChildRemoveNode = AttachmentGroup.Current.SelectSingleNode(string.Format("my:{0}", Constants.AttachmentRemoveNodeName), NS);
                    if (ChildRemoveNode.Value.Contains("TRUE"))
                    {
                        XPathNavigator AttachmentUrlNode = AttachmentGroup.Current.SelectSingleNode(string.Format("my:{0}", Constants.AttachmentUrlNodeName), NS);
                        if (!string.IsNullOrEmpty(AttachmentUrlNode.Value))
                        {
                            AttachmentsToRemove.Add(AttachmentUrlNode.Value);
                        }
                    }
                }
            }
            catch { }

            // Nothing to remove
            if (AttachmentsToRemove.Count == 0)
            {
                return;
            }

            // Begin removing from library
            if (!string.IsNullOrEmpty(WebUrl))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite TrainingSite = new SPSite(WebUrl))
                    {
                        using (SPWeb TrainingWeb = TrainingSite.OpenWeb())
                        {
                            TrainingSite.AllowUnsafeUpdates = true;
                            TrainingWeb.AllowUnsafeUpdates = true;

                            try
                            {
                                StringBuilder BatchDeleteCmd = new StringBuilder();
                                BatchDeleteCmd.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                                BatchDeleteCmd.Append("<Batch OnError=\"Return\">");

                                SPList AttachmentList = default(SPList);
                                try
                                {
                                    AttachmentList = TrainingWeb.GetList(WebUrl + Constants.AttachmentLibURL);
                                }
                                catch { }

                                if (AttachmentList != null)
                                {
                                    int IdCounter = 1;
                                    foreach (string AttachmentURL in AttachmentsToRemove)
                                    {

                                        BatchDeleteCmd.Append(string.Format("<Method ID=\"{0}\">", IdCounter.ToString()));
                                        BatchDeleteCmd.Append(string.Format("<SetList Scope=\"Request\">{0}</SetList>", AttachmentList.ID.ToString()));

                                        SPListItem Attachment = TrainingWeb.GetListItem(AttachmentURL);
                                        BatchDeleteCmd.Append(string.Format("<SetVar Name=\"ID\">{0}</SetVar>", Attachment.ID.ToString()));
                                        BatchDeleteCmd.Append(string.Format("<SetVar Name=\"owsfileref\">{0}</SetVar>", Attachment.File.ServerRelativeUrl.ToString()));
                                        BatchDeleteCmd.Append("<SetVar Name=\"Cmd\">Delete</SetVar>");
                                        BatchDeleteCmd.Append("</Method>");

                                        IdCounter++;
                                    }
                                    BatchDeleteCmd.Append("</Batch>");

                                    TrainingWeb.ProcessBatchData(BatchDeleteCmd.ToString());
                                }
                            }
                            catch (Exception err)
                            {
                                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "DeleteAttachment", err.Message.ToString(), err.StackTrace.ToString()), Form);
                                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "DeleteAttachment", err.Message.ToString(), err.StackTrace.ToString());
                                LoggingHelper.LogExceptionToSP(CustomMsg, err);
                            }

                            TrainingWeb.AllowUnsafeUpdates = false;
                            TrainingSite.AllowUnsafeUpdates = false;
                        }// dispose spweb
                    }// dispose spsite
                });
            }

        }// end DeleteAttachment


    }// end class
}
