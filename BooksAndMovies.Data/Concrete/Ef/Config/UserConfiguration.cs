using BCrypt.Net;
using BooksAndMovies.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BooksAndMovies.Data.Concrete.Ef.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);


            builder.Property(b => b.LastName)
                    .IsRequired()
                    .HasMaxLength(50);


            builder.Property(b => b.Password)
                    .IsRequired()
                    .HasMaxLength(100);



            builder.HasData(
                              new User {Id = 10 , FirstName = "Demo", LastName = "User", Email = "demouser@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("demouser") }
                           );
        }


    }
}
