using System;

namespace DataAccess.Entity
{
    /// <summary>
    /// A special interface that every entity that has an identifier must implement.
    /// </summary>
    public interface IHasIdentityEntity
    {
        Guid Id { get; set; }
    }
}
