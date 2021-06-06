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
    public class UserDataService : IUserDataService
    {


        private readonly LoopusDataContext dataContext;

        public UserDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task SubmitChangeAsync(LoopusUser entity)
        {
            try
            {
                
                dataContext.Entry<LoopusUser>(entity).State = await dataContext.LoopusUsers.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<LoopusUser>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task<LoopusUser> SignIn(string username, string password)
            => await dataContext.LoopusUsers.FirstOrDefaultAsync(f => f.Username == username && f.Password == password);

        public async Task<IEnumerable<LoopusUser>> GetListAsync(Expression<Func<LoopusUser, bool>> query)
        => await dataContext.LoopusUsers.Where(query).ToListAsync();
        public async Task<LoopusUser> GetAsync(Expression<Func<LoopusUser, bool>> query)
        => await dataContext.LoopusUsers.FirstOrDefaultAsync(query);
       

        
    }
}
