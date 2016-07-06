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
        public IEnumerable<Tuple<string, IEnumerable<TheTVDBSharp.Models.Episode>>> GetLatestEpisodes()
        {
            UnitOfWork uow = new UnitOfWork();

            string title = String.Empty;
            IEnumerable<Record> series = uow.Records.Where(r => r.IsSeries == true);
            var se = series.GroupBy(s => s.MovieID).Select((pair) =>
            {
                Movie movie = MovieDO.FindById(pair.Key);
                Episode last = RecordDO.GetLastEpisode(movie.Title);
                title = movie.Title;

                return new TvdbManager(movie.Title).CheckForNewEpisodes(last.Season, last.Serie);
            });

            return se;
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
