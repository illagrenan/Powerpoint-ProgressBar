#region

using System;
using System.Runtime.Serialization;

#endregion

namespace ProgressBar.CustomExceptions
{
    [Serializable]
    public class NoRegisteredBarException : Exception
    {
        public NoRegisteredBarException()
        {
        }

        public NoRegisteredBarException(string message) : base(message)
        {
        }

        public NoRegisteredBarException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoRegisteredBarException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}