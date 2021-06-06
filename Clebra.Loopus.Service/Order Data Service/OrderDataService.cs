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
    public class OrderDataService : IOrderDataService
    {
        private readonly LoopusDataContext dataContext;

        public OrderDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        public async Task SubmitChangeAsync(Order entity)
        {
            try
            {

                dataContext.Entry<Order>(entity).State = await dataContext.Orders.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<Order>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public async Task<IEnumerable<Order>> GetListAsync(Expression<Func<Order, bool>> query)
       
            => await dataContext.Orders.Where(query).ToListAsync();
        
        public async Task<Order> GetAsync(Expression<Func<Order, bool>> query)
        
            => await dataContext.Orders.FirstOrDefaultAsync(query);

       
        
    }
}
