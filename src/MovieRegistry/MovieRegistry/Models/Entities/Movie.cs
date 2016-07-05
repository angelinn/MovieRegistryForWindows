using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Models.Entities
{
    public class Movie : BaseEntity
    {
        public Movie()
        {
            Records = new HashSet<Record>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string ImdbID { get; set; }

        public virtual ICollection<Record> Records { get; set; }

        public int GetId()
        {
            return ID;
        }
    }
}
