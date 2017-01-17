namespace TrackingSystem.Database
{
    using System.Data.Entity;

    internal class ActivityContext: DbContext
    {
        #region Constructos

        public ActivityContext() : base("ActivityTracker")
        {}

        #endregion

        #region Public Fields And Properties

        public DbSet<Activity> Activities { get; set; }

        #endregion
    }
}
