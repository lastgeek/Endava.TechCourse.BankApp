using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Infrastracture.Persistance
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}