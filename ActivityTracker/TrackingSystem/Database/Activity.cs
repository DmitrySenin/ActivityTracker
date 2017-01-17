namespace TrackingSystem.Database
{
    using System;

    public class Activity
    {
        #region Public Fields And Properties

        public DateTime EndDate { get; set; }

        public  int Id { get; set; }

        public string ProcessName { get; set; }

        public DateTime StartDate { get; set; }

        public string WindowTitle { get; set; }

        #endregion
    }
}
