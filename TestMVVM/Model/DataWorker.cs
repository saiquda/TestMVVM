using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMVVM.Model.Data;

namespace TestMVVM.Model
{
    /*Создание, редактирование, удаление*/
    public static class DataWorker
    {
        //получить все отделы
        public static List<Department> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }
        //получить все позиции
        public static List<Order> GetAllOrders()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Orders.ToList();
                return result;
            }
        }
        //получить всех сотрудников
        public static List<Staff> GetAllStaff()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Staff.ToList();
                return result;
            }
        }

        //получение руководителя по id
        public static Staff GetBossById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Staff pos = db.Staff.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        public static Department GetDepartmentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Department pos = db.Departments.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        public static Staff GetStaffById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Staff pos = db.Staff.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        //Создать отдел
        public static void CreateDepartment(string name)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                //проверка существования
                bool checkIsExist = db.Departments.Any(element => element.Name == name);
                if (!checkIsExist)
                {
                    Department newDepartment = new Department { Name = name };
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                }
            }
        }
        //Создать сотрудника
        public static void CreateStaff(string name, string date, int gender, Department department)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string[] nname = name.Split();
                Staff staff = new Staff { Name = nname[0],
                    Date = Convert.ToDateTime(date),
                    Sex = (Staff.Gender)gender,
                    DepartmentId = department.Id };
                if (nname.Length == 2)
                    staff.Surename = nname[1];
                else if (nname.Length == 3)
                {
                    staff.Surename = nname[1];
                    staff.Middlename = nname[2];
                }
                db.Staff.Add(staff);
                db.SaveChanges();
            }
        }
        //Создать заказ
        public static void CreateOrder(string contragent, string date,Staff staff)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                    Order newOrder = new Order { ContrAgent = contragent, Date = Convert.ToDateTime(date), StaffId = staff.Id};
                    db.Orders.Add(newOrder);
                    db.SaveChanges();
            }
        }
        
        public static void DeleteDepartment(Department department)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
            }
        }
        public static void DeleteStaff(Staff staff)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Staff.Remove(staff);
                db.SaveChanges();
            }
        }
        public static void DeleteOrder(Order order)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
        }
        public static void EditDepartment(Department oldDepartment, string name, Staff boss)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Department department = db.Departments.FirstOrDefault(p => p.Id == oldDepartment.Id);
                department.Name = name;
                if(department.BossId != boss.Id)
                {
                    department.BossId = boss.Id;
                    string date = Convert.ToString(department.DepartmentBoss.Date);
                    EditStaff(department.DepartmentBoss, department.DepartmentBoss.Name, date, (int)department.DepartmentBoss.Sex, department);
                }
                db.SaveChanges();
            }
        }
        public static void EditStaff(Staff oldStaff, string name, string date, int sex, Department department)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string[] newName = name.Split();
                Staff staff = db.Staff.FirstOrDefault(p => p.Id == oldStaff.Id);
                staff.Name = newName[0];
                if (newName.Length == 2)
                    staff.Surename = newName[1];
                else if (newName.Length == 3)
                {
                    staff.Surename = newName[1];
                    staff.Middlename = newName[2];
                }
                staff.Date = Convert.ToDateTime(date);
                staff.Sex = (Staff.Gender)sex;
                if(staff.DepartmentId != department.Id)
                {
                    staff.DepartmentId = department.Id;
                }
                
                db.SaveChanges();
            }
        }
        public static void EditOrder(Order oldOrder, string contrAgent, string date, Staff staff)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Order order = db.Orders.FirstOrDefault(p => p.Id == oldOrder.Id);
                order.ContrAgent = contrAgent;
                order.Date = Convert.ToDateTime(date);
                order.StaffId = staff.Id;
                db.SaveChanges();
            }
        }
    }
}
