using System;

namespace CrmOutlookAddin.Exceptions
{
    public class BadStateTransition : Exception
    {
        public BadStateTransition(string message) : base(message)
        {
        }
    }
}
