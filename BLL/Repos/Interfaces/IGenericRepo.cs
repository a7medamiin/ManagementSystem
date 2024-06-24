﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetALLAsync();

       Task< T?> GetAsync(int id);

        Task AddAsync(T entity);
        
        void Update(T entity);
        
        void Delete(T entity);

    }
}
