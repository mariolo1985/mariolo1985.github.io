using System;
using System.Collections.Generic;
using System.Text;


/********************************************************************************************************************************
 * 
 *                  - This class is used to hold values required to build a repeating section in the form
 *                  
 ********************************************************************************************************************************/



namespace TRCore.HelperClass
{
    public class RepeaterNodeHandler
    {
        // **************************************************************** CLASS VARIABLES ****************************************************************
        private string _ChildNodeName;
        private string _ChildNodeVal;

        public string NodeName
        {

            get { return _ChildNodeName; }
            set { _ChildNodeName = value; }
        }

        public string NodeVal
        {
            get { return _ChildNodeVal; }
            set { _ChildNodeVal = value; }
        }

        // **************************************************************** CLASS METHODS ****************************************************************

        public RepeaterNodeHandler(string ChildNodeName, string ChildNodeVal)
        {
            if ((!string.IsNullOrEmpty(ChildNodeName)) && (!string.IsNullOrEmpty(ChildNodeVal)))
            {
                NodeName = ChildNodeName;
                NodeVal = ChildNodeVal;
            }
        }// end RepeaterNodeHandler constructor

       
    }
}
