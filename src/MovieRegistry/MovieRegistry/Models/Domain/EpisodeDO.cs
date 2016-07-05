using MovieRegistry.Models.Entities;
using MovieRegistry.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Models.Domain
{
    public class EpisodeDO
    {
        public static Episode FindOrCreate(int season, int serie)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Episode episode = uow.Episodes.Where(e => e.Season == season && e.Serie == serie).FirstOrDefault();
                if (episode == null)
                {
                    episode = new Episode { Season = season, Serie = serie };
                    uow.Episodes.Add(episode);
                    uow.Save();
                }

                return episode;
            }
        }
    }
}
