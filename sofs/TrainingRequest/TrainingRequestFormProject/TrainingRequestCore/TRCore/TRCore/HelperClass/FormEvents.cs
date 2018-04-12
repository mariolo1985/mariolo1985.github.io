using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.InfoPath;
using Microsoft.SharePoint;
using System.Xml;
using System.Xml.XPath;
using System.Collections;

/************************************************************************
 * 
 *          THIS WILL HANDLE FORM EVENTS
 *          
 * **********************************************************************/

namespace TRCore.HelperClass
{
    public static class FormEventHandler
    {

        /// <summary>
        /// This will handle loading events
        /// </summary>
        /// <param name="Form"></param>
        public static void FormLoadingEvent(XmlFormHostItem Form, LoadingEventArgs e)
        {
            
            // Use lifecycle status to determine additional loading events
            XmlNamespaceManager NS = Form.NamespaceManager;
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), LifecycleStatusNode = root.SelectSingleNode(Constants.LifecycleXPath, NS),
                FormNameNode = root.SelectSingleNode(Constants.FormNameXPath, NS),
                FormWFStageNumNode = root.SelectSingleNode(Constants.FormWFStageNumXPath, NS);

            if (!string.IsNullOrEmpty(LifecycleStatusNode.Value))
            {
                string LifecycleStatus = LifecycleStatusNode.Value, FormName = FormNameNode.Value, FormWFStageNum = FormWFStageNumNode.Value,
                    WebUrl = SPContext.Current.Web.Url.ToString();

                // Refresh attachment section                        
                AttachmentHelper.ClearAttachmentSection(Form);
                AttachmentHelper.LoadAttachments(Form, FormName, WebUrl);

                switch (LifecycleStatus)
                {
                    case "New":
                        // Set lookups
                        LookupSourceHelper.StartBuildingLookupSource(Form, WebUrl);
                        FormHelpers.sbClearSource(Constants.RDGroupXPath, Form, true);

                        e.SetDefaultView(Constants.DraftView);
                        break;

                    case "Assigning Task Owner":
                        e.SetDefaultView(Constants.ReadyOnlyView);
                        break;

                    default:
                        Boolean IsTaskOwner = false;

                        // Check if user is the current task owner or admin
                        if ((SecurityHelper.IsTaskOwner(Form, SPContext.Current.Web.CurrentUser.LoginName)) || (SecurityHelper.IsAdminGroup(Form, SPContext.Current.Web.CurrentUser.LoginName, WebUrl)))
                        {
                            IsTaskOwner = true;
                        }

                        // Determine the view to display on taskowner and stage order #
                        if ((FormWFStageNum != "999") && (IsTaskOwner))
                        {
                            e.SetDefaultView(Constants.EditView);
                        }
                        else
                        {
                            e.SetDefaultView(Constants.ReadyOnlyView);
                        }
                        break;

                }

            }
        }// end FormLoadingEvent

        /// <summary>
        /// Additional events during the request submit event
        /// </summary>
        /// <param name="Form"></param>
        public static void btn_SubmitRequest(XmlFormHostItem Form)
        {
            string WebUrl = SPContext.Current.Web.Url;
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), DeptNode = default(XPathNavigator), AdminGroupNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;

            DeptNode = root.SelectSingleNode(Constants.DeptXPath, NS);
            AdminGroupNode = root.SelectSingleNode(Constants.AdminGroupXPath, NS);

