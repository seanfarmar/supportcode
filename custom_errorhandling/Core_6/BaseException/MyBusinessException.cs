using System;
using System.Runtime.Serialization;

namespace BaseException
{
    public class MyBusinessException : ApplicationException
    {
        public MyBusinessException()
        {
        }

        public MyBusinessException(string message) : base(message)
        {
        }

        public MyBusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyBusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
