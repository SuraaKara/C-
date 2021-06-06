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
    public class SmallTextDataService : ISmallTextDataService
    {
        private readonly LoopusDataContext dataContext;

        public SmallTextDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<SmallText> GetAsync(Expression<Func<SmallText, bool>> query)
            => await dataContext.SmallTexts.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<SmallText>> GetListAsync(Expression<Func<SmallText, bool>> query)
            => await dataContext.SmallTexts.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(SmallText entity)
        {
            try
            {

                dataContext.Entry<SmallText>(entity).State = await dataContext.SmallTexts.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<SmallText>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
