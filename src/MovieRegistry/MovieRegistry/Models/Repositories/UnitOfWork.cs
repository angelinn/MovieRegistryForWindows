using Microsoft.EntityFrameworkCore;
using MovieRegistry.Extensions;
using MovieRegistry.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Models.Repositories
{
    public class UnitOfWork : IDisposable
    {
        public UnitOfWork(MovieRegistryContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public UnitOfWork() : this(new MovieRegistryContext())
        { }

        public void Migrate()
        {
            try
            {
                context.Database.Migrate();
            }
            catch (InvalidOperationException)
            {

            }
        }

        public void Clear()
        {
            context.Movies.Clear();
            context.Episodes.Clear();
            context.Records.Clear();
            context.Users.Clear();

            context.SaveChanges();
        }

        public IGenericRepository<Movie> Movies
        {
            get
            {
                return GetRepository<Movie>();
            }
        }

        public IGenericRepository<WindowsUser> Users
        {
            get
            {
                return GetRepository<WindowsUser>();
            }
        }

        public IGenericRepository<Record> Records
        {
            get
            {
                return GetRepository<Record>();
            }
        }

        public IGenericRepository<Episode> Episodes
        {
            get
            {
                return GetRepository<Episode>();
            }
        }

        private IGenericRepository<T> GetRepository<T>() where T : class, BaseEntity
        {
            if (!repositories.ContainsKey(typeof(T)))
            {
                Type type = typeof(GenericRepository<T>);
                repositories.Add(typeof(T), Activator.CreateInstance(type, context));
            }
            return (IGenericRepository<T>)repositories[typeof(T)];
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        private MovieRegistryContext context;
        private Dictionary<Type, object> repositories;
    }
}
