using BooksAndMovies.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.ValidationRules.FluentValidation
{
   public class TVShowValidator : AbstractValidator<TVShow>
    {
        public TVShowValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.BackdropPath).NotEmpty().NotNull();
            RuleFor(x => x.PosterPath).NotEmpty().NotNull();
            RuleFor(x => x.DatabaseSavingType).NotEmpty().NotNull();
        }
    }
}
