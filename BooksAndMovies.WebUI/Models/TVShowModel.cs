using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class TVShowModel : MediaModel
    {
        private string _name;
        private string _date;

        public string Name
        {
            get => _name.Length < 20 ? _name : _name.Substring(0, 20) + "...";
            set => _name = value;
        }
        public string OriginalName { get; set; }
        public string FirstAirDate
        {
            get => _date;
            set => _date = value.Length > 4 ?  DateTime.Parse(value, new CultureInfo("en-US")).Year.ToString() : value.ToString();
        }

    }
}
