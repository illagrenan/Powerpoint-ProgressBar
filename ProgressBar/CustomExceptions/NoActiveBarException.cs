#region

using System;
using System.Runtime.Serialization;

#endregion

namespace ProgressBar.CustomExceptions
{
    [Serializable]
    public class NoActiveBarException : Exception
    {
        public NoActiveBarException()
        {
        }

        public NoActiveBarException(string message) : base(message)
        {
        }

        public NoActiveBarException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoActiveBarException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}