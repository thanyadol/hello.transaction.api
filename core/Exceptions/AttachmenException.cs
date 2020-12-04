using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using hello.transaction.core.Models;

namespace hello.transaction.core.Exceptions
{
    public class AttachmentNotFoundException : Exception
    {

        private const string DEFAULT_MESSAGE = "AttachmentNotFoundException";
        public string rev { get; }
        public string value { get; }

        public AttachmentNotFoundException()
           : base(DEFAULT_MESSAGE)
        {
        }

        public AttachmentNotFoundException(Guid id)
            : base(string.Format("Attachment with id = {0} not found", id))
        {
        }

        public AttachmentNotFoundException(string message, Attachment transaction)
            : base(message)
        {
        }

        public AttachmentNotFoundException(string message, Exception inner)
       : base(message, inner)
        {
        }

    }

    public class AttachmentNotValidException : Exception
    {

        private const string DEFAULT_MESSAGE = "AttachmentNotValidException";
        public string rev { get; }
        public string value { get; }

        public AttachmentNotValidException()
           : base(DEFAULT_MESSAGE)
        {
        }

        public AttachmentNotValidException(Attachment attach)
            : this(string.Format("Attachment with id = {0} not valid", attach.Id.ToString()))
        {
        }

        public AttachmentNotValidException(string message)
            : base(message)
        {
        }

        public AttachmentNotValidException(string message, Exception inner)
       : base(message, inner)
        {
        }
    }
    public class AttachmentNotCreatedException : Exception
    {

        private const string DEFAULT_MESSAGE = "AttachmentNotCreatedException";
        public AttachmentNotCreatedException()
           : base(DEFAULT_MESSAGE)
        {
        }
    }


}