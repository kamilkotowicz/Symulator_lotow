using System.Runtime.Serialization;

namespace Symulator_lotow
{
    [Serializable]
    internal class BadDataInFileException : Exception
    {
        public BadDataInFileException()
        {
        }

        public BadDataInFileException(string? message) : base(message)
        {
        }

        public BadDataInFileException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BadDataInFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}