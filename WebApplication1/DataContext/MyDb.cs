using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DataContext
{
    public class MyDb : DbContext
    {
        public MyDb(DbContextOptions<MyDb> options) : base(options)
        { }

        public DbSet<School> Schools => Set<School>();
    }

}
