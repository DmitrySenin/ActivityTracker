namespace WindowTracker.WindowsOS
{
    using System;
    using System.Diagnostics;

    internal class Window
    {
        private IntPtr WindowHandle { get; set; }

        public Window(IntPtr windowHandle)
        {
            this.WindowHandle = windowHandle;
        }

        public Process AssociatedProcess
        {
            get { return WinApiWrapper.GetWindowProcess(this.WindowHandle); }
        }

        public string Title
        {
            get { return WinApiWrapper.GetWindowTitle(this.WindowHandle); }
        }

        public override bool Equals(object obj)
        {
            Window objectToComapre = obj as Window;

            if (objectToComapre == null)
            {
                return false;
            }

            return this.AssociatedProcess.Equals(objectToComapre.AssociatedProcess) &&
                   this.Title.Equals(objectToComapre.Title);
        }

        public override int GetHashCode()
        {
            return this.AssociatedProcess.GetHashCode();
        }
    }
}
