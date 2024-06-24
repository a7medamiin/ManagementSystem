using BLL.Repos.Classes;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Repos.Interfaces
{
    public class DepartmentRepo : GenericRepo<Department> , IDepartmentRepo 
    {
        private readonly CompanyContext _dbContext;

        public DepartmentRepo(CompanyContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Department>> GetAllAsync(Expression<Func<Department, bool>> expression)
        {
            return await _context.Departments.Where(expression).ToListAsync();
        }
    }
}
