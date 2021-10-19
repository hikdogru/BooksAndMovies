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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(b => b.SmallThumbnail)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(b => b.Thumbnail)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(x => x.DatabaseSavingType)
                .IsRequired()
                .HasMaxLength(1);
        }
    }
}
