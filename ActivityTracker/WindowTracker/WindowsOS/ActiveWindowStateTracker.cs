namespace WindowTracker.WindowsOS
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    using Common;

    using Trackers;

    public class ActiveWindowStateTracker: IWindowStateTracker
    {
        #region Delegates And Events

        public event EventHandler<WindowStateChangedEventArgs> WindowStateChanged;

        #endregion

        #region  Private Fields And Properties

        private WidowsEventHook ActiveWindowChanged { get; set; }

        private WidowsEventHook ActiveWindowTitleChanged { get; set; }

        private Window TrackedWindow { get; set; }

        #endregion

        #region Methods

        private void ActiveWindowChanged_NewEvent(object sender, Window newWindow)
        {
            if (this.TrackedWindow == null || !this.TrackedWindow.Equals(newWindow))
            {
                this.TrackedWindow = newWindow;
                this.FireNewWindowStateEvent();

                this.ActiveWindowChanged.NewEvent -= this.ActiveWindowChanged_NewEvent;
                this.ActiveWindowChanged.Dispose();
                this.ActiveWindowChanged = new WidowsEventHook(WinApiEvents.ActiveProcessChanged);
                this.ActiveWindowChanged.NewEvent += this.ActiveWindowChanged_NewEvent;

                this.ActiveWindowTitleChanged.NewEvent -= this.ActiveWindowTitleChanged_NewEvent;
                this.ActiveWindowTitleChanged.Dispose();
                this.ActiveWindowTitleChanged = new WidowsEventHook(WinApiEvents.ObjectNameChanged,
                    (uint)this.TrackedWindow.AssociatedProcess.Id);
                this.ActiveWindowTitleChanged.NewEvent += this.ActiveWindowTitleChanged_NewEvent;
            }
        }

        private void ActiveWindowTitleChanged_NewEvent(object sender, Window newWindow)
        {
            if (!this.TrackedWindow.Title.Equals(newWindow.Title))
            {
                this.FireNewWindowStateEvent();
            }
        }

        #endregion

        #region Public Methods

        public void StartTracking()
        {
            this.ActiveWindowChanged = new WidowsEventHook(WinApiEvents.ActiveProcessChanged);
            this.ActiveWindowChanged.NewEvent += this.ActiveWindowChanged_NewEvent;

            this.ActiveWindowTitleChanged = new WidowsEventHook(WinApiEvents.ObjectNameChanged);
            this.ActiveWindowTitleChanged.NewEvent += this.ActiveWindowTitleChanged_NewEvent;
        }

        public void StopTracking()
        {
            this.ActiveWindowChanged.NewEvent -= this.ActiveWindowChanged_NewEvent;
            this.ActiveWindowTitleChanged.NewEvent -= this.ActiveWindowTitleChanged_NewEvent;

            this.ActiveWindowChanged.Dispose();
            this.ActiveWindowTitleChanged.Dispose();
        }

        #endregion

        private void FireNewWindowStateEvent()
        {
            this.WindowStateChanged?.Invoke(this, this.ConstructWindowStateChangedEventArgs());
        }

        private WindowStateChangedEventArgs ConstructWindowStateChangedEventArgs()
        {
            return new WindowStateChangedEventArgs(this.TrackedWindow.Title, this.TrackedWindow.AssociatedProcess.ProcessName);
        }
    }
}
