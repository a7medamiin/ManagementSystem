using BLL.Repos.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos.Classes
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly CompanyContext _context;

        public GenericRepo(CompanyContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
          await  _context.Set<T>().AddAsync(entity);
            
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            
        }

        public async Task<T?> GetAsync(int id)
        {
            
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task< IEnumerable<T>> GetALLAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(e => e.Department).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            
        }

    }
}
