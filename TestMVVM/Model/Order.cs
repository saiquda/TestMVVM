using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVVM.Model
{
    public class Order
    {
        public int Id { get; set; }

        public string ContrAgent { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public virtual Staff Staff { get; set; }

        public int StaffId { get; set; }
        [NotMapped]
        public Staff DepartmentBoss
        {
            get
            {
                return DataWorker.GetStaffById(StaffId);
            }
        }
    }
}