            // Set dept admin
            if (!string.IsNullOrEmpty(DeptNode.Value))
            {
                string AdminPrincipalStr = SecurityHelper.GetAdminGroup(WebUrl, DeptNode.Value);
                AdminGroupNode.SetValue(AdminPrincipalStr);
            }



        }// end btn_submitRequest


        /// <summary>
        /// This handles the upload "Material" button
        /// </summary>
        /// <param name="Form"></param>
        public static void btn_UploadMaterial(XmlFormHostItem Form)
        {
            XmlNamespaceManager NS = Form.NamespaceManager;
            XPathNavigator root = Form.MainDataSource.CreateNavigator(),
                AttachmentObj = root.SelectSingleNode("/my:myFields/my:Core/my:TrainingAttachments/my:TrainingAttachmentObj", NS);
            string WebURL = SPContext.Current.Web.Url.ToString();
            try
            {

                if (!string.IsNullOrEmpty(AttachmentObj.Value))
                {
                    // Get form name to group in Materials library
                    XPathNavigator FormNameNode = root.SelectSingleNode("/my:myFields/my:SystemInternal/my:FormName", NS);
                    string FormName = FormNameNode.Value;


                    // Proceed if a file is attached
                    string AttachmentString = AttachmentObj.Value;
                    Boolean AttachmentUploaded = AttachmentHelper.UploadAttachment(FormName, AttachmentString, WebURL, Form);


                    if (AttachmentUploaded)
                    {
                        // clear all material references
                        AttachmentHelper.ClearAttachmentSection(Form);

                        // reload all material references
                        AttachmentHelper.LoadAttachments(Form, FormName, WebURL);

                        // clear attachmentobj
                        AttachmentObj.SetValue(string.Empty);
                    }
                }

            }
            catch (Exception err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]",
                //  "btn_UploadMaterial", err.Message.ToString(), err.StackTrace.ToString()), Form);

                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "btn_UploadMaterial", err.Message.ToString(), err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, err);

            }

        }// end btn_UploadMaterial


        /// <summary>
        /// Called when the [Remove Selection] button is clicked on Materials view
        /// </summary>
        /// <param name="Form"></param>
        public static void btn_InitiateRemoveMaterials(XmlFormHostItem Form)
        {
            // Clear message box
            UserMessage.ClearUserMessage(Form);

            XmlNamespaceManager NS = Form.NamespaceManager;
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), RemoveTypeNode = root.SelectSingleNode("/my:myFields/my:SystemInternal/my:RemoveType", NS);
            XPathNodeIterator AttachmentGroup = root.Select("/my:myFields/my:Core/my:TrainingAttachments/my:TrainingAttachmentSection/my:TrainingAttachmentGroup", NS);
            ArrayList AttachmentsToRemove = new ArrayList();

            // loop the group for a 'checked' remove
            while (AttachmentGroup.MoveNext())
            {
                XPathNavigator ChildRemoveNode = AttachmentGroup.Current.SelectSingleNode("my:TrainingAttachmentRemove", NS);
                if (ChildRemoveNode.Value.Contains("TRUE"))
                {
                    XPathNavigator AttachmentNameNode = AttachmentGroup.Current.SelectSingleNode("my:TrainingAttachmentName", NS);
                    if (!string.IsNullOrEmpty(AttachmentNameNode.Value))
                    {
                        AttachmentsToRemove.Add(AttachmentNameNode.Value);
                    }
                }
            }

            // Check if we returned any results
            if (AttachmentsToRemove.Count == 0)
            {
                // Prompt that nothing is selected
                UserMessage.SetUserMessage(Form, "No selection has been made.");
                FormHelpers.SetPreviousViewName(Form, "Materials");
                Form.ViewInfos.SwitchView("UserMessage");

            }
            else
            {
                // Displays attachment name(s) and prompt for confirmation
                UserMessage.SetUserMessage(Form, "Are you sure you want to permanently delete the following material(s)?");
                foreach (string AttachmentName in AttachmentsToRemove)
                {
                    UserMessage.SetUserMessage(Form, AttachmentName);
                }// end loop

                FormHelpers.SetPreviousViewName(Form, "Materials");
                Form.ViewInfos.SwitchView("DeleteItems");

                RemoveTypeNode.SetValue("Attachment");

            }

        }// end btn_InitiateRemoveMaterials


        /// <summary>
        /// This handles the [Remove] button on delete confirmation
        /// </summary>
        /// <param name="Form"></param>
        public static void btn_DeleteItem(XmlFormHostItem Form, string RemoveType)
        {
            XmlNamespaceManager NS = Form.NamespaceManager;

            XPathNavigator root = Form.MainDataSource.CreateNavigator(), FormNameNode = default(XPathNavigator);

            FormNameNode = root.SelectSingleNode("/my:myFields/my:SystemInternal/my:FormName", NS);


            switch (RemoveType)
            {
                case "Attachment":
                    try
                    {
                        string FormName = string.Empty, WebUrl = SPContext.Current.Web.Url.ToString();

                        AttachmentHelper.DeleteAttachment(WebUrl, Form);

                        FormName = FormNameNode.Value;
                        AttachmentHelper.ClearAttachmentSection(Form);
                        AttachmentHelper.LoadAttachments(Form, FormName, WebUrl);

                        // Notify delete successful
                        UserMessage.ClearUserMessage(Form);
                        UserMessage.SetUserMessage(Form, "Materials Permanently Deleted.");
                        Form.ViewInfos.SwitchView("UserMessage");
                    }
                    catch { }
                    break;

                case "RelatedDocument":
                    RelatedDocumentHelper.RemoveSelectedDocPath(Form);
                    Form.ViewInfos.SwitchView("RelatedDocuments");
                    break;


            }


        }// end btn_DeleteItem

        /// <summary>
        /// This handles prompting user to confirm removing related document network path
        /// </summary>
        public static void btn_InitiateRemoveRD(XmlFormHostItem Form)
        {
            XmlNamespaceManager NS = Form.NamespaceManager;
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), RemoveTypeNode = root.SelectSingleNode("/my:myFields/my:SystemInternal/my:RemoveType", NS);
            XPathNodeIterator RelatedDocumentGroup = root.Select("/my:myFields/my:Core/my:RelatedDocumentSection/my:RelatedDocumentGroup", NS);
            ArrayList DocPathList = new ArrayList();

            if (RelatedDocumentGroup.Count > 0)
            {
                while (RelatedDocumentGroup.MoveNext())
                {
                    XPathNavigator RemoveNode = RelatedDocumentGroup.Current.SelectSingleNode("my:RelatedDocumentRemove", NS);
                    if (RemoveNode.Value.Contains("TRUE"))
                    {
                        XPathNavigator DocPathNode = RelatedDocumentGroup.Current.SelectSingleNode("my:RelatedDocumentPath", NS);
                        if (!string.IsNullOrEmpty(DocPathNode.Value))
                        {
                            string DocPath = DocPathNode.Value;
                            DocPathList.Add(DocPath);
                        }
                    }
                }// end while
            }

            // prompt user for confirmation
            FormHelpers.SetPreviousViewName(Form, "RelatedDocuments");
            if (DocPathList.Count == 0)
            {
                // Prompt that nothing is selected
                UserMessage.ClearUserMessage(Form);
                UserMessage.SetUserMessage(Form, "No selection has been made.");
                Form.ViewInfos.SwitchView("UserMessage");

            }
            else
            {
                UserMessage.ClearUserMessage(Form);
                UserMessage.SetUserMessage(Form, "Are you sure you want to permanently remove the following related document path(s)?");

                foreach (string Path in DocPathList)
                {
                    UserMessage.SetUserMessage(Form, Path);
                }

                Form.ViewInfos.SwitchView("DeleteItems");
                RemoveTypeNode.SetValue("RelatedDocument");

            }

        }// end btn_InitiateRemoveRD


    }// end class
}
