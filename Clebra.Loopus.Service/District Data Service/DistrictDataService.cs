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
    public class DistrictDataService : IDistrictDataService
    {
        private readonly LoopusDataContext dataContext;

        public DistrictDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<District> GetAsync(Expression<Func<District, bool>> query)
            => await dataContext.Districts.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<District>> GetListAsync(Expression<Func<District, bool>> query)
            => await dataContext.Districts.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(District entity)
        {
            try
            {

                dataContext.Entry<District>(entity).State = await dataContext.Districts.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<District>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
