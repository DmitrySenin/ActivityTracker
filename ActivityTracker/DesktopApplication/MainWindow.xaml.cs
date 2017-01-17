namespace DesktopApplication
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using TrackingSystem.Database;
    using TrackingSystem.Tracking;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructos

        private ActivityTracker Tracker { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();

            // Reporary solution for test purposes
            // new ActivityStorage().RemoveAll();

            this.Tracker = new ActivityTracker();
            this.Tracker.StartTracking();
        }

        protected override void OnClosed(EventArgs e)
        {
            this.Tracker.StopTracking();
        }

        #endregion

        private void ShowDb_OnClick(object sender, RoutedEventArgs e)
        {
            var storage = new ActivityStorage();
            IList<Activity> activities = storage.GetActivities();

            this.ActivityLog.Clear();

            foreach (var activity in activities)
            {
                this.ActivityLog.AppendText("==========================================" + Environment.NewLine);
                this.ActivityLog.AppendText("Id:" + activity.Id + Environment.NewLine);
                this.ActivityLog.AppendText("ProcessName:" + activity.ProcessName + Environment.NewLine);
                this.ActivityLog.AppendText("WindowTitle:" + activity.WindowTitle + Environment.NewLine);
                this.ActivityLog.AppendText("StartDate:" + activity.StartDate + Environment.NewLine);
                this.ActivityLog.AppendText("EndDate:" + activity.EndDate + Environment.NewLine);
                this.ActivityLog.AppendText("==========================================" + Environment.NewLine);
            }
        }
    }
}
