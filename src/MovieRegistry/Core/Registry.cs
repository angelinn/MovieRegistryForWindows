using DataAccess.Domain;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTVDBSharp;
using Core.Extensions;

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
            using (UnitOfWork uow = new UnitOfWork())
            {
                IEnumerable<Record> series = user.Records.Where(r => r.IsSeries == true);

                return new List<MovieDO>
                {
                    new MovieDO(),
                    new MovieDO()
                };
            }
        }

        private Dictionary<string, IEnumerable<Record>> FetchLatest()
        {
            return new Dictionary<string, IEnumerable<Record>>
            {
                { "movies", user.Records.Where(r => r.IsSeries == false).TakeLast(5) },
                { "series", user.Records.Where(r => r.IsSeries == true).TakeLast(5)  }
            };
        }

        private Registry()
        { }

        private WindowsUser user;
        private static Registry instance;
    }
}
