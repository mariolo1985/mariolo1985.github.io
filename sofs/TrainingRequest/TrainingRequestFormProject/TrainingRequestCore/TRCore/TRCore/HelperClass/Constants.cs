using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**********************************************************
 * 
 *      Constants for this solution
 * 
 * ********************************************************/

namespace TRCore.HelperClass
{
    public partial class Constants
    {
        // ***************************XPaths**********************

        // config/admin paths
        public static string TaskOwnerIdXPath = "/my:myFields/my:FormInternal/my:TaskOwnerId";
        public static string AdminGroupXPath = "/my:myFields/my:Admin/my:AdminGroup";
        public static string LifecycleXPath = "/my:myFields/my:FormInternal/my:LifecycleStatus";
        public static string FormNameXPath = "/my:myFields/my:SystemInternal/my:FormName";
        public static string FormWFStageNumXPath = "/my:myFields/my:SystemInternal/my:FormWFStageNum";

        public static string PreviousViewXPath = "/my:myFields/my:SystemInternal/my:PreviousView";
        public static string RemoveTypeXPath = "/my:myFields/my:SystemInternal/my:RemoveType";

        // Document info paths
        public static string RequestTitleXPath = "/my:myFields/my:Core/my:RequestTitle";
        public static string RequestTypeXPath = "/my:myFields/my:Core/my:RequestType";
        public static string DeptXPath = "/my:myFields/my:Core/my:Department";
        
        // Related Document paths
        public static string RDParentXPath = "/my:myFields/my:Core/my:RelatedDocumentSection";
        public static string RDGroupXPath = "/my:myFields/my:Core/my:RelatedDocumentSection/my:RelatedDocumentGroup";
        public static string AddRDPath = "/my:myFields/my:Core/my:RelatedDocumentSection/my:RDPath";
        public static string RDDisplayNodeName = "RelatedDocumentPath";
        public static string RDHyperlinkNodeName = "RelatedDocumentHyperlink";
        public static string RDRemoveNodeName = "RelatedDocumentRemove";

        // Notes paths
        public static string NotesXPath = "/my:myFields/my:Core/my:NotesSection/my:Notes";
        public static string NotesHistoryXPath = "/my:myFields/my:Core/my:NotesSection/my:NotesHistory";

        // Message paths
        public static string MessageXPath = "/my:myFields/my:SystemInternal/my:UserMessage";

        // Attachment paths
        public static string AttachmentParentXPath = "/my:myFields/my:Core/my:TrainingAttachments/my:TrainingAttachmentSection";
        public static string AttachmentGroupXPath = "/my:myFields/my:Core/my:TrainingAttachments/my:TrainingAttachmentSection/my:TrainingAttachmentGroup";
        public static string AttachmentGroupName = "TrainingAttachmentGroup";
        public static string AttachmentNodeName = "TrainingAttachmentName";
        public static string AttachmentUrlNodeName = "TrainingAttachmentURL";
        public static string AttachmentVersionNodeName = "TrainingAttachmentVersion";
        public static string AttachmentRemoveNodeName = "TrainingAttachmentRemove";



        // *****************************View Names************************
        public static string DraftView = "01_Draft";
        public static string EditView = "02_MaterialCreation";
        public static string ReadyOnlyView = "ReadOnly";
        public static string UserMessageView = "UserMessage";
        public static string RelatedDocumentsView = "RelatedDocuments";
        public static string RelatedNotesView = "RelatedNotes";
        public static string MaterialsView = "Materials";


        // ************************For Lookups*****************************
        // Request Type handler
        public static string RequestTypeListURL = "/lists/RequestTypes";
        public static string RequestTypeSourceParentXPath = "/my:myFields/my:FieldSouce/my:RequestTypeSection";
        public static string RequestTypeGroupNodeName = "RequestTypeGroup";
        public static string RequestTypeValueNodeName = "RequestTypeValue";
        public static string RequestTypeValueColumnName = "Title";


        // Department Type handler
        public static string DepartmentListURL = "/lists/Departments";
        public static string DepartmentTypeSourceParentXPath = "/my:myFields/my:FieldSouce/my:DepartmentSection";
        public static string DepartmentGroupNodeName = "DepartmentGroup";
        public static string DepartmentValueNodeName = "DepartmentValue";
        public static string DepartmentValueColumnName = "Title";
        public static string DepartmentAdminColumnName = "AdministratorGroup";

        // ***************************For Attachments*************************

        public static string AttachmentLibURL = "/Materials";

        // Attachment Library
        public static string ALFormNameFieldName = "FormName";

    }// end Constants
}
