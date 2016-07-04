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
        public TvdbManager(string title)
        {
            this.title = title;

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
            allSeries = manager.SearchSeries(title, Language.English).Result;
        }

        private IReadOnlyCollection<Series> allSeries;
        private TheTvdbManager manager;
        private string title;
        private const string API_KEY = "51AEE5CAE610F84E";
    }
}
