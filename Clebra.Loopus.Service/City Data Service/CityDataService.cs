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
    public class CityDataService : ICityDataService
    {
        private readonly LoopusDataContext dataContext;

        public CityDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<City> GetAsync(Expression<Func<City, bool>> query)
             => await dataContext.Cities.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<City>> GetListAsync(Expression<Func<City, bool>> query)
            => await dataContext.Cities.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(City entity)
        {

            try
            {

                dataContext.Entry<City>(entity).State = await dataContext.Cities.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<City>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
