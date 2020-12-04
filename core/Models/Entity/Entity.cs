// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.using System

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hello.transaction.core.Models
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        [JsonProperty("id")]
        public TKey Id { get; set; }

        [JsonProperty("createdOn")]
        public DateTime Created { get; set; }

        protected Entity()
        {
            this.Created = DateTime.UtcNow;
        }
    }
}
