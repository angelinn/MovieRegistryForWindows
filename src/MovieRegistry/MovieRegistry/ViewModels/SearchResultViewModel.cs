using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.ViewModels
{
    public class SearchResultViewModel : NotifyingViewModel
    {
        public SearchResultViewModel()
        {   }

        public SearchResultViewModel(OMDbSharp.Item omdbItem)
        {
            Country = omdbItem.Country;
            Genre = omdbItem.Genre;
            Language = omdbItem.Language;
            Title = omdbItem.Title;
            ImdbID = omdbItem.imdbID;
            ImdbRating = omdbItem.imdbRating;
            Year = omdbItem.Year;
            Poster = omdbItem.Poster;
        }

        public SearchResultViewModel(OMDbSharp.Search omdbSearch)
        {
            Year = omdbSearch.Year;
            Title = omdbSearch.Title;
            ImdbID = omdbSearch.imdbID;
            Type = omdbSearch.Type;
        }


        private string country;
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                if (value != null)
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        private string genre;
        public string Genre
        {
            get
            {
                return genre;
            }
            set
            {
                if (value != null)
                {
                    genre = value;
                    OnPropertyChanged("Genre");
                }
            }
        }

        private string language;
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                if (value != null)
                {
                    language = value;
                    OnPropertyChanged("Language");
                }
            }
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

        private string imdbID;
        public string ImdbID
        {
            get
            {
                return imdbID;
            }
            set
            {
                if (value != null)
                {
                    imdbID = value;
                    OnPropertyChanged("ImdbID");
                }
            }
        }

        private string imdbRating;
        public string ImdbRating
        {
            get
            {
                return imdbRating;
            }
            set
            {
                if (value != null)
                {
                    imdbRating = value;
                    OnPropertyChanged("ImdbRating");
                }
            }
        }

        private string year;
        public string Year
        {
            get
            {
                return year;
            }
            set
            {
                if (value != null)
                {
                    year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        private string poster;
        public string Poster
        {
            get
            {
                return poster;
            }
            set
            {
                if (value != null)
                {
                    poster = value;
                    OnPropertyChanged("Poster");
                }
            }
        }

        private string type;
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value != null)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
    }
}
