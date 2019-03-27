using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeniusSpotify
{
    class SpotifyNotFoundException : Exception
    {
        public SpotifyNotFoundException()
        {
        }

        public SpotifyNotFoundException(string message) : base(message)
        {
        }

        public SpotifyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SpotifyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
