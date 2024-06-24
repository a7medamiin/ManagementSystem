using BLL.Repos.Interfaces;
using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IDepartmentRepo> departments;
        private readonly Lazy<IEmployeeRepo> employees;
        private readonly CompanyContext _Context;

        public UnitOfWork(CompanyContext context)
        {
            departments = new Lazy<IDepartmentRepo>( new DepartmentRepo(context));
            employees = new Lazy<IEmployeeRepo>(new EmployeeRepo(context));
            _Context=context;
        }

        public IEmployeeRepo Employees => employees.Value;

        public IDepartmentRepo Departments => departments.Value;

        public async Task<int> CompleteAsync() => await _Context.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _Context.DisposeAsync();
    }
}
