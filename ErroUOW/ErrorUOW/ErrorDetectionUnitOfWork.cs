namespace ErrorUOW.ErrorUnitOfWork
{
    using System;
    using NServiceBus.UnitOfWork;

    public class ErrorDetectionUnitOfWork : IManageUnitsOfWork
    {
        public void Begin()
        {
        }

        public void End(Exception ex = null)
        {
            if (ex != null)
                Console.WriteLine("Error with Inner Exception: {0}", ex.InnerException.InnerException ?? ex.InnerException);
        }
    }
}