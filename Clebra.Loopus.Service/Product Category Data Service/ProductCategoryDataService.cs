using Clebra.Loopus.DataAccess;
using Clebra.Loopus.Model;
using Clebra.Loopus.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clebra.Loopus.Service
{
    public class ProductCategoryDataService : IProductCategoryDataService
    {

        private readonly LoopusDataContext dataContext;

        public ProductCategoryDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ProductCategory> GetAsync(Expression<Func<ProductCategory, bool>> query)
        => await dataContext.ProductCategories.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<ProductCategory>> GetListAsync(Expression<Func<ProductCategory, bool>> query)
        => await dataContext.ProductCategories.Where(query).ToListAsync();
        public async Task SubmitChangeAsync(ProductCategory entity)
        {
            try
            {
                dataContext.Entry<ProductCategory>(entity).State = await dataContext.ProductCategories.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<ProductCategory>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
