using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Oasis.Common;

namespace Partner.MiscWebParts.Features.MiscWebParts
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("6add1b1b-ef43-47e1-9a2c-44b78e9af831")]
    public class MiscWebPartsEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                CreateAnnouncementSchema(properties);
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.Features.MiscWebParts.FeatureActivated Error", ex, 1234);
            }
        }

        internal static void CreateAnnouncementSchema(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPSite site = properties.Feature.Parent as SPSite;

                    // create site columns...
                    try
                    {
                        site.RootWeb.CreateChoiceColumn(
                            "Announcement Thumbnail",
                            new ArrayList 
                            { 
                                "Alert", 
                                "Article", 
                                "Award", 
                                "Chat", 
                                "Calendar", 
                                "Chart", 
                                "Conversation", 
                                "Donate", 
                                "Eco", 
                                "Economy", 
                                "Gift", 
                                "Globe", 
                                "Legal", 
                                "Lightbulb", 
                                "Medical", 
                                "Music", 
                                "Pulse", 
                                "Quote", 
                                "RadioScience", 
                                "Slideshow", 
                                "Sound", 
                                "Time", 
                                "TV-Old", 
                                "Voice", 
                                "Youtube"
                            },
                            true,
                            "Partner Site Columns");
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError("Partner.MiscWebParts.Features.MiscWebParts.CreateAnnouncementSchema Error - Unable to create site column(s).", ex, 1234);
                    }

                    // create content types...
                    try
                    {
                        site.RootWeb.CreateContentType(
                            "Announcement",
                            "Partner Announcement",
                            new ArrayList { "Announcement Thumbnail" },
                            "Partner Content Types");
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError("Partner.MiscWebParts.Features.MiscWebParts.CreateAnnouncementSchema Error - Unable to create content type(s).", ex, 1234);
                    }


                    // create list if not exists...
                    try
                    {
                        if (!site.RootWeb.ListExists("Announcements"))
                        {
                            site.RootWeb.CreateList(
                                "Announcements",
                                String.Empty,
                                SPListTemplateType.Announcements,
                                new ArrayList { "Announcement Thumbnail" },
                                false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError("Partner.MiscWebParts.Features.MiscWebParts.CreateAnnouncementSchema Error - Unable to create announcements list(s).", ex, 1234);
                    }

                    // configure content types...
                    try
                    {
                        SPList list = site.RootWeb.Lists["Announcements"];
                        list.ContentTypesEnabled = true;
                        list.Update();

                        // add new content types...
                        if (!list.ContentTypeExists("Partner Announcement"))
                        {
                            SPContentType contentType = site.RootWeb.ContentTypes["Partner Announcement"];
                            list.ContentTypes.Add(contentType);
                        }

                        // remove default content type...
                        if (list.ContentTypeExists("Announcement"))
                        {
                            foreach (SPListItem item in list.Items)
                            {
                                if (item.ContentType.Name == "Announcement")
                                {
                                    item["Content Type"] = "Partner Announcement";
                                    item.Update();
                                }
                            }
                            list.RemoveContentType("Announcement");
                        }

                        list.Update();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError("Partner.MiscWebParts.Features.MiscWebParts.CreateAnnouncementSchema Error - Unable to configure content types(s).", ex, 1234);
                    }


                });
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.Features.MiscWebParts.CreateSchema Unkown Error", ex, 1234);
            }
        }

    }
}
