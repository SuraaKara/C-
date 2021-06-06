using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Core.Service
{
    public interface IDataService<TEntity>  
        where  TEntity : class , ILoopusModel
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> query);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> query);
        
        Task SubmitChangeAsync(TEntity entity);
    }
}