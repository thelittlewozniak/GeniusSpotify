using System;
using System.Runtime.Serialization;

namespace Spotify.Exception
{
    class SpotifyNotFoundException : SystemException
    {
        public SpotifyNotFoundException()
        {
        }

        public SpotifyNotFoundException(string message) : base(message)
        {
        }

        public SpotifyNotFoundException(string message, SystemException innerException) : base(message, innerException)
        {
        }

        protected SpotifyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
