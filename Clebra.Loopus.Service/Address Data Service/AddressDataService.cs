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
    public class AddressDataService : IAddressDataService
    {
        private readonly LoopusDataContext dataContext;

        public AddressDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<Address> GetAsync(Expression<Func<Address, bool>> query)
            => await dataContext.Addresses.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<Address>> GetListAsync(Expression<Func<Address, bool>> query)
            => await dataContext.Addresses.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(Address entity)
        {
            try
            {

                dataContext.Entry<Address>(entity).State = await dataContext.Addresses.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<Address>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

