namespace WindowTracker.Trackers
{
    using System;

    using Common;

    public interface IWindowStateTracker
    {
        #region Delegates And Events

        event EventHandler<WindowStateChangedEventArgs> WindowStateChanged;

        #endregion

        #region Public Methods

        void StartTracking();

        void StopTracking();

        #endregion
    }
}
