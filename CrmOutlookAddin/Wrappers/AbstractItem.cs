using CrmOutlookAddin.Core;
using CrmOutlookAddin.Exceptions;
using System;

namespace CrmOutlookAddin.Wrappers
{
    /// <summary>
    /// Abstract superclass for Wrapper classes, which provides the state/transition engine.
    /// </summary>
    public abstract class AbstractItem
    {
        /// <summary>
        /// The name of the property of an Outlook item on which the id of the corresponding CRM 
        /// item will be stored.
        /// </summary>
        /// <remarks>
        /// This will be the only user property we will store on Outlook items.
        /// </remarks>
        public const string CrmIdPropertyName = "CRM_Id";

        /// <summary>
        /// A lock that should be obtained before operations which operate on the State or the
        /// cached value.
        /// </summary>
        private object stateLock = new object();

        /// <summary>
        /// The CRM id of the object I wrap, if known.
        /// </summary>
        public abstract string CrmEntryId { get; set; }

        /// <summary>
        /// A description of the object wrapped.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Get a string in the format `fieldname: fieldvalue;...` for each of the fields which
        /// distinguish the object I wrap, ordered by fieldname.
        /// </summary>
        public abstract string DistinctFields { get; }

        /// <summary>
        /// The time I was last modified.
        /// </summary>
        public DateTime LastModified { get; private set; } = DateTime.Now;

        /// <summary>
        /// The Outlook ID of the object I wrap.
        /// </summary>
        public abstract string OutlookId { get; }

        /// <summary>
        /// The state I am currently in.
        /// </summary>
        public States State { get; private set; } = States.New;

