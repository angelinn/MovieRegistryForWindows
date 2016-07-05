using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Models.Entities
{
    public class Episode : BaseEntity
    {
        public Episode()
        {
            Records = new HashSet<Record>();
        }

        public int ID { get; set; }
        public int Season { get; set; }
        public int Serie { get; set; }

        public virtual ICollection<Record> Records { get; set; }

        public int GetId()
        {
            return ID;
        }
    }
}
