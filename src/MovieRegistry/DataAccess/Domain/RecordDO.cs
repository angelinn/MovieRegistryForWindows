﻿using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    public class RecordDO
    {
        public static Record Create(bool isSeries, DateTime when, Movie movie, WindowsUser user)
        {
            return new Record
            {
                IsSeries = isSeries,
                SeenAt = when,
                Movie = movie,
                User = user
            };
        }
    }
}
