using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos.Interfaces
{
    public interface IEmployeeRepo : IGenericRepo<Employee>
    {
        Task<IEnumerable<Employee>> GetAllAsync(string? Address);
        Task<IEnumerable<Employee>> GetAllAsync(Expression<Func<Employee,bool>> expression);
    }
}
