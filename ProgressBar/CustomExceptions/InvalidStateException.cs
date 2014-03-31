#region

using System;
using System.Runtime.Serialization;

#endregion

namespace ProgressBar.CustomExceptions
{
    [Serializable]
    public class InvalidStateException : Exception
    {
        public InvalidStateException()
        {
        }

        public InvalidStateException(string message) : base(message)
        {
        }

        public InvalidStateException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidStateException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}