using Clebra.Loopus.DataAccess;
using Clebra.Loopus.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clebra.Loopus.Service
{
    public class ProductSizeDataService : IProductSizeDataService
    {
        private readonly LoopusDataContext dataContext;

        public ProductSizeDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ProductSize> GetAsync(Expression<Func<ProductSize, bool>> query)
         => await dataContext.ProductSizes.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<ProductSize>> GetListAsync(Expression<Func<ProductSize, bool>> query)
        => await dataContext.ProductSizes.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(ProductSize entity)
        {
            try
            {
                dataContext.Entry<ProductSize>(entity).State = await dataContext.ProductSizes.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<ProductSize>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
