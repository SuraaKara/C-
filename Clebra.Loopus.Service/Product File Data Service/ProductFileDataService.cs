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
    public class ProductFileDataService : IProductFileDataService
    {
        private readonly LoopusDataContext dataContext;

        public ProductFileDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ProductFile> GetAsync(Expression<Func<ProductFile, bool>> query)
         => await dataContext.ProductFiles.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<ProductFile>> GetListAsync(Expression<Func<ProductFile, bool>> query)
        => await dataContext.ProductFiles.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(ProductFile entity)
        {
            try
            {
                dataContext.Entry<ProductFile>(entity).State = await dataContext.ProductFiles.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<ProductFile>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
