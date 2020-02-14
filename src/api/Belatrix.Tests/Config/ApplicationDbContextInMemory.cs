using Belatrix.Models.Domain;
using Belatrix.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Belatrix.Tests
{
    public static class ApplicationDbContextInMemory
    {
        public static ApplicationDbContext Get() 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"BelatrixChallenge.Db")
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
