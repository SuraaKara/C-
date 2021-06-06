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
    public class ProductStockDataService : IProductStockDataService
    {
        private readonly LoopusDataContext dataContext;

        public ProductStockDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ProductStock> GetAsync(Expression<Func<ProductStock, bool>> query)
         => await dataContext.ProductStocks.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<ProductStock>> GetListAsync(Expression<Func<ProductStock, bool>> query)
        => await dataContext.ProductStocks.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(ProductStock entity)
        {
            try
            {
                dataContext.Entry<ProductStock>(entity).State = await dataContext.ProductStocks.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<ProductStock>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



    }
}
