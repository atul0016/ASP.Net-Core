using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Services
{
    public class EmployeeService : IService<Employee, int>
    {
        private readonly MyAppDbContext _ctx;

        /// <summary>
        /// Inject the DbContext in the ctor
        /// </summary>
        public EmployeeService(MyAppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Employee> CreateAsync(Employee entity)
        {
            var res = await _ctx.Emplopyees.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _ctx.Emplopyees.FindAsync(id);
            if (res != null)
            {
                _ctx.Emplopyees.Remove(res);
                await _ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Employee>> GetAsync()
        {
            var res = await _ctx.Emplopyees.ToListAsync();
            return res;
        }

        public async Task<Employee> GetAsync(int id)
        {
            var res = await _ctx.Emplopyees.FindAsync(id);
            return res;
        }

        public async Task<Employee> UpdateAsync(int id, Employee entity)
        {
            var res = await _ctx.Emplopyees.FindAsync(id);
            if (res != null)
            {
                res.EmpNo = entity.EmpNo;
                res.EmpName = entity.EmpName;
                res.Designation = entity.Designation;
                res.Salary = entity.Salary;
                res.DeptRowId = entity.DeptRowId;
                await _ctx.SaveChangesAsync();
            }
            return res;
        }
    }
}
