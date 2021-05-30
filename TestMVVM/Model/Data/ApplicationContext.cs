using Microsoft.EntityFrameworkCore;

namespace TestMVVM.Model.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }


        public ApplicationContext()
        {
            DBInitializer.Initializer(this);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestMVVM;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasOne(x => x.Boss).WithOne(z => z.Department).IsRequired(false);
            //ApplicationContext db = new ApplicationContext();
            //Staff staff = new Staff { Name = "Сократ", Surename = "Лагкуев", Middlename = "Сосланович", Sex = 0 };
            //Department department = new Department { Name = "Vld", Boss = staff, BossId = staff.Id };
            //db.Add(staff);
            //db.Add(department);
            //db.SaveChanges();
        }

    }
    
}
