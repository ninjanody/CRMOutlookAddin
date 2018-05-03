using CrmOutlookAddin.Core;
using Microsoft.Office.Interop.Outlook;
using System;

namespace CrmOutlookAddin
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        internal void GetFolder(ItemType call)
        {
            throw new NotImplementedException();
        }

        internal ISession GetCRMSession()
        {
            throw new NotImplementedException();
        }

        internal NameSpace GetOutlookSession()
        {
            return Application.Session;
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion VSTO generated code
    }
}
