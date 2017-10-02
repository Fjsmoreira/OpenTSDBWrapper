using System;

namespace OpenTSDBApiWrapper.Exceptions
{
    public class MissingAttributeException : Exception
    {
        public MissingAttributeException(string message) : base(message)
        {
        }
    }
}
