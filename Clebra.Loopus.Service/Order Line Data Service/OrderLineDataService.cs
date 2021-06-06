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
   public  class OrderLineDataService : IOrderLineDataService
   {        
            private readonly LoopusDataContext dataContext;

            public OrderLineDataService(LoopusDataContext dataContext)
            {
                this.dataContext = dataContext;
            }


            public async Task SubmitChangeAsync(OrderLine entity)
            {
                try
                {

                    dataContext.Entry<OrderLine>(entity).State = await dataContext.OrderLines.AnyAsync(a => a.Id == entity.Id)
                        ? EntityState.Modified
                        : EntityState.Added;

                    await dataContext.SaveChangesAsync();

                    dataContext.Entry<OrderLine>(entity).State = EntityState.Detached;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }


            public async Task<IEnumerable<OrderLine>> GetListAsync(Expression<Func<OrderLine, bool>> query)

                => await dataContext.OrderLines.Where(query).ToListAsync();

            public async Task<OrderLine> GetAsync(Expression<Func<OrderLine, bool>> query)

                => await dataContext.OrderLines.FirstOrDefaultAsync(query);

           
        }
    }

