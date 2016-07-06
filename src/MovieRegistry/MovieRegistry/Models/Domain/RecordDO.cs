using MovieRegistry.Models.Entities;
using MovieRegistry.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Models.Domain
{
    public class RecordDO
    {
        public static bool TryCreate(bool isSeries, DateTime when, Movie movie, WindowsUser user, Episode episode)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Record record = uow.Records.Where(r => r.IsSeries == false && r.MovieID == movie.ID).FirstOrDefault();
                if (record == null)
                {
                    record = new Record
                    {
                        IsSeries = isSeries,
                        SeenAt = when,
                        MovieID = movie.ID,
                        UserID = user.ID
                    };

                    if (episode != null)
                        record.EpisodeID = episode.ID;

                    uow.Records.Add(record);
                    uow.Save();

                    return true;
                }
                return false;
            }
        }

        public static IEnumerable<Episode> GetEpisodesByMovie(Movie movie)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Record record = uow.Records.Where(r => r.MovieID == movie.ID).First();
                return uow.Episodes.Where(e => e.ID == record.EpisodeID).ToList();
            }
        }

        public static Episode GetLastEpisode(string title)
        {
            Movie movie = MovieDO.FindByTitle(title);
            return GetEpisodesByMovie(movie).OrderBy(e => e.ID).LastOrDefault();
        }

        public static Movie GetMovie(Record record)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.Movies.Where(m => m.ID == record.MovieID).FirstOrDefault();
            }
        }

        public static Episode GetEpisode(Record record)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.Episodes.Where(e => e.ID == record.EpisodeID).FirstOrDefault();
            }
        }
    }
}
