using MovieRegistry.Managers;
using MovieRegistry.Models.Domain;
using MovieRegistry.Models.Entities;
using MovieRegistry.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TheTVDBSharp.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using EpisodeEntity = MovieRegistry.Models.Entities.Episode;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MovieRegistry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<MovieViewModel> Movies { get; set; }
        public ObservableCollection<SearchResultViewModel> SearchResults { get; set; }
        public string UserName
        {
            get
            {
                return UserDO.GetUser().Name;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;
            NavigationCacheMode = NavigationCacheMode.Enabled;

            Movies = new ObservableCollection<MovieViewModel>();
            SearchResults = new ObservableCollection<SearchResultViewModel>();

            UserDO.CreateOrSetUser("Angie");

            Loaded += MainPage_Loaded;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            TvdbManager manager = new TvdbManager("Friends");

            try
            {
                //await manager.Load();

                Movies.Add(new MovieViewModel());
                Movies[0].Name = "No new episodes found.";
            }
            catch(ServerNotAvailableException)
            {
                Movies[0].Name = "Connection not available.";
            }
            finally
            {
                prLatest.IsActive = false;
            }

        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                prSearch.IsActive = true;
                SearchResults.Clear();

                ImdbManager imdb = new ImdbManager();
                OMDbSharp.Search[] searchList = (await imdb.GetByTitle(txtEntryName.Text)).Search;

                foreach (var search in searchList)
                    SearchResults.Add(new SearchResultViewModel(search));
            }
            catch (Exception)
            {
                SearchResults.Add(new SearchResultViewModel { Title = "Connection not available" });
            }
            finally
            {
                prSearch.IsActive = false;
                prSearch.Visibility = Visibility.Collapsed;
            }
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            e.Handled = true;
            rootFrame.GoBack();
        }

        private async void lvSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchResultViewModel selected = (SearchResultViewModel)e.AddedItems.FirstOrDefault();
            if (selected != null)
            {
                var detailed = await new ImdbManager().GetById(selected.ImdbID);
                Frame.Navigate(typeof(SearchResult), new SearchResultViewModel(detailed));
            }
        }

        private void txtEntryName_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                btnSearch_Click(this, null);
                InputPane.GetForCurrentView().TryHide();
            }

        }

        private async void btnDidISee_Click(object sender, RoutedEventArgs e)
        {
            string title = txtDidISee.Text;
            Movie movie = MovieDO.FindByTitle(title);

            string message = String.Empty;
            if (movie == null)
            {
                message = String.Format("You haven't seen {0}", title);
            }
            else
            {
                EpisodeEntity episode = RecordDO.GetEpisodeByMovie(movie);
                if (episode == null)
                    message = "You have seen that one!";
                else
                    message = String.Format("Last episode you saw was S{0}E{1}", episode.Season, episode.Serie);
            }

            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
