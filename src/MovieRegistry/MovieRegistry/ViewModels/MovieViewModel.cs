using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.ViewModels
{
    public class MovieViewModel : ViewModelBase
    {
        public static MovieViewModel FromDomainModel(MovieDO domain)
        {
            return new MovieViewModel
            {
                Name = domain.Name
            };
        }

        public string Name { get; set; }
    }
}
