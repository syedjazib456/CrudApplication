using Microsoft.EntityFrameworkCore;

namespace CrudApplication.Models
{
    public class ApplicationDbContext:DbContext //child class
    {
        //contructor DbContext constructor
        public ApplicationDbContext(DbContextOptions options):base(options)//overriding
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
