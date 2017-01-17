namespace TrackingSystem.Tracking
{
    using System;

    using Database;

    using WindowTracker.Trackers;
    using WindowTracker.WindowsOS;

    public class ActivityTracker
    {
        private IWindowStateTracker WindowTracker { get; set; }

        private Activity CurrentActivity { get; set; }

        public ActivityTracker()
        {
            this.WindowTracker = new ActiveWindowStateTracker();
            this.WindowTracker.WindowStateChanged += this.WindowTracker_WindowStateChanged;
        }

        private void WindowTracker_WindowStateChanged(object sender, WindowTracker.Common.WindowStateChangedEventArgs e)
        {
            if (this.CurrentActivity == null)
            {
                this.CurrentActivity = new Activity()
                {
                    WindowTitle = e.Title,
                    ProcessName = e.ProcessName,
                    StartDate = DateTime.UtcNow
                };

                return;
            }

            this.CurrentActivity.EndDate = DateTime.UtcNow;

            var storage = new ActivityStorage();
            storage.Add(this.CurrentActivity);

            this.CurrentActivity = new Activity()
            {
                WindowTitle = e.Title,
                ProcessName = e.ProcessName,
                StartDate = DateTime.UtcNow
            };
        }

        public void StartTracking()
        {
            this.WindowTracker.StartTracking();
        }

        public void StopTracking()
        {
            this.WindowTracker.StopTracking();
        }
    }
}
