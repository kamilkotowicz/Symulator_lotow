using System.Runtime.Serialization;

namespace Symulator_lotow
{
    [Serializable]
    internal class BadDataInFileException : Exception
    {
        public BadDataInFileException(string? message) : base(message)
        {
        }
    }
    internal class BrakOdcinkaException : Exception
    {
        public BrakOdcinkaException(string? message) : base(message)
        {
        }
    }
}