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
        public static bool TryCreate(bool isSeries, DateTime when, Movie movie, WindowsUser user)
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

                    uow.Records.Add(record);
                    uow.Save();

                    return true;
                }
                return false;
            }
        }
    }
}
