using BooksAndMovies.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Concrete.Ef.Config
{
    public class TVShowConfiguration : IEntityTypeConfiguration<TVShow>
    {
        public void Configure(EntityTypeBuilder<TVShow> builder)
        {
            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(b => b.OriginalName)
                .HasMaxLength(100);

            builder
                .Property(b => b.PosterPath)
                .IsRequired()
                .HasMaxLength(100);


            builder
                .Property(b => b.BackdropPath)
                .IsRequired()
                .HasMaxLength(100);


            builder
                .Property(b => b.DatabaseSavingType)
                .IsRequired()
                .HasMaxLength(1);


            builder
                .Property(b => b.Overview)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