        public abstract void CacheItem();

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.Editing"/>.
        /// </summary>
        /// <exception cref="BadStateTransition">If this transition is not permitted.</exception>
        public void SetEditing()
        {
            lock (this.stateLock)
            {
                switch (this.State)
                {
                    /* you mostly can switch to editing */
                    case States.Transmitted:
                    case States.Invalid:
                        throw new BadStateTransition($"{this.State} => Editing");
                    default:
                        this.SetState(States.Editing);
                        break;
                }
            }
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.Invalid"/>.
        /// </summary>
        /// <remarks>
        /// Removes the invalid sync state from the caches, which will hopefully allow
        /// the system to stabilise itself.
        /// </remarks>
        public void SetInvalid()
        {
            this.State = States.Invalid;
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.NewFromCRM"/>.
        /// </summary>
        /// <exception cref="BadStateTransition">If this transition is not permitted.</exception>
        public void SetNewFromCRM()
        {
            lock (this.stateLock)
            {
                switch (this.State)
                {
                    case States.New:
                    case States.NewFromOutlook:
                        this.SetState(States.NewFromCRM);
                        break;

                    default:
                        throw new BadStateTransition($"{this.State} => NewFromCRM");
                }
            }
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.NewFromOutlook"/>.
        /// </summary>
        /// <exception cref="BadStateTransition">If this transition is not permitted.</exception>
        public void SetNewFromOutlook()
        {
            lock (this.stateLock)
            {
                switch (this.State)
                {
                    case States.New:
                        this.SetState(States.NewFromOutlook);
                        break;

                    default:
                        throw new BadStateTransition($"{this.State} => NewFromOutlook");
                }
            }
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.Pending"/>.
        /// </summary>
        /// <param name="iSwearThatTransmissionHasFailed">Allows override of state transition
        /// flow ONLY when transmission has failed.</param>
        /// <exception cref="BadStateTransition">If this transition is not permitted.</exception>
        public void SetPending(bool iSwearThatTransmissionHasFailed = false)
        {
            lock (this.stateLock)
            {
                if (iSwearThatTransmissionHasFailed && this.State == States.Transmitted)
                {
                    this.SetState(States.Pending);
                }
                switch (this.State)
                {
                    case States.NewFromOutlook:
                    case States.PresentAtStartup:
                    /* a new item may, and often will, be set to 'Pending'. */
                    case States.Editing:
                    /* an item being edited SHOULD end up pending */
                    case States.Pending:
                        /* If 'Pending', may remain 'Pending'. */
                        this.SetState(States.Pending);
                        break;

                    default:
                        throw new BadStateTransition($"{this.State} => Pending");
                }
            }
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.PendingDeletion"/>.
        /// </summary>
        public void SetPendingDeletion()
        {
            lock (this.stateLock)
            {
                switch (this.State)
                {
                    case States.NewFromOutlook:
                    /* if a CRM outlook is deleted while Outlook is offline,
                     * you'll get this sequence. */
                    case States.Synced:
                        this.SetState(States.PendingDeletion);
                        break;

                    default:
                        throw new BadStateTransition($"{this.State} => PendingDeletion");
                }
            }
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.PresentAtStartup"/>.
        /// </summary>
        /// <exception cref="BadStateTransition">If this transition is not permitted.</exception>
        public void SetPresentAtStartup()
        {
            lock (this.stateLock)
            {
                switch (this.State)
                {
                    case States.New:
                    case States.NewFromOutlook:
                        this.SetState(States.PresentAtStartup);
                        break;

                    default:
                        throw new BadStateTransition($"{this.State} => PresentAtStartup");
                }
            }
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.Queued"/>.
        /// </summary>
        /// <exception cref="BadStateTransition">If this transition is not permitted.</exception>
        public void SetQueued()
        {
            lock (this.stateLock)
            {
                switch (this.State)
                {
                    case States.Pending:
                        this.SetState(States.Queued);
                        break;

                    default:
                        throw new BadStateTransition($"{this.State} => Queued");
                }
            }
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.Synced"/>,
        /// and recache its Outlook item.
        /// </summary>
        /// <param name="iSwearReceivedFromCRM">When an item is received from CRM, it is created or updated in
        /// Outlook and is likely to be queued for retransmission back to CRM before SetSynced() happens.
        /// Under this situation ONLY, it's allowable to move from any to Synced.</param>
        /// <exception cref="BadStateTransition">If this transition is not permitted.</exception>
        public void SetSynced(bool iSwearReceivedFromCRM = false)
        {
            lock (this.stateLock)
            {
                if (iSwearReceivedFromCRM)
                {
                    this.State = States.Synced;
                }
                else
                {
                    switch (this.State)
                    {
                        case States.NewFromCRM:
                        /* if ol item is created from a CRM record, it will be 'NewFromCRM' then 'Synced' */
                        case States.PresentAtStartup:
                        /* when the add-in first starts up, new SyncStates will get synced. */
                        case States.Synced:
                        /* if ol item is unchanged but CRM record is changed, it will be 'Synced' then 'Synced' */
                        case States.Transmitted:
                        /* if ol item is transmitted to CRM, it will be 'Transmitted' then 'Synced' */
                        case States.PendingDeletion:
                            /* if a state has been marked as pending deletion and then is found on the next
                             * synchronisation run, it should be set back to synced */
                            this.CacheItem();
                            this.SetState(States.Synced);
                            this.LastModified = DateTime.UtcNow;
                            break;

                        default:
                            throw new BadStateTransition($"{this.State} => Synced");
                    }
                }
            }
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.Synced"/>
        /// and its CRM entry ID to this crmEntryId, and recache its Outlook item.
        /// </summary>
        /// <param name="crmEntryId">The id of the object in CRM.</param>
        /// <exception cref="BadStateTransition">If this transition is not permitted.</exception>
        public void SetSynced(string crmEntryId)
        {
            this.SetSynced();
            this.CrmEntryId = crmEntryId;
        }

        /// <summary>
        /// Set the transmission state of this SyncState object to <see cref="States.Transmitted"/>.
        /// </summary>
        /// <exception cref="BadStateTransition">If this transition is not permitted.</exception>
        public void SetTransmitted()
        {
            lock (this.stateLock)
            {
                switch (this.State)
                {
                    case States.Queued:
                        this.SetState(States.Transmitted);
                        break;

                    default:
                        throw new BadStateTransition($"{this.State} => Transmitted");
                }
            }
        }

        /// <summary>
        /// Set me into the specified state, logging the transition if in DEBUG.
        /// </summary>
        /// <param name="newState"></param>
        private void SetState(States newState)
        {
            this.State = newState;
        }
    }
}
