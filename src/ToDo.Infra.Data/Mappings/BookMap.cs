using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Models;

namespace ToDo.Infra.Data.Mappings
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title)
                .HasColumnType("varchar(150)");
            builder.Property(c => c.Author)
                .HasColumnType("varchar(150)");
            builder.Property(c => c.Synopsis)
                .HasColumnType("varchar(1000)");
            builder.Property(c => c.Available)
                .HasDefaultValue(true);
            builder.Property(c => c.Url)
                .HasColumnType("varchar(500)");
        }
    }
}
