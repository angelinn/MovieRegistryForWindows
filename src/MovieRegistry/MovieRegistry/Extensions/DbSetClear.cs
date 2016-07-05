using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Extensions
{
    public static class DbSetClear
    {
        public static void Clear<T>(this DbSet<T> set) where T : class
        {
            set.RemoveRange(set);
        }
    }
}
