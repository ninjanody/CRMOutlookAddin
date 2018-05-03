namespace CrmOutlookAddin.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// An interface representing a session with a CRM server.
    /// </summary>
    public interface ISession
    {
        string Transmit(string v);
    }
}
