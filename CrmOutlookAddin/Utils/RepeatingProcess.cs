﻿/**
 * Outlook integration for SuiteCRM.
 * @package Outlook integration for SuiteCRM
 * @copyright Simon Brooke simon@journeyman.cc
 * @author Simon Brooke simon@journeyman.cc
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU LESSER GENERAL PUBLIC LICENCE as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU LESSER GENERAL PUBLIC LICENCE
 * along with this program; if not, see http://www.gnu.org/licenses
 * or write to the Free Software Foundation,Inc., 51 Franklin Street,
 * Fifth Floor, Boston, MA 02110-1301  USA
 */
namespace CrmOutlookAddin.Utils
{
    using Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// An agent which does something repeatedly.
    /// </summary>
    public abstract class RepeatingProcess
    {
        /// <summary>
        /// The name by which I am known.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The polling interval; default is five minutes.
        /// </summary>
        protected TimeSpan Interval = TimeSpan.FromMinutes(5);

        /// <summary>
        /// All known instances of repeating processes.
        /// </summary>
        private static ThreadSafeList<RepeatingProcess> allInstances = new ThreadSafeList<RepeatingProcess>();

        /// <summary>
        /// A mechanism to cancel delays during shutdown.
        /// </summary>
        private CancellationTokenSource interrupter = new CancellationTokenSource();

        /// <summary>
        /// When my last run ccompleted.
        /// </summary>
        /// <remarks>
        /// Initialised to 'now', so that at startup we won't mistakenly
        /// believe that things have happened after it.
        /// </remarks>
        private DateTime lastIterationCompleted = DateTime.Now;

        /// <summary>
        /// When the preceding run completed.
        /// </summary>
        /// <remarks>
        /// Initialised to 'min value' so that things that have happened since the last
        /// time Outlook was running don't get missed.
        /// </remarks>
        private DateTime previousIterationCompleted = DateTime.MinValue;

        /// <summary>
        /// The thread in which syncing is run.
        /// </summary>
        private Thread process;

        /// <summary>
        /// A lock on the process
        /// </summary>
        private object processLock = new object();

        /// <summary>
        /// The run state I am currently in.
        /// </summary>
        private RunState state = RunState.Stopped;

        public RepeatingProcess(string name)
        {
            this.Name = name;
            RepeatingProcess.allInstances.Add(this);
        }

        /// <summary>
        /// True if I am stopped, else false.
        /// </summary>
        public Boolean Stopped
        {
            get
            {
                return this.state == RunState.Stopped;
            }
        }

        /// <summary>
        /// When my last run completed.
        /// </summary>
        protected DateTime LastRunCompleted
        {
            get { return this.lastIterationCompleted; }
        }

        /// <summary>
        /// The logger to which I log.
        /// </summary>
        protected Log Log => Log.Instance;

        /// <summary>
        /// When the iteration prior to my last run completed.
        /// </summary>
        protected DateTime PreviousRunCompleted
        {
            get { return this.previousIterationCompleted; }
        }

        /// <summary>
        /// True if I should be active, else false.
        /// </summary>
        private Boolean IsActive
        {
            get { return this.state == RunState.Running || this.state == RunState.Waiting; }
        }

        /// <summary>
        /// Prepare to shutdown all running processes.
        /// </summary>
        /// <returns>zero if all processes are stopped, else the number of tasks to complete.</returns>
        public static int PrepareShutdownAll()
        {
            int tasks = 0;

            /* make a copy of all instances so I can remove items from it as I iterate */
            List<RepeatingProcess> stillAlive = new List<RepeatingProcess>();
            stillAlive.AddRange(RepeatingProcess.allInstances);

            foreach (RepeatingProcess process in stillAlive)
            {
                var stillToDo = process.PrepareShutdown();

                if (stillToDo == 0 && process.Stop())
                {
                    /* that's OK... */
                    Log.Instance.Info($"RepeatingProcess.PrepareShutdownAll: process {process.Name} is stopped.");
                    RepeatingProcess.allInstances.Remove(process);
                }
                else
                {
                    /* one for an unfinished process, plus one for each item still to do */
                    Log.Instance.Info($"RepeatingProcess.PrepareShutdownAll: process {process.Name} is running with {stillToDo} tasks to complete.");
                    tasks += stillToDo + 1;
                }
            }

            return tasks;
        }

        /// <summary>
        /// Put me into a mode where I finish all the work I have to do quickly.
        /// </summary>
        /// <remarks>
        /// This method will be called repeatedly; overrides should be written with this in mind.
        /// </remarks>
        /// <returns>Zero if I may be stopped immediately (this is the default);
        /// otherwise an integer indicating the number of work units to complete
        /// before I can be stopped.</returns>
        public virtual int PrepareShutdown()
        {
            return 0;
        }

        /// <summary>
        /// If I am not currently running, set me running.
        /// </summary>
        public void Start()
        {
            lock (this.processLock)
            {
                switch (this.state)
                {
                    case RunState.Stopped:
                        this.process = new Thread(() => this.PerformRepeatedly());
                        this.process.Name = $"{this.Name}";
                        Log.Debug($"Starting thread {this.Name}");
                        this.state = RunState.Running;
                        this.process.Start();
                        break;

                    case RunState.Stopping:
                        this.state = RunState.Running;
                        break;

                    default:
                        Log.Warn($"Did not start thread {this.Name} as it appears to be running");
                        break;
                }
            }
        }

        /// <summary>
        /// Stop me at the end of my current iteration; does not force an immediate
        /// stop unless no work is currently active.
        /// </summary>
        /// <returns>true if I am now stopped.</returns>
        public bool Stop()
        {
            lock (processLock)
            {
                if (this.IsActive)
                {
                    this.state = RunState.Stopping;
                    interrupter.Cancel();
                    Log.Debug($"Stopping thread {this.Name} at end of current iteration.");
                }
            }

            return this.Stopped;
        }

        /// <summary>
        /// Do whatever it is I do, once.
        /// </summary>
        internal abstract void PerformIteration();

        /// <summary>
        /// Do whatever it is I do repeatedly.
        /// </summary>
        private async void PerformRepeatedly()
        {
            do
            {
                var fred = Thread.CurrentThread;
                if (fred.Name == null)
                {
                    Log.Warn($"Anonymous thread {fred.ManagedThreadId} running as '{this.Name}'.");
                }

                lock (processLock)
                {
                    this.state = RunState.Running;
                }

                /* deal with any pending Windows messages, which we don't need to know about */
                System.Windows.Forms.Application.DoEvents();

                this.previousIterationCompleted = this.lastIterationCompleted;
                this.lastIterationCompleted = DateTime.UtcNow;

                if (this.state == RunState.Running)
                {
                    try
                    {
                        lock (processLock)
                        {
                            this.state = RunState.Waiting;
                        }
                        await Task.Delay(this.Interval, interrupter.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        // that's OK, that's what's supposed to happen.
                    }
                }
            }
            while (this.IsActive);

            lock (processLock)
            {
                Log.Debug($"Stopping thread {this.Name} immediately.");
                this.state = RunState.Stopped;
                this.process = null;
            }
        }
    }
}
