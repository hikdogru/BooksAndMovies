using AutoMapper;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Profiles
{
    public class TVShowProfile : Profile
    {
        public TVShowProfile()
        {
            CreateMap<TVShowModel, TVShowWatchList>().ReverseMap();
        }
    }
}
