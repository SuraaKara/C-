using Clebra.Loopus.DataAccess;
using Clebra.Loopus.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clebra.Loopus.Service
{
    public class CountryDataService : ICountryDataService
    {
        private readonly LoopusDataContext dataContext;

        public CountryDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<Country> GetAsync(Expression<Func<Country, bool>> query)
            => await dataContext.Countries.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<Country>> GetListAsync(Expression<Func<Country, bool>> query)
            => await dataContext.Countries.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(Country entity)
        {
            try
            {

                dataContext.Entry<Country>(entity).State = await dataContext.Countries.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<Country>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
