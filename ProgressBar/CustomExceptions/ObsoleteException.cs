using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.CustomExceptions
{
    [Serializable]
    public class ObsoleteException : Exception
    {
        public ObsoleteException() { }
        public ObsoleteException(string message) : base(message) { }
        public ObsoleteException(string message, Exception inner) : base(message, inner) { }
        protected ObsoleteException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
