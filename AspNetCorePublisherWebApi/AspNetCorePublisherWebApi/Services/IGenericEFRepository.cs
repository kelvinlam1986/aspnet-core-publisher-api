using System.Collections.Generic;

namespace AspNetCorePublisherWebApi.Services
{
    public interface IGenericEFRepository
    {
        IEnumerable<TEntity> Get<TEntity>() where TEntity : class;
        TEntity Get<TEntity>(int id, bool includeRelatedEntities = false) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        bool Save();
        bool Exist<TEntity>(int id) where TEntity : class;
        void Delete<TEntity>(TEntity item) where TEntity : class;
    }
}
