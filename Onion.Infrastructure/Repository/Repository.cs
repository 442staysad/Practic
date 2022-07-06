﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Onion.Core.Entities;
using Onion.Infrastructure.Data;
using Onion.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onion.Infrastructure.Repository
{
    public class Repository<T>:IRepository<T> where T:BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        public IQueryable<T> GetAll()=> _context.Set<T>().AsQueryable();
        

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        public async Task<IEnumerable<T>> GetAllAsync()=> await _context.Set<T>().ToListAsync();
        
        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        public T Find(Expression<Func<T, bool>> expression)=> _context.Set<T>().SingleOrDefault(expression);

        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)=> await _context.Set<T>().SingleOrDefaultAsync(expression);

        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        public IQueryable<T> FindAll(Func<IQueryable<T>, IQueryable<T>> func) => func(_context.Set<T>().AsNoTracking());

        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression)=> await _context.Set<T>().Where(expression).ToListAsync();

        /// <summary>
        /// Adds a single object to the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Updates a single object and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                return null;

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        public async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a collection of objects into the database and commits the chфnges
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entityList)
        {
            _context.Set<T>().AddRange(entityList);
            await _context.SaveChangesAsync();
            return entityList;
        }

        /// <summary>
        /// Deletes a collection of objects into the database and commits the chфnges
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        public async Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entityList)
        {
            _context.Set<T>().RemoveRange(entityList);
            await _context.SaveChangesAsync();
            return entityList;
        }

        /// <summary>
        /// Updates a collection of objects into the database and commits the chфnges
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entityList)
        {
            _context.Set<T>().UpdateRange(entityList);
            await _context.SaveChangesAsync();
            return entityList;
        }
    }
}
