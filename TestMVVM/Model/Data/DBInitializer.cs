using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMVVM.Model.Data
{
    class DBInitializer
    {
        public static void Initializer(ApplicationContext db)
        {
            db.Database.EnsureCreated();
            if (db.Staff.Any())
                return;
            var department = new Department[]
            {
                new Department{Name="Vld" },
                new Department{Name="Msc" },
                new Department{Name="Aboba" } };
            foreach (Department s in department)
                db.Departments.Add(s);
            db.SaveChanges();
            var staff = new Staff[]
            {
                new Staff{Name="Сократ", Surename="Лагкуев", Middlename="Сосланович", DepartmentId=department[0].Id, Date=Convert.ToDateTime("2000-10-21"),Sex=0},
                new Staff{Name="Петр", Surename="Петров", Middlename="Петрович", DepartmentId=department[1].Id, Date=Convert.ToDateTime("1990-01-01"),Sex=0},
                new Staff{Name="Илья", Surename="Макаренко", DepartmentId=department[2].Id, Date=Convert.ToDateTime("2000-01-20"),Sex=0}
            };
            foreach (Staff s in staff)
                db.Staff.Add(s);
            db.SaveChanges();
            for (int i = 0; i < 3; i++)
            {
                department[i].BossId = staff[i].Id;
            }
            db.SaveChanges();
        }
    }
}
