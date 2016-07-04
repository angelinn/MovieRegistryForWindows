using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class WindowsUser : BaseEntity
    {
        public WindowsUser()
        {
            Records = new HashSet<Record>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Record> Records { get; set; }
        
        public int GetId()
        {
            return ID;
        }
    }
}
