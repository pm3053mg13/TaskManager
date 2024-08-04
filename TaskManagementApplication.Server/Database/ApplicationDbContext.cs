using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Server.Domain;

namespace TaskManagementApplication.Server.Database
{
    public class ApplicationDbContext : DbContext
    {
        private List<Type> DomainClasses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Db Sets as per required database tables

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Domain.Task> Task { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Domain.Task>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Task>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().HasOne(x => x.Role).WithMany(x => x.User).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasMany(m => m.Tasks).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired();

            modelBuilder.Entity<Status>().HasMany(x => x.Tasks).WithOne(x => x.Status).HasForeignKey(x => x.StatusId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Domain.Task>().HasOne(x => x.Status).WithMany(x => x.Tasks).HasForeignKey(x => x.StatusId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
