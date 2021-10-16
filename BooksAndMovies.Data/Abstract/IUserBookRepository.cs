using BooksAndMovies.Core.Data;
using BooksAndMovies.Core.Entities;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Abstract
{
    public interface IUserBookRepository : IEntityRepository<UserBook>
    {
    }
}
