using BLL.Repos.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos.Classes
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(CompanyContext context) : base(context)
        {
        }

        public Task<IEnumerable<Employee>> GetAllAsync(string? Address)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(Expression<Func<Employee, bool>> expression)
        {
            return await _context.Employees.Where(expression).ToListAsync();
        }

        public Task<IEnumerable<Employee>> GetAllAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
