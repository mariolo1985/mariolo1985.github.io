using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Oasis.Common;
using Partner.Infrastructure;

namespace Partner.MiscWebParts.Layouts.Partner.MiscWebParts.Pages
{
    public partial class BulkPcaUpload : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                lblStatus.Text = String.Empty;
                List<String> upload = ReadFileUpload(fileUpload.PostedFile.InputStream);
                // ClearPcaAssignments();
                ProcessFileUpload(upload);
            }
        }

        internal List<String> ReadFileUpload(Stream input)
        {
            List<String> results = new List<String>();

            using (StreamReader userStream = new StreamReader(input))
            {
                while (!userStream.EndOfStream)
                {
                    String line = userStream.ReadLine();
                    if (line.Length > 0)
                    {
                        results.Add(line);
                    }
                }
            }

            return results;
        }

        internal void ProcessFileUpload(List<String> upload)
        {
            Int32 count = 0;
            Int32 successful = 0;
            Int32 errors = 0;

            // elevate privileges...
            using (SPSite site = new SPSite(SPContext.Current.Web.Url))
            {
                // clear existing pcas...
                List<String> uniquePartnerIds = new List<String>();

                foreach (String line in upload)
                {
                    String[] values = line.Split(',');
                    String partnerId = values[1];
                    if (!uniquePartnerIds.Contains(partnerId))
                    {
                        uniquePartnerIds.Add(partnerId);
                    }
                }

                foreach (String partnerId in uniquePartnerIds)
                {
                    ParnterProvisioningHelper.ClearExistingPCAs(site, partnerId);
                }

                // asign pcas...
                foreach (String line in upload)
                {
                    count++;
                    try
                    {
                        String[] values = line.Split(',');
                        String partnerTitle = values[0];
                        String partnerId = values[1];
                        String pca = values[2];

                        ParnterProvisioningHelper.AssignPCA(site, pca, partnerId);

                        successful++;
                    }
                    catch
                    {
                        errors++;
                        lblStatus.Text = lblStatus.Text + "ERROR Line " + count + ": " + line + "<br>";
                    }
                }
            }

            lblStatus.Text = lblStatus.Text + "<br>";
            lblStatus.Text = lblStatus.Text + successful + " PCAs assigned.<br>";
            lblStatus.Text = lblStatus.Text + errors + " errors encountered.<br>";
        }
    }
}
