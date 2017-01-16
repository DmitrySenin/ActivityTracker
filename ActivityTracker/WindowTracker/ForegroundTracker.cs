namespace WindowTracker
{
    using System;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    class ForegroundTracker
    {
        #region Public Methods

        public static void Main()
        {
            // Listen for foreground changes across all processes/threads on current desktop...
            IntPtr hhook = ForegroundTracker.SetWinEventHook(ForegroundTracker.EventSystemForeground, ForegroundTracker.EventSystemForeground, IntPtr.Zero,
                    ForegroundTracker.procDelegate, 0, 0, ForegroundTracker.WineventOutOfContext);

            // MessageBox provides the necessary mesage loop that SetWinEventHook requires.
            MessageBox.Show("Tracking focus, close message box to exit.");

            ForegroundTracker.UnhookWinEvent(hhook);
        }

        #endregion

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType,
           IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr
           hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess,
           uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        // Constants from winuser.h
        const uint EventSystemForeground = 3;

        const uint WineventOutOfContext = 0;

        // Need to ensure delegate is not collected while we're using it,
        // storing it in a class field is simplest way to do this.
        static WinEventDelegate procDelegate = new WinEventDelegate(ForegroundTracker.WinEventProc);

        static void WinEventProc(IntPtr hWinEventHook, uint eventType,
            IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            Console.WriteLine("Foreground changed to {0:x8}", hwnd.ToInt32());
        }
    }
}
