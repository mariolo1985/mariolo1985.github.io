using Microsoft.Office.InfoPath;
using System;
using System.Xml;
using System.Xml.XPath;

using TRCore.HelperClass;
using Microsoft.SharePoint;

using System.Collections.Generic;
using System.Collections;


namespace TrainingRequestXSN
{
    public partial class FormCore
    {
        public void InternalStartup()
        {
            EventManager.FormEvents.Loading += new LoadingEventHandler(FormEvents_Loading);
            ((ButtonEvent)EventManager.ControlEvents["btn_SubmitRequest"]).Clicked += new ClickedEventHandler(btn_SubmitRequest_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_UploadMaterial"]).Clicked += new ClickedEventHandler(btn_UploadMaterial_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_CoreInfoView"]).Clicked += new ClickedEventHandler(btn_CoreInfoView_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_RelatedDocumentView"]).Clicked += new ClickedEventHandler(btn_RelatedDocumentView_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_RelatedNotesView"]).Clicked += new ClickedEventHandler(btn_RelatedNotesView_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_MaterialsView"]).Clicked += new ClickedEventHandler(btn_MaterialsView_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_ReturnToPrevious"]).Clicked += new ClickedEventHandler(btn_ReturnToPrevious_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_AddRDPath"]).Clicked += new ClickedEventHandler(btn_AddRDPath_Clicked);

            ((ButtonEvent)EventManager.ControlEvents["btn_SaveNotes"]).Clicked += new ClickedEventHandler(btn_SaveNotes_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_Cancel"]).Clicked += new ClickedEventHandler(btn_Cancel_Clicked);

            ((ButtonEvent)EventManager.ControlEvents["btn_DeleteItem"]).Clicked += new ClickedEventHandler(btn_DeleteItem_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_InitiateRemoveMaterials"]).Clicked += new ClickedEventHandler(btn_InitiateRemoveMaterials_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_InitiateRemoveRD"]).Clicked += new ClickedEventHandler(btn_InitiateRemoveRD_Clicked);
        }

        public void FormEvents_Loading(object sender, LoadingEventArgs e)
        {
            XPathNavigator root = MainDataSource.CreateNavigator();// form source

            // Set form variables
            FormVariable.SetFormVariable();

            // Check if this is the first time the form is loaded
            if (e.InputParameters.ContainsKey("SaveLocation"))
            {
                // Get a handle of SaveLocation as a fall back
                XPathNavigator SaveLocationNode = root.SelectSingleNode("/my:myFields/my:SystemInternal/my:SaveLocation", NamespaceManager);
                SaveLocationNode.SetValue(e.InputParameters["SaveLocation"].ToString());

                // Give form a unique GUID
                Guid NewGUID = Guid.NewGuid();

                XPathNavigator FormGUIDNode = root.SelectSingleNode("/my:myFields/my:SystemInternal/my:FormGUID", NamespaceManager);
                if (string.IsNullOrEmpty(FormGUIDNode.Value))
                {
                    FormGUIDNode.SetValue(NewGUID.ToString());
                }
            }

            FormEventHandler.FormLoadingEvent(this, e);

        }// End FORMLOADING

        /// <summary>
        /// Initial submit request
        /// </summary>
        public void btn_SubmitRequest_Clicked(object sender, ClickedEventArgs e)
        {
            // check if all required fields are filled out
            UserMessage.ClearUserMessage(this);
            Boolean ValidReqFields = FormHelpers.ValidateRequiredFields(this);

            if (ValidReqFields)
            {
                // Run additional btn events
                FormEventHandler.btn_SubmitRequest(this);

                // If required field check passes - Submit to library
                Boolean Submitted = SubmitHelper.ExecuteSubmit(this);
                if (Submitted)
                {
                    XPathNavigator root = MainDataSource.CreateNavigator();
                    root.SelectSingleNode("/my:myFields/my:SystemInternal/my:HideReturnBtn", NamespaceManager).SetValue("TRUE");
                }
            }
            else
            {
                // Set previous view to return to
                FormHelpers.SetPreviousViewName(this, Constants.DraftView);

            }

            ViewInfos.SwitchView(Constants.UserMessageView);
        }// end SubmitRequest

        /// <summary>
        /// This will upload a document to the Materials document library
        /// </summary>
        public void btn_UploadMaterial_Clicked(object sender, ClickedEventArgs e)
        {
            FormEventHandler.btn_UploadMaterial(this);
        }// end btn_uploadmaterial


        public void btn_CoreInfoView_Clicked(object sender, ClickedEventArgs e)
        {
            // Direct user to Core Information view
            ViewInfos.SwitchView(Constants.EditView);

        }// end btn_CoreInfoView

        public void btn_RelatedDocumentView_Clicked(object sender, ClickedEventArgs e)
        {
            // Direct user to Related Documents view
            ViewInfos.SwitchView(Constants.RelatedDocumentsView);
        }// end btn_RelatedDocumentView



        public void btn_RelatedNotesView_Clicked(object sender, ClickedEventArgs e)
        {
            // Direct user to Related Notes view
            ViewInfos.SwitchView(Constants.RelatedNotesView);
        }// end btn_RelatedNotesView



        public void btn_MaterialsView_Clicked(object sender, ClickedEventArgs e)
        {
            // Direct user to Related Materials view
            ViewInfos.SwitchView(Constants.MaterialsView);
        }// end btn_MaterialsView

        public void btn_ReturnToPrevious_Clicked(object sender, ClickedEventArgs e)
        {
            // Gets the value of previous view to direct user back
            // This button is only available on "UserMessage" view
            XPathNavigator root = MainDataSource.CreateNavigator(), PreviousViewNode = default(XPathNavigator);

            try
            {
                PreviousViewNode = root.SelectSingleNode(Constants.PreviousViewXPath, NamespaceManager);
                if (!string.IsNullOrEmpty(PreviousViewNode.Value))
                {
                    string PreviousView = PreviousViewNode.Value;
                    ViewInfos.SwitchView(PreviousView);
                }

            }
            catch { }

        }// end btn_ReturnToPrevious

        public void btn_AddRDPath_Clicked(object sender, ClickedEventArgs e)
        {
            try
            {
                RelatedDocumentHelper.SetRelatedDocumentPath(this);

                // Commit the path
                SubmitHelper.ExecuteSubmit(this);
            }
            catch { }

        }// end btn_AddRDPath

        public void btn_SaveNotes_Clicked(object sender, ClickedEventArgs e)
        {
            // Saves notes (if supplied) and save form
            try
            {
                RelatedNotesHelper.SetNotesHistory(this);
                SubmitHelper.ExecuteSubmit(this);
            }
            catch { }
        }// end btn_SaveNotes

        /// <summary>
        /// Cancel button from the delete item view
        /// </summary>
        public void btn_Cancel_Clicked(object sender, ClickedEventArgs e)
        {
            btn_ReturnToPrevious_Clicked(sender, e);// Returns to previous view w/o changes
        }// end btn_Cancel_clicked


        /// <summary>
        /// Deletes the selected materials
        /// </summary>
        public void btn_DeleteItem_Clicked(object sender, ClickedEventArgs e)
        {
            XPathNavigator root = MainDataSource.CreateNavigator(), RemoveNodeType = root.SelectSingleNode(Constants.RemoveTypeXPath, NamespaceManager);

            FormEventHandler.btn_DeleteItem(this, RemoveNodeType.Value);
            if (RemoveNodeType.Value == "RelatedDocument")
            {
                // Commit removing the path
                SubmitHelper.ExecuteSubmit(this);
            }

        }// end btn_deleteitem

        public void btn_InitiateRemoveMaterials_Clicked(object sender, ClickedEventArgs e)
        {
            FormEventHandler.btn_InitiateRemoveMaterials(this);

        }// end btn_initiateRemoveMaterials

        public void btn_InitiateRemoveRD_Clicked(object sender, ClickedEventArgs e)
        {
            FormEventHandler.btn_InitiateRemoveRD(this);
        }// end btn_InitiateRemoveRD
    }
}
