using CrmOutlookAddin.Core;
using CrmOutlookAddin.Exceptions;
using NUnit.Framework;
using System;

namespace CrmOutlookAddin.Wrappers.Tests
{
    [TestFixture()]
    public class StateTransitionEngineTests
    {
        [Test()]
        public void SetEditingTest()
        {
            TestItem instanceNew = new TestItem();
            /* you can go from New to Editing */
            Assert.DoesNotThrow(() => instanceNew.SetEditing());

            /* you can go from NewFromOutlook to Editing */
            TestItem instanceNewFromOutlook = new TestItem();
            instanceNewFromOutlook.SetNewFromOutlook();
            Assert.DoesNotThrow(() => instanceNewFromOutlook.SetEditing());

            /* you can go from PresentAtStartup to Editing */
            TestItem instancePresentAtStartup = new TestItem();
            instancePresentAtStartup.SetPresentAtStartup();
            Assert.DoesNotThrow(() => instancePresentAtStartup.SetEditing());

            /* you can go from NewFromCRM to Editing */
            TestItem instanceNewFromCRM = new TestItem();
            instanceNewFromCRM.SetNewFromCRM();
            Assert.DoesNotThrow(() => instanceNewFromCRM.SetEditing());

            /* you can go from Editing to Editing */
            TestItem instanceEditing = new TestItem();
            instanceEditing.SetEditing();
            Assert.DoesNotThrow(() => instanceEditing.SetEditing());

            /* you can go from Pending to Editing */
            TestItem instancePending = new TestItem();
            instancePending.SetNewFromOutlook();
            instancePending.SetPending();
            Assert.DoesNotThrow(() => instancePending.SetEditing());

            /* you can go from Queued to Editing */
            TestItem instanceQueued = new TestItem();
            instanceQueued.SetNewFromOutlook();
            instanceQueued.SetPending();
            instanceQueued.SetQueued();
            Assert.DoesNotThrow(() => instanceQueued.SetEditing());

            /* you cannot go from Transmitted to Editing */
            TestItem instanceTransmitted = new TestItem();
            instanceTransmitted.SetNewFromOutlook();
            instanceTransmitted.SetPending();
            instanceTransmitted.SetQueued();
            instanceTransmitted.SetTransmitted();
            Assert.Throws<BadStateTransition>(() => instanceTransmitted.SetEditing());

            /* you can go from Synced to Editing */
            TestItem instanceSynced = new TestItem();
            instanceSynced.SetNewFromOutlook();
            instanceSynced.SetPending();
            instanceSynced.SetQueued();
            instanceSynced.SetTransmitted();
            instanceSynced.SetSynced();
            Assert.DoesNotThrow(() => instanceSynced.SetEditing());

            /* you can go from PendingDeletion to Editing */
            TestItem instancePendingDeletion = new TestItem();
            instancePendingDeletion.SetNewFromOutlook();
            instancePendingDeletion.SetPendingDeletion();
            Assert.DoesNotThrow(() => instanceSynced.SetEditing());
        }

        [Test()]
        public void SetInvalidTest()
        {
            /* you can go from New to Invalid */
            Assert.DoesNotThrow(() => new TestItem().SetInvalid());

            /* you cannot go from NewFromOutlook to Invalid */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.NewFromOutlook).SetInvalid());

