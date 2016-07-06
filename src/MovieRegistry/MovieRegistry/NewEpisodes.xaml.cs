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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieRegistry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewEpisodes : Page
    {
        public ObservableCollection<NewEpisodeViewModel> Episodes { get; set; }

        public NewEpisodes()
        {
            this.InitializeComponent();

            Episodes = new ObservableCollection<NewEpisodeViewModel>();
            DataContext = this;

            Loaded += NewEpisodes_Loaded;
        }

        private async void NewEpisodes_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var newEpisodes = await Registry.Instance.GetLatestEpisodes();
                foreach (var tuple in newEpisodes)
                {
                    foreach (Episode episode in tuple.Item2)
                        Episodes.Add(new NewEpisodeViewModel { ShowTitle = tuple.Item1, Episode = episode });
                }

                txtTitle.Text = Episodes.Count == 0 ? "No new episodes found." : "Found some new episodes!!!";
            }
            catch(Exception ex)
            {
                Episodes.Add(new NewEpisodeViewModel { ShowTitle = ex.Message });
            }
            finally
            {
                prNewEpisodes.IsActive = false;
                prNewEpisodes.Visibility = Visibility.Collapsed;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
    }
}
