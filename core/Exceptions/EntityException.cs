// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.using System

using System;

namespace hello.transaction.core.Exceptions
{
    public class EntityException : Exception
    {
        public EntityException() { }

        public EntityException(string message) : base(message) { }
    }

    public class EntityAlreadyExistsException : EntityException
    {
    }

    public class EntityNotFoundException : EntityException
    {
    }

    public class EntityNotCreateException : EntityException
    {
    }


}
