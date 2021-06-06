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
    public class BigTextDataService : IBigTextDataService
    {
        private readonly LoopusDataContext dataContext;

        public BigTextDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<BigText> GetAsync(Expression<Func<BigText, bool>> query)
            => await dataContext.BigTexts.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<BigText>> GetListAsync(Expression<Func<BigText, bool>> query)
            => await dataContext.BigTexts.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(BigText entity)
        {
            try
            {

                dataContext.Entry<BigText>(entity).State = await dataContext.BigTexts.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<BigText>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
