namespace TrackingSystem.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ActivityStorage
    {
        #region Public Methods

        public void Add(Activity activity)
        {
            TimeSpan offset = activity.EndDate - activity.StartDate;

            if (offset.TotalSeconds > 30)
            {
                using (var context = new ActivityContext())
                {
                    context.Activities.Add(activity);
                    context.SaveChanges();
                }
            }
        }

        public IList<Activity> GetActivities()
        {
            using (var context = new ActivityContext())
            {
                return context.Activities.ToList();
            }
        }

        #endregion
    }
}
