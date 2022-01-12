using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService.Data
{
    public static class PrepDb
    {
        public static void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("InMemoryUserDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                if (!context.Users.Any())
                {
                    context.Users.AddRange(GetUsers());
                    context.SaveChanges();
                }
            }
        }

        private static IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User(){ Name = "Pan Józef", Funds = 66.5M },
                new User(){ Name = "Pan Staś", Funds = 132 },
                new User(){ Name = "Pani Beata", Funds = 1.69M }
            };
            return users;
        }
    }
}
