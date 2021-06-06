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
    public class DiscountDefinitionDataService : IDiscountDefinitionDataService
    {
        private readonly LoopusDataContext dataContext;

        public DiscountDefinitionDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<DiscountDefinition> GetAsync(Expression<Func<DiscountDefinition, bool>> query)
            => await dataContext.DiscountDefinitions.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<DiscountDefinition>> GetListAsync(Expression<Func<DiscountDefinition, bool>> query)
            => await dataContext.DiscountDefinitions.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(DiscountDefinition entity)
        {
            try
            {

                dataContext.Entry<DiscountDefinition>(entity).State = await dataContext.DiscountDefinitions.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<DiscountDefinition>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

