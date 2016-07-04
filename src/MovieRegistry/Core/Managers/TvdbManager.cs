using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTVDBSharp;
using TheTVDBSharp.Models;

namespace Core.Managers
{
    public class TvdbManager
    {
        public TvdbManager(string title, string imdbID = null)
        {
            this.title = title;
            this.imdbID = imdbID;

            Load();
        }

        public void CheckForNewEpisodes(int lastSeason, int lastEpisode)
        {

        }
            
        public bool EpisodeExists(int season, int episode)
        {
            return true;
        }

        private void Load()
        {
            manager = new TheTvdbManager(API_KEY);
            IReadOnlyCollection<Series>  allSeries = manager.SearchSeries(title, Language.English).Result;
            
            series = (imdbID == null) ? allSeries.FirstOrDefault() : allSeries.Where(s => s.ImdbId == imdbID).FirstOrDefault();
        }

        public Series Series
        {
            get
            {
                return series;
            }
        }

        private Series series;
        private TheTvdbManager manager;
        private string title;
        private string imdbID;
        private const string API_KEY = "51AEE5CAE610F84E";
    }
}
