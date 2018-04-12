using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPLoggingWriter;

namespace TrainingRequestWFProj.TrainingMaterialsWF.HelperClass
{
    public partial class LogWriting
    {

        public static void LogExceptionToSP(string Message, Exception Err)
        {
            SPLogWriter.LogErrorMessage("TrainingRequestWF", Message, Err);
        }// end LogExceptionToSP

    }// end class
}
