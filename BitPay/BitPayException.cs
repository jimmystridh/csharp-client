using System;
using System.Runtime.Serialization;

namespace BitPayAPI
{
    /// <summary>
    /// Provides an API specific exception handler.
    /// </summary>
    [Serializable]
    public class BitPayException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public BitPayException()
        {
        }

        public BitPayException(string message) : base(message)
        {
        }

        public BitPayException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BitPayException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}