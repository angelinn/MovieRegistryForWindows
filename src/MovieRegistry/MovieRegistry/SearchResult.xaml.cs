using MovieRegistry.Managers;
using MovieRegistry.Models.Domain;
using MovieRegistry.Models.Entities;
using MovieRegistry.Models.Repositories;
using MovieRegistry.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieRegistry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchResult : Page
    {
        public SearchResultViewModel Search { get; set; }

        public SearchResult()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Search = (SearchResultViewModel)e.Parameter;

            DataContext = Search;

            if (Search.Type != "movie")
            {
                tvdb = new TvdbManager(Search.Title);
                await tvdb.Load();
            }
        }

        private async void btnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            Episode episode = null;
            bool isSeries = Search.Type != "movie";
            if (isSeries)
            {
                int season = Int32.Parse(txtSeason.Text);
                int series = Int32.Parse(txtEpisode.Text);

                if (!tvdb.EpisodeExists(season, series))
                {
                    await new MessageDialog("That episode does not exist.").ShowAsync();
                    return;
                }

                episode = EpisodeDO.FindOrCreate(season, series);
            }

            Movie movie = MovieDO.FindOrCreate(Search.ImdbID, Search.Title, Search.Year);

            bool created = RecordDO.TryCreate(isSeries, DateTime.Now, movie, Registry.Instance.User, episode);

            string message = created ? String.Format("{0} successfully added at {1}.", Search.Title, DateTime.Now)
                                     : String.Format("{0} already exists!", Search.Title);
            
            await new MessageDialog(message).ShowAsync();
        }

        private TvdbManager tvdb;
    }
}
