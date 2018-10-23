/// <summary>
/// Objects which wrap Outlook COM objects.
/// </summary>
/// <remarks>
/// <para>
/// Outlook COM objects are horrible to deal with for a lot of reasons. They're 
/// not true objects, so inheritance and other useful things don't work, and every 
/// time you touch them they fire off a flurry of events, inevitably in other threads. 
/// Consequently we will wrap every COM object we touch in a wrapper object of our own. 
/// Only the wrapper object will interact with the COM object directly; everything else 
/// will interact only with the wrapper.
/// </para>
/// 
/// <para>Storing our own data on Outlook objects is a dubious practice.Although Outlook allows us to do this through UserProperties, and version 3 does so, there are a number of problems with this:</para>
/// <para>1. UserProperties are not thread safe;</para>
/// <para>2. When a UserProperty is changed, Outlook sends an ItemChange event which, in version 3's multi-threaded design, ends up being handled in a different thread. This can easily lead to a cascade of ItemChange events.</para>
/// <para>Consequently, it's better not to store our own state data on the outlook item directly. We need a wrapper class on a one-to-one basis with Outlook items, and, indeed, version 3 provides this in the form of SyncState classes. the 3.0.11-develop branch provides a single factory/repository object for these wrappers, which greatly speeds up access to them and prevents threading issues creating duplicate instances.</para>
/// <para>My understanding of the development of the code is that SyncState objects were originally intended only to hold the state/transition engine state, and that additional functionality accreted to them over time.But the hybrid strategy with some information held on user properties and some in the SyncState is a bad compromise.Storing information on UserProperties doesn't really work in a multi-threaded design.</para>
/// <para>Consequently I believe we need to upgrade our SyncState classes into first class Wrapper classes, which will be our only access to the underlying Outlook item, and cease using UserProperties altogether.</para>
/// </remarks>
namespace CrmOutlookAddin.Wrappers
{
}
