using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTVDBSharp;

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

        public void RegisterUser(string name)
        {

        }

        public void AddMovie(string id, string seenAt, bool isSeries, int season = 0, int episode = 0)
        {

        }

        public IEnumerable<MovieDO> GetLatestEpisodes()
        {
            return new List<MovieDO>
            {
                new MovieDO(),
                new MovieDO()
            };
        }

        private Registry()
        { }

        private static Registry instance;
    }
}
