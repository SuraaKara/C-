using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Clebra.Loopus.DataAccess;
using Clebra.Loopus.Model;
using Microsoft.EntityFrameworkCore;

namespace Clebra.Loopus.Service
{
    public class ProductDataService : IProductDataService
    {
        private readonly LoopusDataContext dataContext;
        
        public ProductDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task SubmitChangeAsync(Product entity)
        {
            try
            {
                dataContext.Entry<Product>(entity).State = await dataContext.Products.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;
            
                await dataContext.SaveChangesAsync();
           
                dataContext.Entry<Product>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> query)
            => await dataContext.Products.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<Product>> GetListAsync(Expression<Func<Product, bool>> query)
            => await dataContext.Products.Where(query).ToListAsync();
    }
}