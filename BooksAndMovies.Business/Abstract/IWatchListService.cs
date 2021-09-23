using System;
using System.Collections.Generic;
using BooksAndMovies.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Abstract
{
    public interface IWatchListService
    {
        void Add(WatchList entity);
    }
}
