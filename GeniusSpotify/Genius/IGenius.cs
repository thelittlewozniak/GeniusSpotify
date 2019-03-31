using Genius.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genius
{
    public interface IGenius
    {
        Task<Search> SearchSong(string name);
    }
}
