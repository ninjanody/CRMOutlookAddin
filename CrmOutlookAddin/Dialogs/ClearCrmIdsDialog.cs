﻿namespace SuiteCRMAddIn.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    public partial class ClearCrmIdsDialog : Form
    {
        /// <summary>
        ///  The fred in which things get done (don't know whether I actually need this)
        /// </summary>
        private static BackgroundWorker fred = new BackgroundWorker();

        /// <summary>
        /// The total number of items which may have to be cleared.
        /// </summary>
        private readonly int total;

        private IEnumerable<WithRemovableSynchronisationProperties> items = Globals.ThisAddIn.GetSynchronisableItems();

        /// <summary>
        /// The logger to which I shall log.
        /// </summary>
        private ILogger log;

        /// <summary>
        /// The number of items remaining.
        /// </summary>
        private int remaining;

        public ClearCrmIdsDialog(ILogger log)
        {
            this.log = log;
            InitializeComponent();

            total = items.Count();
            remaining = total;
        }

        private void fred_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            if (Thread.CurrentThread.Name == null)
            {
                Thread.CurrentThread.Name = "ClearIds";
            }

            if (worker != null)
            {
                foreach (var item in this.items)
                {
                    item.RemoveSynchronisationProperties();
                    this.remaining--;
                    double percentageRemaining = (100.0 * this.remaining) / this.total;
                    worker.ReportProgress((int)(100.0 - percentageRemaining));

                    /* deal with any pending Windows messages, which we don't need to know about */
                    Application.DoEvents();

                    Thread.Sleep(10);
                }
            }
        }

        private void fred_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.ShowProgressOrClose();
        }

        private void ShowProgressOrClose()
        {
            if (this.remaining == 0)
            {
                this.log.Debug("ClearCrmIdsDialog: completed, closing.");
                base.Close();
            }
            else
            {
                double percentageRemaining = (100.0 * this.remaining) / this.total;

                this.progress.Value = 100 - (int)percentageRemaining;
                this.log.Debug($"ClearCrmIdsDialog: progress {percentageRemaining}%; {this.remaining}/{this.total}");
            }
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            yesButton.Enabled = false;
            noButton.Enabled = false;
            progress.Visible = true;

            fred.WorkerReportsProgress = true;

            fred.ProgressChanged += fred_ProgressChanged;
            fred.DoWork += fred_DoWork;

            fred.RunWorkerAsync();
        }
    }
}
