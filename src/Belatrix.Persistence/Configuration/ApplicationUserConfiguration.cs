using Belatrix.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Belatrix.Persistence.Configuration
{
    public class ApplicationUserConfiguration
    {
        public ApplicationUserConfiguration(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
        }
    }
}
