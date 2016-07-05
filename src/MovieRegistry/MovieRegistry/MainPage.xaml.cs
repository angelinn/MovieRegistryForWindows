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

            Movies = new ObservableCollection<MovieViewModel>(Registry.GetInstance().GetLatestEpisodes().Select(m => MovieViewModel.FromDomainModel(m)));
            SearchResults = new ObservableCollection<SearchResultViewModel>();

            Loaded += MainPage_Loaded;
        }
        

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            TvdbManager manager = new TvdbManager("Friends");

            try
            {
                await manager.Load();
                Movies[0].Name = manager.Series.Title;
                Movies[1].Name = manager.Series.Description;
            }
            catch(ServerNotAvailableException)
            {
                Movies[0].Name = "Connection not available.";
                return;
            }
            finally
            {
                lvLatest.Visibility = Visibility.Visible;
                prLatest.IsActive = false;
            }

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Options));
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ImdbManager imdb = new ImdbManager();
            OMDbSharp.Search[] searchList = (await imdb.GetByTitle(txtEntryName.Text)).Search;
            
            foreach (var search in searchList)
                SearchResults.Add(new SearchResultViewModel(search));
        }
    }
}
