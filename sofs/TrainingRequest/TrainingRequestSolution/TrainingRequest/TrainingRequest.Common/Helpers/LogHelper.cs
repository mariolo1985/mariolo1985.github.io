using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint.Administration;

namespace TrainingRequest.Common.Helpers
{
    public static class LogHelper
    {

        public static void LogErrorMessage(String methodName, String message, Exception ex)
        {
            try
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory(Constants.AppName, TraceSeverity.High, EventSeverity.Error), TraceSeverity.High, methodName + ": " + message + " - " + ex.Message, ex.StackTrace);
            }
            catch { }
        }

    }// end class
}
