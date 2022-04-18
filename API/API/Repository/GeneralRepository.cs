using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }
        // Mengambil semua data
        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }
        // Mengambil data berdasarkan PK
        public Entity Get(Key key)
        {
            return entities.Find(key);
        }
        public int DeleteAll()
        {
            var unity = entities.ToList();
            myContext.RemoveRange(unity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Delete(Key key)
        {
            var unity = entities.Find(key);
            myContext.Remove(unity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Insert(Entity entity)
        { 
            entities.Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Update(Entity entity, Key key)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
        
    }
}
