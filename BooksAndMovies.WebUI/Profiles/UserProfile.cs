using AutoMapper;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoginModel>().ReverseMap();
        }
    }
}
