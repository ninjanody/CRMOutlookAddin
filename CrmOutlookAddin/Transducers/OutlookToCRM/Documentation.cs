/// <summary>
/// Transducers which convert from wrapped outlook items to their CRM (JSON) representations.
/// </summary>
/// <remarks>
/// <para>
/// For each of
/// </para>
/// <list type="ordered">
/// <item>Contact</item>
/// <item>Task</item>
/// </list>
/// <para>Only one `set_entry` string need be passed from Outlook to CRM; however, for each of</para>
/// <list type="ordered">
/// <item>Call</item>
/// <item>Email</item>
/// <item>Meeting</item>
/// </list>
/// <para>the initial `set_entry` call must be followed by one or more `set_relationship` calls. 
/// Thus the transducer must be able to return a variable number of strings.</para>
/// <para>Further, when an object is being sent to CRM the first time, its CRM id will not be known; 
/// consequently we must have a special marker in the second and subsequent strings, which will be 
/// substituted for by the value of the id returned by the initial `set_entry` call.</para>
/// </remarks>
namespace CrmOutlookAddin.Transducers.OutlookToCRM
{
}
