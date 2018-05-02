namespace CrmOutlookAddin.Wrappers.Tests
{
    using CrmOutlookAddin.Core;
    using CrmOutlookAddin.Exceptions;
    using NUnit.Framework;
    using System;

    /// <summary>
    /// Tests for the state transition engine.
    /// </summary>
    /// <remarks>
    /// This is in some sense the key file for the whole project: the policy set by this file 
    /// controls the flow of information between that addin and CRM.
    /// </remarks>
    [TestFixture()]
    public class StateTransitionEngineTests
    {
        [Test()]
        public void SetEditingTest()
        {
            TestItem instanceNew = new TestItem();
            /* you can go from New to Editing */
            Assert.DoesNotThrow(() => instanceNew.SetEditing(), "you can go from New to Editing");
            Assert.AreEqual(States.Editing, instanceNew.State, "after transition, instance is in state Editing");

            /* you can go from NewFromOutlook to Editing */
            TestItem instanceNewFromOutlook = new TestItem();
            instanceNewFromOutlook.SetNewFromOutlook();
            Assert.DoesNotThrow(() => instanceNewFromOutlook.SetEditing(), "you can go from NewFromOutlook to Editing");
            Assert.AreEqual(States.Editing, instanceNewFromOutlook.State, "after transition, instance is in state Editing");

            /* you can go from PresentAtStartup to Editing */
            TestItem instancePresentAtStartup = new TestItem();
            instancePresentAtStartup.SetPresentAtStartup();
            Assert.DoesNotThrow(() => instancePresentAtStartup.SetEditing(), "you can go from PresentAtStartup to Editing");
            Assert.AreEqual(States.Editing, instancePresentAtStartup.State, "after transition, instance is in state Editing");

            /* you can go from NewFromCRM to Editing */
            TestItem instanceNewFromCRM = new TestItem();
            instanceNewFromCRM.SetNewFromCRM();
            Assert.DoesNotThrow(() => instanceNewFromCRM.SetEditing(), "you can go from NewFromCRM to Editing");
            Assert.AreEqual(States.Editing, instanceNewFromCRM.State, "after transition, instance is in state Editing");

            /* you can go from Editing to Editing */
            var instanceEditing = new TestItem().StepTo(States.Editing);
            Assert.DoesNotThrow(() => instanceEditing.SetEditing(), "you can go from Editing to Editing");
            Assert.AreEqual(States.Editing, instanceEditing.State, "after transition, instance is in state Editing");

            /* you can go from Pending to Editing */
            var instancePending = new TestItem().StepTo(States.Pending);
            Assert.DoesNotThrow(() => instancePending.SetEditing(), "you can go from Pending to Editing");
            Assert.AreEqual(States.Editing, instancePending.State, "after transition, instance is in state Editing");

            /* you can go from Queued to Editing */
            var instanceQueued = new TestItem().StepTo(States.Queued);
            Assert.DoesNotThrow(() => instanceQueued.SetEditing(), "you can go from Queued to Editing");
            Assert.AreEqual(States.Editing, instanceQueued.State, "after transition, instance is in state Editing");

            /* you cannot go from Transmitted to Editing */
            var instanceTransmitted = new TestItem().StepTo(States.Transmitted);
            Assert.Throws<BadStateTransition>(() => instanceTransmitted.SetEditing(), "you cannot go from Transmitted to Editing");
            Assert.AreEqual(States.Transmitted, instanceTransmitted.State, "after transition, instance is in state Transmitted");

            /* you can go from Synced to Editing */
            var instanceSynced = new TestItem().StepTo(States.Synced);
            Assert.DoesNotThrow(() => instanceSynced.SetEditing(), "you can go from Synced to Editing");
            Assert.AreEqual(States.Editing, instanceSynced.State, "after transition, instance is in state Editing");

            /* you can go from PendingDeletion to Editing */
            var instancePendingDeletion = new TestItem().StepTo(States.PendingDeletion);
            Assert.DoesNotThrow(() => instancePendingDeletion.SetEditing(), "you can go from PendingDeletion to Editing");
            Assert.AreEqual(States.Editing, instancePendingDeletion.State, "after transition, instance is in state Editing");
        }

        [Test()]
        public void SetInvalidTest()
        {
            /* you can go from New to Invalid */
            var instanceNew = new TestItem();
            Assert.DoesNotThrow(() => instanceNew.SetInvalid(), "you can go from New to Invalid");
            Assert.AreEqual(States.Invalid, instanceNew.State, "after transition, instance is in state Invalid");

            /* you can go from NewFromOutlook to Invalid */
            var instanceNewFromOutlook = new TestItem().StepTo(States.NewFromOutlook);
            Assert.DoesNotThrow(() => instanceNewFromOutlook.SetInvalid(), "you can go from NewFromOutlook to Invalid");
            Assert.AreEqual(States.Invalid, instanceNewFromOutlook.State, "after transition, instance is in state Invalid");

            /* you can go from PresentAtStartup to Invalid */
            var instancePresentAtStartup = new TestItem().StepTo(States.PresentAtStartup);
            Assert.DoesNotThrow(() => instancePresentAtStartup.SetInvalid(), "you can go from PresentAtStartup to Invalid");
            Assert.AreEqual(States.Invalid, instancePresentAtStartup.State, "after transition, instance is in state Invalid");

            /* you can go from NewFromCRM to Invalid */
            var instanceNewFromCRM = new TestItem().StepTo(States.NewFromCRM);
            Assert.DoesNotThrow(() => instanceNewFromCRM.SetInvalid(), "you can go from NewFromCRM to Invalid");
            Assert.AreEqual(States.Invalid, instanceNewFromCRM.State, "after transition, instance is in state Invalid");

            /* you can go from Editing to Invalid */
            var instanceEditing = new TestItem().StepTo(States.Editing);
            Assert.DoesNotThrow(() => instanceEditing.SetInvalid(), "you can go from Editing to Invalid");
            Assert.AreEqual(States.Invalid, instanceEditing.State, "after transition, instance is in state Invalid");

            /* you can go from PresentAtStartup to Invalid */
            var instancePending = new TestItem().StepTo(States.Pending);
            Assert.DoesNotThrow(() => instancePending.SetInvalid(), "you can go from PresentAtStartup to Invalid");
            Assert.AreEqual(States.Invalid, instancePending.State, "after transition, instance is in state Invalid");

            /* you can go from Queued to Invalid */
            var instanceQueued = new TestItem().StepTo(States.Queued);
            Assert.DoesNotThrow(() => instanceQueued.SetInvalid(), "you can go from Queued to Invalid");
            Assert.AreEqual(States.Invalid, instanceQueued.State, "after transition, instance is in state Invalid");

            /* you can go from Transmitted to Invalid */
            var instanceTransmitted = new TestItem().StepTo(States.Transmitted);
            Assert.DoesNotThrow(() => instanceTransmitted.SetInvalid(), "you can go from Transmitted to Invalid");
            Assert.AreEqual(States.Invalid, instanceTransmitted.State, "after transition, instance is in state Invalid");

            /* you can go from Synced to Invalid */
            var instanceSynced = new TestItem().StepTo(States.Synced);
            Assert.DoesNotThrow(() => instanceSynced.SetInvalid(), "you can go from Synced to Invalid");
            Assert.AreEqual(States.Invalid, instanceSynced.State, "after transition, instance is in state Invalid");

            /* you can go from PendingDeletion to Invalid */
            var instancePendingDeletion = new TestItem().StepTo(States.PendingDeletion);
            Assert.DoesNotThrow(() => instancePendingDeletion.SetInvalid(), "you can go from PendingDeletion to Invalid");
            Assert.AreEqual(States.Invalid, instancePendingDeletion.State, "after transition, instance is in state Invalid");
        }

        [Test()]
        public void SetNewFromCRMTest()
        {
            /* you can go from New to NewFromCRM */
            TestItem instanceNew = new TestItem();
            Assert.DoesNotThrow(() => instanceNew.SetNewFromCRM(), "you can go from New to NewFromCRM");
            Assert.AreEqual(States.NewFromCRM, instanceNew.State, "after transition, instance is in state NewFromCRM");

            /* you cannot go from NewFromOutlook to NewFromCRM */
            var instanceNewFromOutlook = new TestItem().StepTo(States.NewFromOutlook);
            Assert.DoesNotThrow(() => instanceNewFromOutlook.SetNewFromCRM());
            Assert.AreEqual(States.NewFromCRM, instanceNewFromOutlook.State, "after transition, instance is in state NewFromCRM");

            /* you cannot go from PresentAtStartup to NewFromCRM */
            var instancePresentAtStartup = new TestItem().StepTo(States.PresentAtStartup);
            Assert.Throws<BadStateTransition>(() => instancePresentAtStartup.SetNewFromCRM(), "you cannot go from PresentAtStartup to NewFromCRM");
            Assert.AreEqual(States.PresentAtStartup, instancePresentAtStartup.State, "after transition, instance is in state PresentAtStartup");

            /* you cannot go from NewFromCRM to NewFromCRM */
            var instanceNewFromCRM = new TestItem().StepTo(States.NewFromCRM);
            Assert.Throws<BadStateTransition>(() => instanceNewFromCRM.SetNewFromCRM(), "you cannot go from NewFromCRM to NewFromCRM");
            Assert.AreEqual(States.NewFromCRM, instanceNewFromCRM.State, "after transition, instance is in state NewFromCRM");

            /* you cannot go from Editing to NewFromCRM */
            var instanceEditing = new TestItem().StepTo(States.Editing);
            Assert.Throws<BadStateTransition>(() => instanceEditing.SetNewFromCRM(), "you cannot go from Editing to NewFromCRM");
            Assert.AreEqual(States.Editing, instanceEditing.State, "after transition, instance is in state Editing");

            /* you cannot go from Pending to NewFromCRM */
            var instancePending = new TestItem().StepTo(States.Pending);
            Assert.Throws<BadStateTransition>(() => instancePending.SetNewFromCRM(), "you cannot go from Pending to NewFromCRM");
            Assert.AreEqual(States.Pending, instancePending.State, "after transition, instance is in state Pending");

            /* you cannot go from Queued to NewFromCRM */
            var instanceQueued = new TestItem().StepTo(States.Queued);
            Assert.Throws<BadStateTransition>(() => instanceQueued.SetNewFromCRM(), "you cannot go from Queued to NewFromCRM");
            Assert.AreEqual(States.Queued, instanceQueued.State, "after transition, instance is in state Queued");

            /* you cannot go from Transmitted to NewFromCRM */
            var instanceTransmitted = new TestItem().StepTo(States.Transmitted);
            Assert.Throws<BadStateTransition>(() => instanceTransmitted.SetNewFromCRM(), "you cannot go from Transmitted to NewFromCRM");
            Assert.AreEqual(States.Transmitted, instanceTransmitted.State, "after transition, instance is in state Transmitted");

            /* you cannot go from Synced to NewFromCRM */
            var instanceSynced = new TestItem().StepTo(States.Synced);
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetNewFromCRM(), "you cannot go from Synced to NewFromCRM");
            Assert.AreEqual(States.Synced, instanceSynced.State, "after transition, instance is in state Synced");

            /* you cannot go from PendingDeletion to NewFromCRM */
            var instancePendingDeletion = new TestItem().StepTo(States.PendingDeletion);
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetNewFromCRM(), "you cannot go from PendingDeletion to NewFromCRM");
            Assert.AreEqual(States.PendingDeletion, instancePendingDeletion.State, "after transition, instance is in state PendingDeletion");
        }

        [Test()]
        public void SetNewFromOutlookTest()
        {
            /* you can go from New to NewFromOutlook */
            Assert.DoesNotThrow(() => new TestItem().SetNewFromOutlook());

            /* you cannot go from NewFromOutlook to NewFromOutlook */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.NewFromOutlook).SetNewFromOutlook());

            /* you cannot go from PresentAtStartup to NewFromOutlook */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.PresentAtStartup).SetNewFromOutlook());

            /* you cannot go from NewFromCRM to NewFromOutlook */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.NewFromCRM).SetNewFromOutlook());

            /* you cannot go from Editing to NewFromOutlook */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Editing).SetNewFromOutlook());

            /* you cannot go from PresentAtStartup to NewFromOutlook */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Pending).SetNewFromOutlook());

            /* you cannot go from Queued to NewFromOutlook */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Queued).SetNewFromOutlook());

            /* you cannot go from Transmitted to NewFromOutlook */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Transmitted).SetNewFromOutlook());

            /* you cannot go from Synced to NewFromOutlook */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Synced).SetNewFromOutlook());

            /* you cannot go from PendingDeletion to NewFromOutlook */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.PendingDeletion).SetNewFromOutlook());
        }

        [Test()]
        public void SetPendingDeletionTest()
        {
            /* you cannot go from New to PendingDeletion */
            Assert.Throws<BadStateTransition>(() => new TestItem().SetPendingDeletion());

            /* you can go from NewFromOutlook to PendingDeletion */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.NewFromOutlook).SetPendingDeletion());

            /* you can go from PresentAtStartup to PendingDeletion */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.PresentAtStartup).SetPendingDeletion());

            /* you cannot go from NewFromCRM to PendingDeletion */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.NewFromCRM).SetPendingDeletion());

            /* you cannot go from Editing to PendingDeletion */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Editing).SetPendingDeletion());

            /* you cannot go from PresentAtStartup to PendingDeletion */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Pending).SetPendingDeletion());

            /* you cannot go from Queued to PendingDeletion */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Queued).SetPendingDeletion());

            /* you cannot go from Transmitted to PendingDeletion */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Transmitted).SetPendingDeletion());

            /* you can go from Synced to PendingDeletion */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.Synced).SetPendingDeletion());

            /* you cannot go from PendingDeletion to PendingDeletion */
            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.PendingDeletion).SetPendingDeletion());
        }

        [Test()]
        public void SetPendingTest()
        {
            TestItem instanceNew = new TestItem();
            /* you cannot go from New to Pending */
            Assert.Throws<BadStateTransition>(() => instanceNew.SetPending());

            /* you can go from NewFromOutlook to Pending */
            TestItem instanceNewFromOutlook = new TestItem();
            instanceNewFromOutlook.SetNewFromOutlook();
            Assert.DoesNotThrow(() => instanceNewFromOutlook.SetPending());
            Assert.AreEqual(States.Pending, instanceNewFromOutlook.State);

            /* you can go from PresentAtStartup to Pending */
            TestItem instancePresentAtStartup = new TestItem();
            instancePresentAtStartup.SetPresentAtStartup();
            Assert.DoesNotThrow(() => instancePresentAtStartup.SetPending());
            Assert.AreEqual(States.Pending, instancePresentAtStartup.State);

            /* you cannot go from NewFromCRM to Pending */
            TestItem instanceNewFromCRM = new TestItem();
            instanceNewFromCRM.SetNewFromCRM();
            Assert.Throws<BadStateTransition>(() => instanceNewFromCRM.SetPending());

            /* you can go from Editing to Pending */
            TestItem instanceEditing = new TestItem();
            instanceEditing.SetEditing();
            Assert.DoesNotThrow(() => instanceEditing.SetPending());
            Assert.AreEqual(States.Pending, instanceEditing.State);

            /* you can go from Pending to Pending */
            TestItem instancePending = new TestItem();
            instancePending.SetNewFromOutlook();
            instancePending.SetPending();
            Assert.DoesNotThrow(() => instancePending.SetPending(), "you can go from Pending to Pending");
            Assert.AreEqual(States.Pending, instancePending.State);

            /* you cannot go from Queued to Pending */
            TestItem instanceQueued = new TestItem();
            instanceQueued.SetNewFromOutlook();
            instanceQueued.SetPending();
            instanceQueued.SetQueued();
            Assert.Throws<BadStateTransition>(() => instanceQueued.SetPending(), "you cannot go from Queued to Pending");

            /* you can go from Transmitted to Pending */
            TestItem instanceTransmitted = new TestItem();
            instanceTransmitted.SetNewFromOutlook();
            instanceTransmitted.SetPending();
            instanceTransmitted.SetQueued();
            instanceTransmitted.SetTransmitted();
            Assert.DoesNotThrow(() => instanceTransmitted.SetPending(true));

            /* you cannot go from Synced to Pending */
            TestItem instanceSynced = new TestItem();
            instanceSynced.SetNewFromOutlook();
            instanceSynced.SetPending();
            instanceSynced.SetQueued();
            instanceSynced.SetTransmitted();
            instanceSynced.SetSynced();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetPending());

            /* you cannot go from PendingDeletion to Pending */
            TestItem instancePendingDeletion = new TestItem();
            instancePendingDeletion.SetNewFromOutlook();
            instancePendingDeletion.SetPendingDeletion();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetPending());
        }

        [Test()]
        public void SetPresentAtStartupTest()
        {
            TestItem instanceNew = new TestItem();
            /* you can go from New to PresentAtStartup */
            Assert.DoesNotThrow(() => instanceNew.SetPresentAtStartup());

            /* you can go from NewFromOutlook to PresentAtStartup */
            TestItem instanceNewFromOutlook = new TestItem();
            instanceNewFromOutlook.SetNewFromOutlook();
            Assert.DoesNotThrow(() => instanceNewFromOutlook.SetPresentAtStartup());

            /* you cannot go from PresentAtStartup to PresentAtStartup */
            TestItem instancePresentAtStartup = new TestItem();
            instancePresentAtStartup.SetPresentAtStartup();
            Assert.Throws<BadStateTransition>(() => instancePresentAtStartup.SetPresentAtStartup());

            /* you cannot go from NewFromCRM to PresentAtStartup */
            TestItem instanceNewFromCRM = new TestItem();
            instanceNewFromCRM.SetNewFromCRM();
            Assert.Throws<BadStateTransition>(() => instanceNewFromCRM.SetPresentAtStartup());

            /* you cannot go from Editing to PresentAtStartup */
            TestItem instanceEditing = new TestItem();
            instanceEditing.SetEditing();
            Assert.Throws<BadStateTransition>(() => instanceEditing.SetPresentAtStartup());

            /* you cannot go from Pending to PresentAtStartup */
            TestItem instancePending = new TestItem();
            instancePending.SetNewFromOutlook();
            instancePending.SetPending();
            Assert.Throws<BadStateTransition>(() => instancePending.SetPresentAtStartup());

            /* you cannot go from Queued to PresentAtStartup */
            TestItem instanceQueued = new TestItem();
            instanceQueued.SetNewFromOutlook();
            instanceQueued.SetPending();
            instanceQueued.SetQueued();
            Assert.Throws<BadStateTransition>(() => instanceQueued.SetPresentAtStartup());

            /* you cannot go from Transmitted to PresentAtStartup */
            TestItem instanceTransmitted = new TestItem();
            instanceTransmitted.SetNewFromOutlook();
            instanceTransmitted.SetPending();
            instanceTransmitted.SetQueued();
            instanceTransmitted.SetTransmitted();
            Assert.Throws<BadStateTransition>(() => instanceTransmitted.SetPresentAtStartup());

            /* you cannot go from Synced to PresentAtStartup */
            TestItem instanceSynced = new TestItem();
            instanceSynced.SetNewFromOutlook();
            instanceSynced.SetPending();
            instanceSynced.SetQueued();
            instanceSynced.SetTransmitted();
            instanceSynced.SetSynced();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetPresentAtStartup());

            /* you cannot go from PendingDeletion to PresentAtStartup */
            TestItem instancePendingDeletion = new TestItem();
            instancePendingDeletion.SetNewFromOutlook();
            instancePendingDeletion.SetPendingDeletion();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetPresentAtStartup());
        }

        [Test()]
        public void SetQueuedTest()
        {
            TestItem instanceNew = new TestItem();
            /* you cannot go from New to Queued */
            Assert.Throws<BadStateTransition>(() => instanceNew.SetQueued());

            /* you cannot go from NewFromOutlook to Queued */
            TestItem instanceNewFromOutlook = new TestItem();
            instanceNewFromOutlook.SetNewFromOutlook();
            Assert.Throws<BadStateTransition>(() => instanceNewFromOutlook.SetQueued());

            /* you cannot go from PresentAtStartup to Queued */
            TestItem instancePresentAtStartup = new TestItem();
            instancePresentAtStartup.SetPresentAtStartup();
            Assert.Throws<BadStateTransition>(() => instancePresentAtStartup.SetQueued());

            /* you cannot go from NewFromCRM to Queued */
            TestItem instanceNewFromCRM = new TestItem();
            instanceNewFromCRM.SetNewFromCRM();
            Assert.Throws<BadStateTransition>(() => instanceNewFromCRM.SetQueued());

            /* you can go from Editing to Queued */
            TestItem instanceEditing = new TestItem();
            instanceEditing.SetEditing();
            Assert.Throws<BadStateTransition>(() => instanceEditing.SetQueued());

            /* you can go from PresentAtStartup to Queued */
            TestItem instancePending = new TestItem();
            instancePending.SetNewFromOutlook();
            instancePending.SetPending();
            Assert.DoesNotThrow(() => instancePending.SetQueued());

            /* you cannot go from Queued to Queued */
            TestItem instanceQueued = new TestItem();
            instanceQueued.SetNewFromOutlook();
            instanceQueued.SetPending();
            instanceQueued.SetQueued();
            Assert.Throws<BadStateTransition>(() => instanceQueued.SetQueued());

            /* you cannot go from Transmitted to Queued */
            TestItem instanceTransmitted = new TestItem();
            instanceTransmitted.SetNewFromOutlook();
            instanceTransmitted.SetPending();
            instanceTransmitted.SetQueued();
            instanceTransmitted.SetTransmitted();
            Assert.Throws<BadStateTransition>(() => instanceTransmitted.SetQueued());

            /* you cannot go from Synced to Queued */
            TestItem instanceSynced = new TestItem();
            instanceSynced.SetNewFromOutlook();
            instanceSynced.SetPending();
            instanceSynced.SetQueued();
            instanceSynced.SetTransmitted();
            instanceSynced.SetSynced();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetQueued());

            /* you cannot go from PendingDeletion to Queued */
            TestItem instancePendingDeletion = new TestItem();
            instancePendingDeletion.SetNewFromOutlook();
            instancePendingDeletion.SetPendingDeletion();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetQueued());
        }

        [Test()]
        public void SetSyncedTest()
        {
            Assert.Throws<BadStateTransition>(() => new TestItem().SetSynced(), "you cannot go from New to Synced");

            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.NewFromOutlook).SetSynced(), "you cannot go from NewFromOutlook to Synced");

            Assert.DoesNotThrow(() => new TestItem().StepTo(States.PresentAtStartup).SetSynced(), "you can go from PresentAtStartup to Synced");

            Assert.DoesNotThrow(() => new TestItem().StepTo(States.NewFromCRM).SetSynced(), "you can go from NewFromCRM to Synced");

            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Editing).SetSynced(), "you cannot go from Editing to Synced");

            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Pending).SetSynced(), "you cannot go from Pending to Synced");

            Assert.Throws<BadStateTransition>(() => new TestItem().StepTo(States.Queued).SetSynced(), "you cannot go from Queued to Synced");

            Assert.DoesNotThrow(() => new TestItem().StepTo(States.Transmitted).SetSynced(), "you can go from Transmitted to Synced");

            Assert.DoesNotThrow(() => new TestItem().StepTo(States.Synced).SetSynced(), "you can go from Synced to Synced");

            Assert.DoesNotThrow(() => new TestItem().StepTo(States.PendingDeletion).SetSynced(), "you can go from PendingDeletion to Synced");
        }

        [Test()]
        public void SetTransmittedTest()
        {
            TestItem instanceNew = new TestItem();
            /* you cannot go from New to Transmitted */
            Assert.Throws<BadStateTransition>(() => instanceNew.SetTransmitted());

            /* you cannot go from NewFromOutlook to Transmitted */
            TestItem instanceNewFromOutlook = new TestItem();
            instanceNewFromOutlook.SetNewFromOutlook();
            Assert.Throws<BadStateTransition>(() => instanceNewFromOutlook.SetTransmitted());

            /* you cannot go from PresentAtStartup to Transmitted */
            TestItem instancePresentAtStartup = new TestItem();
            instancePresentAtStartup.SetPresentAtStartup();
            Assert.Throws<BadStateTransition>(() => instancePresentAtStartup.SetTransmitted());

            /* you cannot go from NewFromCRM to Transmitted */
            TestItem instanceNewFromCRM = new TestItem();
            instanceNewFromCRM.SetNewFromCRM();
            Assert.Throws<BadStateTransition>(() => instanceNewFromCRM.SetTransmitted());

            /* you cannot go from Editing to Transmitted */
            TestItem instanceEditing = new TestItem();
            instanceEditing.SetEditing();
            Assert.Throws<BadStateTransition>(() => instanceEditing.SetTransmitted());

            /* you cannot go from PresentAtStartup to Transmitted */
            TestItem instancePending = new TestItem();
            instancePending.SetNewFromOutlook();
            instancePending.SetPending();
            Assert.Throws<BadStateTransition>(() => instancePending.SetTransmitted());

            /* you can go from Queued to Transmitted */
            TestItem instanceQueued = new TestItem();
            instanceQueued.SetNewFromOutlook();
            instanceQueued.SetPending();
            instanceQueued.SetQueued();
            Assert.DoesNotThrow(() => instanceQueued.SetTransmitted());

            /* you cannot go from Transmitted to Transmitted */
            TestItem instanceTransmitted = new TestItem();
            instanceTransmitted.SetNewFromOutlook();
            instanceTransmitted.SetPending();
            instanceTransmitted.SetQueued();
            instanceTransmitted.SetTransmitted();
            Assert.Throws<BadStateTransition>(() => instanceTransmitted.SetTransmitted());

            /* you cannot go from Synced to Transmitted */
            TestItem instanceSynced = new TestItem();
            instanceSynced.SetNewFromOutlook();
            instanceSynced.SetPending();
            instanceSynced.SetQueued();
            instanceSynced.SetTransmitted();
            instanceSynced.SetSynced();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetTransmitted());

            /* you cannot go from PendingDeletion to Transmitted */
            TestItem instancePendingDeletion = new TestItem();
            instancePendingDeletion.SetNewFromOutlook();
            instancePendingDeletion.SetPendingDeletion();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetTransmitted());
        }

        private class TestItem : AbstractItem
        {
            public override string CrmEntryId
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override string Description
            {
                get
                {
                    return "Test only";
                }
            }

            public override string DistinctFields
            {
                get
                {
                    return "Test only";
                }
            }

            public override string OutlookId
            {
                get
                {
                    return "Test only";
                }
            }

            public override void CacheItem()
            {
                /* do nothing */
            }

            public TestItem StepTo(States target)
            {
                if (this.State == States.New)
                {
                    switch (target)
                    {
                        case States.Editing:
                            this.SetNewFromOutlook();
                            this.SetEditing();
                            break;

                        case States.Invalid:
                            this.SetInvalid();
                            break;

                        case States.NewFromCRM:
                            this.SetNewFromCRM();
                            break;

                        case States.NewFromOutlook:
                            this.SetNewFromOutlook();
                            break;

                        case States.Pending:
                            this.SetNewFromOutlook();
                            this.SetPending();
                            break;

                        case States.PendingDeletion:
                            this.SetNewFromOutlook();
                            this.SetPendingDeletion();
                            break;

                        case States.PresentAtStartup:
                            this.SetPresentAtStartup();
                            break;

                        case States.Queued:
                            this.SetNewFromOutlook();
                            this.SetPending();
                            this.SetQueued();
                            break;

                        case States.Synced:
                            this.SetNewFromOutlook();
                            this.SetPending();
                            this.SetQueued();
                            this.SetTransmitted();
                            this.SetSynced();
                            break;

                        case States.Transmitted:
                            this.SetNewFromOutlook();
                            this.SetPending();
                            this.SetQueued();
                            this.SetTransmitted();
                            break;

                        default:
                            throw new Exception($"Unknown state {target}");
                    }
                }
                else
                {
                    throw new Exception("Can only StepTo from a new instance");
                }

                return this;
            }
        }
    }
}
