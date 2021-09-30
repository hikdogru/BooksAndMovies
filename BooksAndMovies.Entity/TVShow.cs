using BooksAndMovies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Entity
{
    public class TVShow : Media, IEntity
    {
        private string _name;

        public string Name
        {
            get => _name.Length < 20 ? _name : _name.Substring(0, 20) + "...";
            set => _name = value;
        }
        public string OriginalName { get; set; }
        public string FirstAirDate { get; set; }
    }
}
