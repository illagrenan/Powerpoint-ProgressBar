using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar._CustomExceptions
{
    [Serializable]
    public class NoActiveBarException : Exception
    {
        public NoActiveBarException() { }
        public NoActiveBarException(string message) : base(message) { }
        public NoActiveBarException(string message, Exception inner) : base(message, inner) { }
        protected NoActiveBarException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
