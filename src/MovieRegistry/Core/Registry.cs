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
                new MovieDO(),
                new MovieDO()
            };
        }

        private Registry()
        { }

        private static Registry instance;
    }
}
