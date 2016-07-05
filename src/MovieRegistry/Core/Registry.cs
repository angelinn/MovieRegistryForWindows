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
using Core.Managers;

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
            using (UnitOfWork uow = new UnitOfWork())
            {
                user = uow.Users.Where(u => u.Name == name).FirstOrDefault();
                if (user == null)
                {
                    user = new WindowsUser { Name = name };
                    uow.Users.Add(user);
                    uow.Save();
                }
            }
        }

        public async Task AddMovie(string title, string seenAt, bool isSeries, int season = 0, int episode = 0)
        {
            if (isSeries && !await EpisodeExists(title, season, episode))
                return;

            // use IMDB here..

            using (UnitOfWork uow = new UnitOfWork())
            {
                // Fix it
                Episode series = null;
                Movie movie = new Movie { };// imdb data.. };
                uow.Movies.Add(movie);

                if (isSeries)
                {
                    series = new Episode { };//tvdb data.. };
                    uow.Episodes.Add(series);
                }

                Record record = new Record
                {
                    Movie = movie,
                    Episode = series,
                    IsSeries = isSeries,
                    SeenAt = DateTime.Now,
                    User = user
                };

                uow.Records.Add(record);
                uow.Save();
            }
        }

        public IEnumerable<MovieDO> GetLatestEpisodes()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //IEnumerable<Record> series = user.Records.Where(r => r.IsSeries == true);

                return new List<MovieDO>
                {
                    new MovieDO(),
                    new MovieDO()
                };
            }
        }

        private async Task<bool> EpisodeExists(string title, int season, int episode)
        {
            TvdbManager tvdb = new TvdbManager(title);
            await tvdb.Load();

            return tvdb.EpisodeExists(season, episode);
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
