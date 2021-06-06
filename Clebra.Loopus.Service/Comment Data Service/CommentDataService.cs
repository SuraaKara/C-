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
    public class CommentDataService : ICommentDataService
    {
        private readonly LoopusDataContext dataContext;

        public CommentDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<Comment> GetAsync(Expression<Func<Comment, bool>> query)
         => await dataContext.Comments.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<Comment>> GetListAsync(Expression<Func<Comment, bool>> query)
         => await dataContext.Comments.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(Comment entity)
        {
            try
            {

                dataContext.Entry<Comment>(entity).State = await dataContext.Comments.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<Comment>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
