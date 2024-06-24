using DAL.Models;
using System.Linq.Expressions;

namespace BLL.Repos.Interfaces
{
    public interface IDepartmentRepo : IGenericRepo<Department>
    {
        Task<IEnumerable<Department>> GetAllAsync(Expression<Func<Department, bool>> expression);
    }
}
