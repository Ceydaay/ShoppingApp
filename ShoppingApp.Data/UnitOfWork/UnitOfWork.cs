using Microsoft.EntityFrameworkCore.Storage;
using ShoppingApp.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoppingAppDbContext _db;

        private IDbContextTransaction _transaction;
        public UnitOfWork(ShoppingAppDbContext db)
        {
            _db = db;
        }
        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _db.Dispose();

            //Where we give the Garbage Collecter permission to clean.
            //It is not deleted at that moment-it makes it erasable.
        }

        public async Task RollBackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
