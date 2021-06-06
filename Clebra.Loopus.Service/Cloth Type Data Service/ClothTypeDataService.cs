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
    public class ClothTypeDataService : IClothTypeDataService
    {

        private readonly LoopusDataContext dataContext;

        public ClothTypeDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ClothType> GetAsync(Expression<Func<ClothType, bool>> query)
        => await dataContext.ClothTypes.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<ClothType>> GetListAsync(Expression<Func<ClothType, bool>> query)
        => await dataContext.ClothTypes.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(ClothType entity)
        {
            try
            {

                dataContext.Entry<ClothType>(entity).State = await dataContext.ClothTypes.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<ClothType>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
