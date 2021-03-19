using LibreriaAdmin.Interfaces;
using LibreriaAdmin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Repository
{
    public class LibreriaRepository : IRepository
    {
        private readonly DbContext _dbContext;

        public LibreriaRepository(LibreriaDatabaseContext libreriaDatabaseContext)
        {
            _dbContext = libreriaDatabaseContext;
        }

        public void Create<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}
