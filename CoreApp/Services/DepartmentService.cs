using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Services
{
    public class DepartmentService : IService<Department, int>
    {
        private readonly MyAppDbContext _ctx;

        /// <summary>
        /// Inject the DbContext in the ctor
        /// </summary>
        public DepartmentService(MyAppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Department> CreateAsync(Department entity)
        {
            var res = await _ctx.Departments.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _ctx.Departments.FindAsync(id);
            if (res != null)
            {
                _ctx.Departments.Remove(res);
                await _ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Department>> GetAsync()
        {
            var res = await _ctx.Departments.ToListAsync();
            return res;
        }

        public async Task<Department> GetAsync(int id)
        {
            var res = await _ctx.Departments.FindAsync(id);
            return res;
        }

        public async Task<Department> UpdateAsync(int id, Department entity)
        {
            var res = await _ctx.Departments.FindAsync(id);
            if (res != null)
            {
                res.DeptNo = entity.DeptNo;
                res.DeptName = entity.DeptName;
                res.Location = entity.Location;
                res.Capacity = entity.Capacity;
                await _ctx.SaveChangesAsync();
            }
            return res;
        }
    }
}
