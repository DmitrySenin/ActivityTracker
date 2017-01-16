namespace WindowTracker.WindowsOS
{
    using System;

    using Common;

    internal class WidowsEventHook : IOperationSystemHook<Window>, IDisposable
    {
        #region Delegates And Events

        public event EventHandler<Window> NewEvent;

        #endregion

        #region  Private Fields And Properties

        private IntPtr HookIdentifier { get; set; }

        private WinApiEvents TrackedEvent { get; set; }

        private uint TrackedProcessId { get; set; }

        private WinApiWrapper.WinApiCallbackSignature Callback { get; set; }

        #endregion

        #region Constructos

        public WidowsEventHook(WinApiEvents trackedEvent, uint processId = 0)
        {
            this.TrackedEvent = trackedEvent;
            this.TrackedProcessId = processId;
            this.Callback = this.HookBody;

            uint eventCode = (uint) this.TrackedEvent;
            this.HookIdentifier = WinApiWrapper.SetWinEventHook(eventCode, eventCode, IntPtr.Zero, this.Callback,
                this.TrackedProcessId, 0, WidowsEventHook.WinEventOutOfContext);
        }

        #endregion

        #region Methods

        private void HookBody(
            IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread,
            uint dwmsEventTime)
        {
            this.NewEvent?.Invoke(this, new Window(hwnd));
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {
            WinApiWrapper.UnhookWinEvent(this.HookIdentifier);
        }

        #endregion

        private const uint WinEventOutOfContext = 0;
    }
}
