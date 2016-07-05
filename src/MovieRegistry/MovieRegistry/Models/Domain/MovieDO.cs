using MovieRegistry.Models.Entities;
using MovieRegistry.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Models.Domain
{
    public class MovieDO
    {
        public static Movie FindOrCreate(string imdbId, string title, string year)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Movie movie = uow.Movies.Where(m => m.ImdbID == imdbId).FirstOrDefault();
                if (movie == null)
                {
                    movie = new Movie { ImdbID = imdbId, Title = title, Year = Int32.Parse(year) };
                    uow.Movies.Add(movie);
                    uow.Save();
                }

                return movie;
            }
        }

        public string Name
        {
            get
            {
                return movie.Title;
            }
        }

        private Movie movie;
    }
}
