/// <summary>
/// Objects which wrap Outlook COM objects.
/// </summary>
/// <remarks>
/// Outlook COM objects are horrible to deal with for a lot of reasons. They're 
/// not true objects, so inheritance and other useful things don't work, and every 
/// time you touch them they fire off a flurry of events, inevitably in other threads. 
/// Consequently we will wrap every COM object we touch in a wrapper object of our own. 
/// Only the wrapper object will interact with the COM object directly; everything else 
/// will interact only with the wrapper.
/// </remarks>
namespace CrmOutlookAddin.Wrappers
{
}
