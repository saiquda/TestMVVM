using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVVM.Model
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("BossFK")]
        public virtual Staff Boss { get; set; }

        public int BossId { get; set; }

        [NotMapped]
        public Staff DepartmentBoss
        {
            get
            {
                return DataWorker.GetBossById(BossId);
            }
        }

        public List<Staff> Staff { get; set; }
    }
}
