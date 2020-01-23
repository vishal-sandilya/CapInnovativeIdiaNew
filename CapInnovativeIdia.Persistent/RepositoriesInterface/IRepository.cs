﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CapInnovativeIdia.Persistent.RepositoriesInterface
{
    public interface IRepository<TEntity> where TEntity:class
    {
        TEntity GET(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);       

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
