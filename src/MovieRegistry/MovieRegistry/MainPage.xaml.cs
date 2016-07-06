using MovieRegistry.Managers;
using MovieRegistry.Models.Domain;
using MovieRegistry.Models.Entities;
using MovieRegistry.Models.Repositories;
using MovieRegistry.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        public ObservableCollection<LatestViewModel> Movies { get; set; }
        public ObservableCollection<SearchResultViewModel> SearchResults { get; set; }

        public string UserName
        {
            get
            {
                return Registry.Instance.User == null ? null : Registry.Instance.User.Name;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;
            NavigationCacheMode = NavigationCacheMode.Enabled;

            Movies = new ObservableCollection<LatestViewModel>();
            SearchResults = new ObservableCollection<SearchResultViewModel>();

            Loaded += MainPage_Loaded;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.Migrate();
                }
            });

            UserDO.CreateOrSetUser("Angie");

            Movies.Clear();
            IEnumerable<LatestViewModel> latest = Registry.Instance.FetchLatest();
            foreach (LatestViewModel m in latest)
                Movies.Add(m);

            prDatabase.IsActive = false;
            prDatabase.Visibility = Visibility.Collapsed;
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
                EpisodeEntity lastEpisode = RecordDO.GetEpisodesByMovie(movie).LastOrDefault();
                if (lastEpisode == null)
                    message = "You have seen that one!";
                else
                    message = String.Format("Last episode you saw was S{0:D2}E{1:D2}", lastEpisode.Season, lastEpisode.Serie);
            }

            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        private void btnNewEpisodes_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewEpisodes));
        }

        private void btnClearData_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.Clear();
            }
            Movies.Clear();
        }
    }
}