            /* you cannot go from PresentAtStartup to Invalid */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.PresentAtStartup).SetInvalid());

            /* you cannot go from NewFromCRM to Invalid */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.NewFromCRM).SetInvalid());

            /* you cannot go from Editing to Invalid */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.Editing).SetInvalid());

            /* you cannot go from PresentAtStartup to Invalid */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.Pending).SetInvalid());

            /* you cannot go from Queued to Invalid */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.Queued).SetInvalid());

            /* you cannot go from Transmitted to Invalid */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.Transmitted).SetInvalid());

            /* you cannot go from Synced to Invalid */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.Synced).SetInvalid());

            /* you cannot go from PendingDeletion to Invalid */
            Assert.DoesNotThrow(() => new TestItem().StepTo(States.PendingDeletion).SetInvalid());
        }

        [Test()]
        public void SetNewFromCRMTest()
        {
            TestItem instanceNew = new TestItem();
            /* you can go from New to NewFromCRM */
            Assert.DoesNotThrow(() => instanceNew.SetNewFromCRM());

            /* you cannot go from NewFromOutlook to NewFromCRM */
            TestItem instanceNewFromOutlook = new TestItem();
            instanceNewFromOutlook.SetNewFromOutlook();
            Assert.DoesNotThrow(() => instanceNewFromOutlook.SetNewFromCRM());

            /* you cannot go from PresentAtStartup to NewFromCRM */
            TestItem instancePresentAtStartup = new TestItem();
            instancePresentAtStartup.SetPresentAtStartup();
            Assert.Throws<BadStateTransition>(() => instancePresentAtStartup.SetNewFromCRM());

            /* you cannot go from NewFromCRM to NewFromCRM */
            TestItem instanceNewFromCRM = new TestItem();
            instanceNewFromCRM.SetNewFromCRM();
            Assert.Throws<BadStateTransition>(() => instanceNewFromCRM.SetNewFromCRM());

            /* you cannot go from Editing to NewFromCRM */
            TestItem instanceEditing = new TestItem();
            instanceEditing.SetEditing();
            Assert.Throws<BadStateTransition>(() => instanceEditing.SetNewFromCRM());

            /* you cannot go from PresentAtStartup to NewFromCRM */
            TestItem instancePending = new TestItem();
            instancePending.SetNewFromOutlook();
            instancePending.SetPending();
            Assert.Throws<BadStateTransition>(() => instancePending.SetNewFromCRM());

            /* you cannot go from Queued to NewFromCRM */
            TestItem instanceQueued = new TestItem();
            instanceQueued.SetNewFromOutlook();
            instanceQueued.SetPending();
            instanceQueued.SetQueued();
            Assert.Throws<BadStateTransition>(() => instanceQueued.SetNewFromCRM());

            /* you cannot go from Transmitted to NewFromCRM */
            TestItem instanceTransmitted = new TestItem();
            instanceTransmitted.SetNewFromOutlook();
            instanceTransmitted.SetPending();
            instanceTransmitted.SetQueued();
            instanceTransmitted.SetTransmitted();
            Assert.Throws<BadStateTransition>(() => instanceTransmitted.SetNewFromCRM());

            /* you cannot go from Synced to NewFromCRM */
            TestItem instanceSynced = new TestItem();
            instanceSynced.SetNewFromOutlook();
            instanceSynced.SetPending();
            instanceSynced.SetQueued();
            instanceSynced.SetTransmitted();
            instanceSynced.SetSynced();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetNewFromCRM());

            /* you cannot go from PendingDeletion to NewFromCRM */
            TestItem instancePendingDeletion = new TestItem();
            instancePendingDeletion.SetNewFromOutlook();
            instancePendingDeletion.SetPendingDeletion();
            Assert.Throws<BadStateTransition>(() => instanceSynced.SetNewFromCRM());
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

            /* you can go from PresentAtStartup to Pending */
            TestItem instancePresentAtStartup = new TestItem();
            instancePresentAtStartup.SetPresentAtStartup();
            Assert.DoesNotThrow(() => instancePresentAtStartup.SetPending());

            /* you cannot go from NewFromCRM to Pending */
            TestItem instanceNewFromCRM = new TestItem();
            instanceNewFromCRM.SetNewFromCRM();
            Assert.Throws<BadStateTransition>(() => instanceNewFromCRM.SetPending());

            /* you can go from Editing to Pending */
            TestItem instanceEditing = new TestItem();
            instanceEditing.SetEditing();
            Assert.DoesNotThrow(() => instanceEditing.SetPending());

            /* you cannot go from PresentAtStartup to Pending */
            TestItem instancePending = new TestItem();
            instancePending.SetNewFromOutlook();
            instancePending.SetPending();
            Assert.DoesNotThrow(() => instancePending.SetPending());

            /* you cannot go from Queued to Pending */
            TestItem instanceQueued = new TestItem();
            instanceQueued.SetNewFromOutlook();
            instanceQueued.SetPending();
            instanceQueued.SetQueued();
            Assert.Throws<BadStateTransition>(() => instanceQueued.SetPending());

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
            public override string Description
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

            public AbstractItem StepTo(States target)
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
