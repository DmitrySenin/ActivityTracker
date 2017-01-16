namespace DesktopApplication
{
    using System;
    using System.Windows;

    using WindowTracker.Trackers;
    using WindowTracker.WindowsOS;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructos

        private IWindowStateTracker Tracker { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();

            this.Tracker = new ActiveWindowStateTracker();
            this.Tracker.WindowStateChanged += this.Tracker_WindowStateChanged;
            this.Tracker.StartTracking();
        }

        private void Tracker_WindowStateChanged(object sender, WindowTracker.Common.WindowStateChangedEventArgs e)
        {
            this.ActivityLog.AppendText("=============================================" + Environment.NewLine);
            this.ActivityLog.AppendText("Title: " + e.Title + Environment.NewLine);
            this.ActivityLog.AppendText("ProcessName: " + e.ProcessName + Environment.NewLine);
            this.ActivityLog.AppendText("=============================================" + Environment.NewLine);
        }

        protected override void OnClosed(EventArgs e)
        {
            this.Tracker.StopTracking();
        }

        #endregion
    }
}
