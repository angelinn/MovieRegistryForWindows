using Core;
using Core.Managers;
using DataAccess;
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;

            Movies = new ObservableCollection<MovieViewModel>();
            SearchResults = new ObservableCollection<SearchResultViewModel>();

            Loaded += MainPage_Loaded;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            TvdbManager manager = new TvdbManager("Friends");

            try
            {
                await manager.Load();

                Movies.Add(new MovieViewModel());
                Movies.Add(new MovieViewModel());

                Movies[0].Name = manager.Series.Title;
                Movies[1].Name = manager.Series.Description;
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

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Options));
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

            if (rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }
    }
}
