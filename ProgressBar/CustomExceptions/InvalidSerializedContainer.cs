using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ProgressBar.CustomExceptions
{
    [Serializable]
    public class InvaidSerializedContainerException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvaidSerializedContainerException()
        {
        }

        public InvaidSerializedContainerException(string message) : base(message)
        {
        }

        public InvaidSerializedContainerException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvaidSerializedContainerException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
