namespace WindowTracker.Common
{
    public class WindowStateChangedEventArgs
    {
        #region Constructos

        public WindowStateChangedEventArgs(string title, string processName)
        {
            this.Title = title;
            this.ProcessName = processName;
        }

        #endregion

        #region Public Fields And Properties

        public string Title { get; private set; }

        public string ProcessName { get; private set; }

        #endregion
    }
}
