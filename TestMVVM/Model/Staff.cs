using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVVM.Model
{
    public class Staff
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surename { get; set; }

        public string Middlename { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public enum Gender { Мужчина, Женщина }

        public Gender Sex { get; set; }

        public virtual Department Department { get; set; }

        public int DepartmentId { get; set; }
        [NotMapped]
        public Department DepartmentName
        {
            get
            {
                return DataWorker.GetDepartmentById(DepartmentId);
            }
        }
    }
}
