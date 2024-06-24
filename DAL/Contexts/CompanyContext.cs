﻿using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contexts
{
    public class CompanyContext : IdentityDbContext<AppUser>
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//    optionsBuilder.UseSqlServer("server=.; database=Companyyy; trusted_connection=true");

		//}
	}
}
