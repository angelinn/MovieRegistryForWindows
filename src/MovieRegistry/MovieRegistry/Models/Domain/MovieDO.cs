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
                    movie = new Movie { ImdbID = imdbId, Title = title };

                    int y;
                    if (Int32.TryParse(year, out y))
                        movie.Year = y;

                    uow.Movies.Add(movie);
                    uow.Save();
                }

                return movie;
            }
        }

        public static Movie FindByTitle(string title)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.Movies.Where(m => m.Title == title).FirstOrDefault();
            }
        }

        public static Movie FindById(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.Movies.FindById(id);
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
