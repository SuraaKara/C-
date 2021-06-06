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
    public class NeighborhoodDataService : INeighborhoodDataService
    {
        private readonly LoopusDataContext dataContext;

        public NeighborhoodDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<Neighborhood> GetAsync(Expression<Func<Neighborhood, bool>> query)
            => await dataContext.Neighborhoods.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<Neighborhood>> GetListAsync(Expression<Func<Neighborhood, bool>> query)
            => await dataContext.Neighborhoods.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(Neighborhood entity)
        {
            try
            {

                dataContext.Entry<Neighborhood>(entity).State = await dataContext.Neighborhoods.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<Neighborhood>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
