using System;

namespace CtgModels.Exceptions.Data
{
    public class DbException : Exception
    {
        public DbExceptionReason Reason { get; }

        public DbException(DbExceptionReason reason) : base(reason.ToString())
        {
            Reason = reason;
        }
        public DbException(DbExceptionReason reason, string message) : base(message)
        {
            Reason = reason;
        }

    }
}