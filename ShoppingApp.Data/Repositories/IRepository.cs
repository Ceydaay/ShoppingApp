﻿using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Repositories
{
    // IRepository, generic bir repository arayüzüdür. TEntity türünde veritabanı işlemleri için metodlar tanımlar.
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity, bool softDelete = true);
        void Delete(int id);
        void Update(TEntity entity);

        TEntity GetById(int id);

        TEntity Get(Expression<Func<TEntity,bool>> predicate);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity,bool>> predicate = null);
        Task<bool> UserExistAsync(int customerId);






    }
}
