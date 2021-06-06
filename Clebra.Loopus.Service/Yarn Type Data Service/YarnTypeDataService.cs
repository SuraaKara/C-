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
    public class YarnTypeDataService : IYarnTypeDataService
    {
        private readonly LoopusDataContext dataContext;

        public YarnTypeDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<YarnType> GetAsync(Expression<Func<YarnType, bool>> query)
         => await dataContext.YarnTypes.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<YarnType>> GetListAsync(Expression<Func<YarnType, bool>> query)
        => await dataContext.YarnTypes.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(YarnType entity)
        {
            try
            {
                dataContext.Entry<YarnType>(entity).State = await dataContext.YarnTypes.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<YarnType>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
