/// <summary>
/// Transducers are objects whose purpose is to convert (transduce) a thing from one 
/// representation to another. 
/// </summary>
/// <remarks>
/// <para>
/// We deal with five sorts of 'thing' from this point of view, 
/// namely Calls, Contacts, Emails, Meetings and Tasks. However, while Calls and Meetings 
/// are different sorts of thing at the CRM side, they're instantiated as only one sort of 
/// thing, Appointments, on the Outlook side.</para>

/// <para>Therefore for each of Contacts, Emails and Tasks we need two transducers - an 
/// Outlook-to-CRM transducer and a CRM-to-Outlook transducer.</para>

/// <para>For Calls and Meetings we have to determine which Appointments should be synchronised 
/// as Calls and which as Meetings.We could do that in a single Outlook-to-CRM transducer 
/// for Appointments, but it seems to me that it makes more sense to have separate wrapper 
/// classes for Calls and Meetings (each class wrapping an outlook AppointmentItem) and 
/// also to have two transducers for each of Calls and Meetings as well, making ten 
/// transducers in total(plus probably two abstract superclasses).</para>
/// 
/// <para>Transducers are intended to be lightweight; you create one when you need one
/// and throw it away afterwards, rather than hanging onto and reusing it.</para>
/// </remarks>
namespace CrmOutlookAddin.Transducers
{
}
