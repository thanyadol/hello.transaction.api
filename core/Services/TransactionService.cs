using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//model
using hello.transaction.core.Models;

//logg
using Serilog;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using hello.transaction.core.Extensions;
using AutoMapper;
using hello.transaction.core.Repositories;
using hello.transaction.core.Exceptions;

namespace hello.transaction.core.Services
{

    public interface ITransactionService
    {
        Task<Transaction> CreateAsync(Transaction transaction);
        //Task<Transaction> UpdateAsync(Transaction transaction);
        //Task<Transaction> DeleteAsync(string id);

        Task<Transaction> GetAsync(string id);

        Task<IEnumerable<Transaction>> ListAsync();
        Task<Transaction> EnforceTransactionExistenceAsync(string id);
        Task<IEnumerable<Transaction>> CreateRangeAsync(IEnumerable<Transaction> transactions);

        //list
        Task<IEnumerable<TransactionPayment>> ListByCurrencyAsync(string currency);
        Task<IEnumerable<TransactionPayment>> ListByStatusAsync(string status);
        Task<IEnumerable<TransactionPayment>> ListByDateAsync(DateTime from, DateTime to);
    }

    public class TransactionService : ITransactionService
    {
        //transaction status
        protected readonly IGenericRepository<Transaction, string> _transactionRepository;

        private readonly IMapper _mapper;

        public TransactionService(IGenericRepository<Transaction, string> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            //logg
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .CreateLogger();
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            var entity = await _transactionRepository.CreateAsync(transaction);
            if (entity == null)
            {
                throw new TransactionNotCreatedException();
            }

            return entity;
        }

        public async Task<IEnumerable<Transaction>> CreateRangeAsync(IEnumerable<Transaction> transactions)
        {

            try
            {
                var entity = await _transactionRepository.CreateRangeAsync(transactions);
                return entity;
            }
            catch (DbUpdateException)
            {
                throw new TransactionNotCreatedException();
            }

        }

        public async Task<Transaction> GetAsync(string id)
        {
            var entity = await _transactionRepository.GetAsync(id);
            return entity;
        }


        public async Task<IEnumerable<Transaction>> ListAsync()
        {
            return await _transactionRepository.ListAsync();
        }

        public async Task<IEnumerable<TransactionPayment>> ListByCurrencyAsync(string currency)
        {
            var entities = await _transactionRepository.ListAsync();
            entities = entities.Where(c => c.CurrencyCode == currency);

            //map enumstatus and get code
            return _mapper.Map<List<TransactionPayment>>(entities);
        }

        public async Task<IEnumerable<TransactionPayment>> ListByStatusAsync(string status)
        {
            var entities = await _transactionRepository.ListAsync();
            entities = entities.Where(c => c.Status == status);

            //map enumstatus and get code
            return _mapper.Map<List<TransactionPayment>>(entities);
        }

        public async Task<IEnumerable<TransactionPayment>> ListByDateAsync(DateTime from, DateTime to)
        {
            var entities = await _transactionRepository.ListAsync();
            entities = entities.Where(c => (c.TransactionDate.GetValueOrDefault().Date >= from.Date
                                        && c.TransactionDate.GetValueOrDefault().Date <= to.Date));

            //map enumstatus and get code
            return _mapper.Map<List<TransactionPayment>>(entities);
        }


        public async Task<Transaction> EnforceTransactionExistenceAsync(string id)
        {
            var transaction = await _transactionRepository.GetAsync(id);

            if (transaction == null)
            {
                throw new TransactionNotFoundException();
            }

            return transaction;
        }

    }

}