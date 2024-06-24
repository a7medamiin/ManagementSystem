using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IEmployeeRepo Employees { get; }
        IDepartmentRepo Departments { get; }

       Task<int> CompleteAsync();
    }
}
