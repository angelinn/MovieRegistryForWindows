using MovieRegistry.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTVDBSharp;
using TheTVDBSharp.Models;

namespace MovieRegistry.Managers
{
    public class TvdbManager
    {
        public TvdbManager(string title, string imdbID = null)
        {
            this.title = title;
            this.imdbID = imdbID;
        }

        public Tuple<string, IEnumerable<Episode>> CheckForNewEpisodes(int lastSeason, int lastEpisode)
        {
            if (HasFinished(lastSeason, lastEpisode))
                return null;

            Episode current = series.Episodes.Where(e => e.SeasonNumber == lastSeason && e.Number == lastEpisode).FirstOrDefault();
            if (current == null)
                return null;

            return new Tuple<string, IEnumerable<Episode>>(String.Empty, series.Episodes.Where(e =>
            {
                return e.FirstAired != null && e.FirstAired > current.FirstAired && e.FirstAired < DateTime.Now;
            }));
        }
            
        public bool EpisodeExists(int season, int episode)
        {
            return series.Episodes.Any(e => e.SeasonNumber == season && e.Number == episode);
        }

        public async Task Load()
        {
            if (!loaded)
            {
                manager = new TheTvdbManager(API_KEY);
                IReadOnlyCollection<Series> allSeries = await manager.SearchSeries(title, Language.English);

                series = (imdbID == null) ? allSeries.FirstOrDefault() : allSeries.Where(s => s.ImdbId == imdbID).FirstOrDefault();
                series = await manager.GetSeries(series.Id, Language.English);
                loaded = true;
            }
        }

        public Series Series
        {
            get
            {
                return series;
            }
        }

        private bool HasFinished(int season, int episode)
        {
            return series.Episodes.OrderBy(e => e.SeasonNumber).Last().Number == episode;
        }

        private bool loaded;
        private Series series;
        private TheTvdbManager manager;
        private string title;
        private string imdbID;
        private const string API_KEY = "51AEE5CAE610F84E";
    }
}
