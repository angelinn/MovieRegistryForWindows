using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    public class MovieDO
    {
        public MovieDO()
        {
            movie = new Movie { ID = 0, Title = "Kewl Movie" };
        }

        public string Name
        {
            get
            {
                return movie.Title;
            }
        }

        private Movie movie;
    }
}
