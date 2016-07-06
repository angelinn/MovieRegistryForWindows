using MovieRegistry.Models.Domain;
using MovieRegistry.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.ViewModels
{
    public class LatestViewModel : NotifyingViewModel
    {
        public override string ToString()
        {
            if (season == 0 && serie == 0)
                return String.Format("{0} ({1})", title, year);

            return String.Format("{0} - S{1:D2}E{2:D2}", title, season, serie);
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != null)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private int year;
        public int Year
        {
            get
            { 
                return year;
            }
            set
            {
                year = value;
                OnPropertyChanged("Year");
            }
        }

        private int season;
        public int Season
        {
            get
            {
                return season;
            }
            set
            {
                season = value;
                OnPropertyChanged("Season");
            }
        }

        private int serie;
        public int Serie
        {
            get
            {
                return serie;
            }
            set
            {
                serie = value;
                OnPropertyChanged("Serie");
            }
        }
    }
}
