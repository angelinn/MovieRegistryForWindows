using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Movie : BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int GetId()
        {
            return ID;
        }
    }
}
