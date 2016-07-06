using MovieRegistry.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTVDBSharp;
using MovieRegistry.Models.Repositories;
using MovieRegistry.Models.Domain;
using MovieRegistry.Managers;
using MovieRegistry.Extensions;
using MovieRegistry.ViewModels;

namespace MovieRegistry
{
    public class Registry
    {
        public async Task<IEnumerable<Tuple<string, IEnumerable<TheTVDBSharp.Models.Episode>>>> GetLatestEpisodes()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                IEnumerable<Record> series = uow.Records.Where(r => r.IsSeries == true);

                var grouped = series.GroupBy(s => s.MovieID);

                var res = new List<Tuple<string, IEnumerable<TheTVDBSharp.Models.Episode>>>();
                foreach (IGrouping<int, Record> group in grouped)
                {
                    Movie movie = MovieDO.FindById(group.Key);
                    Episode last = RecordDO.GetLastEpisode(movie.Title);

                    TvdbManager tvdb = new TvdbManager(movie.Title);
                    await tvdb.Load();

                    res.Add(tvdb.CheckForNewEpisodes(movie.Title, last.Season, last.Serie));
                }

                return res;
            }
        }

        private async Task<bool> EpisodeExists(string title, int season, int episode)
        {
            TvdbManager tvdb = new TvdbManager(title);
            await tvdb.Load();

            return tvdb.EpisodeExists(season, episode);
        }

        public IEnumerable<LatestViewModel> FetchLatest()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                IEnumerable<Record> records = uow.Records.All().OrderByDescending(r => r.ID).Take(5);
                IEnumerable<Movie> movies = records.Select(r => RecordDO.GetMovie(r));

                foreach (Record rec in records)
                {
                    Movie m = RecordDO.GetMovie(rec);
                    Episode e = RecordDO.GetEpisode(rec);

                    yield return new LatestViewModel
                    {
                        Title = m.Title,
                        Year = m.Year,
                        Season = e == null ? 0 : e.Season,
                        Serie = e == null ? 0 : e.Serie
                    };
                }
            }
        }

        private Registry()
        { }
        
        public WindowsUser User { get; set; }

        private static Registry instance;
        public static Registry Instance
        {
            get
            {
                if (instance == null)
                    instance = new Registry();

                return instance;
            }
        }
    }
}
