using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using hello.transaction.core.Models;

namespace hello.transaction.core.Exceptions
{
    public class TransactionNotFoundException : Exception
    {

        private const string DEFAULT_MESSAGE = "TransactionNotFoundException";
        public string rev { get; }
        public string value { get; }

        public TransactionNotFoundException()
           : base(DEFAULT_MESSAGE)
        {
        }

        public TransactionNotFoundException(Guid id)
            : base(string.Format("Transaction with id = {0} not found", id))
        {
        }

        public TransactionNotFoundException(string message, Transaction transaction)
            : base(message)
        {
        }

        public TransactionNotFoundException(string message, Exception inner)
       : base(message, inner)
        {
        }

    }

    public class TransactionNotCreatedException : Exception
    {

        private const string DEFAULT_MESSAGE = "TransactionNotCreatedException";

        public TransactionNotCreatedException()
           : base(DEFAULT_MESSAGE)
        {
        }
    }
}