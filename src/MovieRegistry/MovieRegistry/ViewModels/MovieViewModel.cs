﻿using MovieRegistry.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.ViewModels
{
    public class MovieViewModel : NotifyingViewModel
    {
        public static MovieViewModel FromDomainModel(MovieDO domain)
        {
            return new MovieViewModel
            {
                Name = domain.Name
            };
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != null)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
    }
}
