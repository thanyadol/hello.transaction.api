using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//model
using hello.transaction.core.Models;
//EF
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace hello.transaction.core.Repositories
{
    /*
     * 
     *
     */

    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> ListAsync();


        Task<Transaction> GetAsync(string id);
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<Transaction> UpdateAsync(Transaction transaction);

        Task<IEnumerable<Transaction>> CreateRangeAsync(IEnumerable<Transaction> transactions);

        //Task<Transaction> DeleteAsync(string id);
    }

    public class TransactionRepository : ITransactionRepository
    {

        private readonly NorthwindContext _context;
        public TransactionRepository(NorthwindContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        }


        public async Task<IEnumerable<Transaction>> CreateRangeAsync(IEnumerable<Transaction> transactions)
        {
            await _context.Transactions.AddRangeAsync(transactions);
            _context.SaveChanges();

            return transactions;
        }


        public async Task<Transaction> CreateAsync(Transaction transaction)
        {

            var entity = await _context.Transactions.AddAsync(transaction);
            _context.SaveChanges();

            return entity.Entity;
        }

        public async Task<Transaction> DeleteAsync(string id)
        {
            var entity = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<IEnumerable<Transaction>> ListAsync()
        {

            var entities = await _context.Transactions.ToListAsync();
            return entities;
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {

            var entity = await _context.Transactions.FindAsync(transaction.Id);
            _context.Transactions.Update(transaction);

            _context.SaveChanges();
            return entity;
        }

        public async Task<Transaction> GetAsync(string id)
        {
            var entity = await _context.Transactions.FindAsync(id);
            return entity;
        }


    }
}