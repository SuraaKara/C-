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
    public class SubImageDataService : ISubImageDataService
    {
        private readonly LoopusDataContext dataContext;

        public SubImageDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<SubImage> GetAsync(Expression<Func<SubImage, bool>> query)
            => await dataContext.SubImages.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<SubImage>> GetListAsync(Expression<Func<SubImage, bool>> query)
            => await dataContext.SubImages.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(SubImage entity)
        {
            try
            {

                dataContext.Entry<SubImage>(entity).State = await dataContext.SubImages.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<SubImage>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
