using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AspNetCorePublisherWebApi.Entities;

namespace AspNetCorePublisherWebApi.Services
{
    public class GenericEFRepository : IGenericEFRepository
    {
        private SqlDbContext _db;

        public GenericEFRepository(SqlDbContext db)
        {
            _db = db;
        }
        
        public IEnumerable<TEntity> Get<TEntity>() where TEntity : class
        {
            return _db.Set<TEntity>();
        }

        public TEntity Get<TEntity>(int id, bool includeRelatedEntities = false) where TEntity : class
        {
            var entity = _db.Set<TEntity>().Find(new object[] { id });
            if (entity != null && includeRelatedEntities)
            {
                var dbsets = typeof(SqlDbContext)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.PropertyType.Name.Contains("DbSet"))
                    .Select(x => x.Name);
                var tables = typeof(TEntity)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => dbsets.Contains(x.Name))
                    .Select(x => x.Name);
                if (tables.Count() > 0)
                {
                    foreach (var table in tables)
                    {
                        _db.Entry(entity).Collection(table).Load();
                    }
                }
            }

            return entity;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _db.Add<TEntity>(entity);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool Exist<TEntity>(int id) where TEntity : class
        {
            return _db.Set<TEntity>().Find(new object[] { id }) != null;
        }

        public void Delete<TEntity>(TEntity item) where TEntity : class
        {
            _db.Set<TEntity>().Remove(item);
        }
    }
}
