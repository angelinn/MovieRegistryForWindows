using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTVDBSharp.Models;

namespace MovieRegistry.ViewModels
{
    public class NewEpisodeViewModel : NotifyingViewModel
    {
        public override string ToString()
        {
            if (episode != null)
                return String.Format("{0} - S{1:D2}E{2:D2}", showTitle, episode.SeasonNumber, episode.Number);

            return showTitle;
        }

        private string showTitle;
        public string ShowTitle
        {
            get
            {
                return showTitle;
            }
            set
            {
                if (value != null)
                {
                    showTitle = value;
                    OnPropertyChanged("ShowTitle");
                }
            }
        }

        private Episode episode;
        public Episode Episode
        {
            get
            {
                return episode;
            }
            set
            {
                if (value != null)
                {
                    episode = value;
                    OnPropertyChanged("Episode");
                }
            }
        }
    }
}
