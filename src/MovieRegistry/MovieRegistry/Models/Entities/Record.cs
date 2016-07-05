using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Models.Entities
{
    public class Record : BaseEntity
    {
        public int ID { get; set; }
        public bool IsSeries { get; set; }
        public DateTime? SeenAt { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Episode Episode { get; set; }
        public virtual WindowsUser User { get; set; }

        public int GetId()
        {
            return ID;
        }
    }
}
