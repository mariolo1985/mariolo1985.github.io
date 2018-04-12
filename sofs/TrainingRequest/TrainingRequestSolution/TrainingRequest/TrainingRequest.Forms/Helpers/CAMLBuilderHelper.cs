using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint;

namespace TrainingRequest.Forms.Helpers
{
    class CAMLBuilderHelper
    {
        /// <summary>
        /// Contains query
        /// </summary>
        public static string GetContainsCAMLStr(SPField Field, string queryVal)
        {
            string queryStr = string.Empty;

            queryStr = String.Format("<Where><Contains><FieldRef Name='{0}'/><Value Type='{1}'>{2}</Value></Contains></Where>", Field.InternalName.ToString(), Field.Type.ToString(), queryVal);

            return queryStr;
        }// end fnGetCAMLStr

        /// <summary>
        /// Equal-to query
        /// </summary>
        public static string GetEqCAMLStr(SPField Field, string queryVal)
        {
            string queryStr = string.Empty;

            queryStr = String.Format("<Where><Eq><FieldRef Name='{0}'/><Value Type='{1}'>{2}</Value></Eq></Where>", Field.InternalName.ToString(), Field.Type.ToString(), queryVal);

            return queryStr;
        }// end fnGetCAMStrL

        /// <summary>
        /// This is to query only content type of "Folder" and a matching "fieldname" value
        /// </summary>
        /// <param name="fieldName">Column to query against</param>
        /// <param name="fieldType">Column type</param>
        /// <param name="queryVal">Search Value</param>
        /// <returns>A query string</returns>
        public static string GetEqFolderQueryStr(SPField Field, string queryVal)
        {
            string queryStr = string.Empty;

            queryStr = string.Format("<Where><And><Eq><FieldRef Name='ContentType'/><Value Type='Text'>Folder</Value></Eq><Eq><FieldRef Name='{0}'/><Value Type='{1}'>{2}</Value></Eq></And></Where>",
                 Field.InternalName.ToString(), Field.Type.ToString(), queryVal);

            return queryStr;
        }// end GetEqFolderQuery

    }//end class
}
