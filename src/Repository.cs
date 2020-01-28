using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncService
{
    public class Repository<T> : IRepository<T> where T: Entity
    {
        List<T> Entities { get; set; } = new List<T>();

        public IQueryable<T> GetAll()
        {
            return Entities.AsQueryable();
        }

        public T Insert(T entity)
        {
            Entities.Add(entity);
            return entity;
        }

        public void Delete(T entity )
        {
            Entities.Remove(entity);
        }

        public T Find(int id)
        {
            return this.Entities.Find(x => x.Id == id);
        }

        public T Update(T entity) 
        {
            if(Entities.Exists(x => x.Id == entity.Id))
            {
                Delete(entity);
                return Insert(entity);
            }
            return null;
        }
    }
}
