using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.DAL.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        public readonly DataContext dataContext;
        public GenericRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<List<T>> GetAllAsync()
        {
            var data = await dataContext.Set<T>().ToListAsync();
            return data;
        }
        public async Task<T> GetAsync(Guid id)
        {
            var data = await dataContext.Set<T>().FindAsync(id);
            return data;
        }
        public async Task<T> PostAsync(T entity)
        {
            var add_Data = await dataContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            var update_Data = dataContext.Set<T>().Update(entity);
            return entity;
        }
        public async Task<T> DeleteAsync(Guid id)
        {
            var find_Data = await dataContext.Set<T>().FindAsync(id);
            var delete_Data = dataContext.Set<T>().Remove(find_Data);
            return find_Data;
        }
        public async Task<List<T>> DeleteAllAsync()
        {
            var data = await dataContext.Set<T>().ToListAsync();
            dataContext.RemoveRange(data);
            return data;
        }
        

    }
}



  
