﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository
{
    public class GeneralRepository<Context,Entity,Key>: IRepository<Entity,Key>
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

        public int Delete(Key key)
        {
             var entity = entities.Find(key);
            entities.Remove(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            return entities.Find(key);
        }

        public int Insert(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity null");

            }
            entities.Add(entity);
            var result = myContext.SaveChanges();
                return result;
        }

        public int Update(Entity entity)
        {
            //if (entity == null)
            //{
            //    throw new ArgumentNullException("Entity null");

            //}

            myContext.Entry(entity).State = EntityState.Modified;

            var result = myContext.SaveChanges();
            return result;
        }
    }
}
