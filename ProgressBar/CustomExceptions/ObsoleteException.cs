#region

using System;
using System.Runtime.Serialization;

#endregion

namespace ProgressBar.CustomExceptions
{
    [Serializable]
    public class ObsoleteException : Exception
    {
        public ObsoleteException()
        {
        }

        public ObsoleteException(string message) : base(message)
        {
        }

        public ObsoleteException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ObsoleteException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}