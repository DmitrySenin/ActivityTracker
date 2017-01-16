namespace WindowTracker.WindowsOS
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    internal static class WinApiWrapper
    {
        #region Delegates And Events

        /// <summary>
        /// Decribes signature of standard WinApi callback.
        /// </summary>
        /// <param name="hWinEventHook"></param>
        /// <param name="eventType"></param>
        /// <param name="hwnd"></param>
        /// <param name="idObject"></param>
        /// <param name="idChild"></param>
        /// <param name="dwEventThread"></param>
        /// <param name="dwmsEventTime"></param>
        public delegate void WinApiCallbackSignature(
            IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread,
            uint dwmsEventTime);

        #endregion

        #region Methods

        /// <summary>
        /// Determines identifier of process associated with window.
        /// </summary>
        /// <param name="handle">A handle to the window</param>
        /// <param name="processId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines process associated with window.
        /// </summary>
        /// <param name="windowHandle">A handle to window.</param>
        /// <returns>Process running of window.</returns>
        public static Process GetWindowProcess(IntPtr windowHandle)
        {
            int processId = WinApiWrapper.GetWindowProcessId(windowHandle);
            return Process.GetProcessById(processId);
        }

        /// <summary>
        /// Determines identifier of process associated with window.
        /// </summary>
        /// <param name="windowHandle">A handle to window.</param>
        /// <returns>Identifier fo process.</returns>
        public static int GetWindowProcessId(IntPtr windowHandle)
        {
            int processId;
            WinApiWrapper.GetWindowThreadProcessId(windowHandle, out processId);
            return processId;
        }

        /// <summary>
        /// Determines title of window associated witn passed handle.
        /// </summary>
        /// <param name="windowHandle">A handle to window.</param>
        /// <returns>Title of window.</returns>
        public static string GetWindowTitle(IntPtr windowHandle)
        {
            return WinApiWrapper.GetWindowProcess(windowHandle).MainWindowTitle;
        }

        /// <summary>
        /// Subscribe passed callback on some particular Windows' events.
        /// </summary>
        /// <param name="eventMin"></param>
        /// <param name="eventMax"></param>
        /// <param name="hmodWinEventProc"></param>
        /// <param name="lpfnWinEventProc"></param>
        /// <param name="idProcess"></param>
        /// <param name="idThread"></param>
        /// <param name="dwFlags"></param>
        /// <returns>Pointer that is be used to unhook.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc,
            WinApiCallbackSignature lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        /// <summary>
        /// Unsubscribe callback associated with passed pointer.
        /// </summary>
        /// <param name="hWinEventHook"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        #endregion
    }
}
