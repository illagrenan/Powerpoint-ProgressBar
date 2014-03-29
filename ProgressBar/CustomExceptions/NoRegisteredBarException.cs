using System;

namespace ProgressBar.CustomExceptions
{
    [Serializable]
    public class NoRegisteredBarException : Exception
    {
        public NoRegisteredBarException() { }
        public NoRegisteredBarException(string message) : base(message) { }
        public NoRegisteredBarException(string message, Exception inner) : base(message, inner) { }
        protected NoRegisteredBarException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}
