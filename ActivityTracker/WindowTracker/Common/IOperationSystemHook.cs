namespace WindowTracker.Common
{
    using System;

    internal interface IOperationSystemHook<T>
    {
        #region Delegates And Events

        event EventHandler<T> NewEvent;

        #endregion
    }
}
