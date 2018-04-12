using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint;
using System.Web.UI.WebControls;
using TrainingRequest.Common.Helpers;

namespace TrainingRequest.Forms.Helpers
{
    public static class AttachmentHelper
    {
        public enum uploadTo
        {
            NewRequest = 1,
            Request
        }

        public static void UploadAttachment(FileUpload uploader, uploadTo to, string foldername)
        {
            if (uploader.HasFile)
            {
                try
                {
                    byte[] fileByte = uploader.FileBytes;
                    string filename = uploader.FileName;
                    string webUrl = SPContext.Current.Web.Url;

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite trainingSite = new SPSite(webUrl))
                        using (SPWeb trainingWeb = trainingSite.OpenWeb())
                        {
                            trainingSite.AllowUnsafeUpdates = true;
                            trainingWeb.AllowUnsafeUpdates = true;


                            try
                            {
                                string libUrl = string.Empty;
                                switch (to)
                                {
                                    case uploadTo.NewRequest:
                                        libUrl = Constants.url_NewRequestAttachment;
                                        break;

                                    case uploadTo.Request:
                                        libUrl = Constants.url_RequestAttachment;
                                        break;

                                }

                                if (!string.IsNullOrEmpty(libUrl))
                                {
                                    SPList attachmentList = trainingWeb.GetList(libUrl);// to get attachment folder

                                    string folderUrl = string.Format("{0}/{1}", libUrl, foldername);
                                    SPFolder folder = default(SPFolder);
                                    folder = trainingWeb.GetFolder(folderUrl);

                                    if (!folder.Exists)
                                    {
                                        // create folder
                                        SPListItem folderItem = attachmentList.Items.Add(libUrl, SPFileSystemObjectType.Folder, foldername);
                                        folderItem[Constants.column_Title] = foldername;
                                        folderItem.Update();

                                        folder = trainingWeb.GetFolder(folderUrl);
                                    }

                                    if (folder.Exists)
                                    {
                                        // folder should exist/created at this point
                                        SPFile file = folder.Files.Add(filename, fileByte, true);
                                        file.Update();
                                    }
                                }
                            }
                            catch (Exception err)
                            {
                                LogHelper.LogErrorMessage("AttachmentHelper.UploadAttachment.uploading", err.Message, err);
                            }

                            trainingSite.AllowUnsafeUpdates = false;
                            trainingWeb.AllowUnsafeUpdates = false;
                        }// dispose sp obj
                    });

                }
                catch (Exception err)
                {
                    LogHelper.LogErrorMessage("AttachmentHelper.UploadAttachment", err.Message, err);
                }

            }

        }// end uploadattachment


        public static string GetAttachmentMarkup(uploadTo to, string foldername)
        {
            string Markup = string.Empty;
            string tableTag = "<center><table class='tblNewRequestAttachment'><thead><tr><th>Attachment</th><th>Version</th></tr></thead>{0}</table></center>";
            string rowTag = "<tr><td>{0}</td><td>{1}</td></tr>";
            string aTag = "<a href='{0}' Target='_blank'>{1}</a>";

            string webUrl = SPContext.Current.Web.Url;
            StringBuilder rows = new StringBuilder();

            try
            {
                // get attachment folder
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite trainingSite = new SPSite(webUrl))
                    using (SPWeb trainingWeb = trainingSite.OpenWeb())
                    {
                        string libUrl = string.Empty;
                        switch (to)
                        {
                            case uploadTo.NewRequest:
                                libUrl = Constants.url_NewRequestAttachment;
                                break;

                            case uploadTo.Request:
                                libUrl = Constants.url_RequestAttachment;
                                break;
                        }

                        string folderUrl = string.Format("{0}/{1}", libUrl, foldername);
                        SPFolder attachmentFolder = trainingWeb.GetFolder(folderUrl);
                        if (attachmentFolder.Exists)
                        {
                            if (attachmentFolder.Files.Count > 0)
                            {
                                string filename = string.Empty, fileUrl = string.Empty, versionNum = string.Empty, anchor = string.Empty;

                                SPFileCollection files = attachmentFolder.Files;                                
                                foreach (SPFile file in files)
                                {
                                    filename = file.Name;
                                    fileUrl = string.Format("{0}/{1}", trainingWeb.Url, file.Url);
                                    anchor = string.Format(aTag, fileUrl, filename);

                                    versionNum = file.UIVersionLabel;

                                    // build markup
                                    rows.Append(string.Format(rowTag, anchor, versionNum));
                                }

                                Markup = string.Format(tableTag, rows.ToString());
                            }

                        }

                    }// dispose sp obj

                });
            }
            catch (Exception err)
            {
                LogHelper.LogErrorMessage("AttachmentHelper.GetAttachmentMarkup", err.Message, err);
            }

            return Markup;
        }// end loadattachemnts
    }// end attachmenthelper
}
