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
    public class SliderDataService : ISliderDataService
    {
        private readonly LoopusDataContext dataContext;

        public SliderDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<Slider> GetAsync(Expression<Func<Slider, bool>> query)
            => await dataContext.Sliders.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<Slider>> GetListAsync(Expression<Func<Slider, bool>> query)
            => await dataContext.Sliders.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(Slider entity)
        {
            try
            {

                dataContext.Entry<Slider>(entity).State = await dataContext.Sliders.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<Slider>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
