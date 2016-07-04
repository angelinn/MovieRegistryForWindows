using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Registry
    {

        public static Registry GetInstance()
        {
            if (instance == null)
                instance = new Registry();

            return instance;
        }

        public IEnumerable<MovieDO> GetLatestEpisodes()
        {
            return new List<MovieDO>
            {
                new MovieDO { Movie = new DataAccess.Models.Movie { ID = 0, Name = "American Pie" } },
                new MovieDO { Movie = new DataAccess.Models.Movie { ID = 1, Name = "Game of Thrones" } }
            };
        }

        private Registry()
        { }

        private static Registry instance;
    }
}
