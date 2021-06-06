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
    public class ColorDataService : IColorDataService
    {
        private readonly LoopusDataContext dataContext;

        public ColorDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<Color> GetAsync(Expression<Func<Color, bool>> query)
        => await dataContext.Colors.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<Color>> GetListAsync(Expression<Func<Color, bool>> query)
        => await dataContext.Colors.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(Color entity)
        {
            try
            {

                dataContext.Entry<Color>(entity).State = await dataContext.Colors.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<Color>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
