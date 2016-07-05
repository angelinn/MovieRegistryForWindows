﻿using DataAccess.Domain;
using DataAccess.Entities;
using DataAccess.Repositories;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Search = (SearchResultViewModel)e.Parameter;

            DataContext = Search;
        }

        private async void btnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Movie movie = MovieDO.FindOrCreate(Search.ImdbID, Search.Title, Search.Year);
                Record record = RecordDO.Create(false, DateTime.Now, movie, UserDO.GetUser());

                uow.Movies.Add(movie);
                uow.Records.Add(record);
                uow.Save();
            }

            MessageDialog dialog = new MessageDialog(String.Format("{0} successfully added at {1}.", Search.Title, DateTime.Now));
            await dialog.ShowAsync();
        }
    }
}
