using System;
using System.Collections.Generic;

namespace BusinessSafe.Domain.Common
{
    /// <summary>
    /// Repository contract: for persisting an entity.
    /// </summary>
    public interface IRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId> 
        where TId : struct
    {
        /// <summary>
        /// Saves an entity if it is new, or updates it if it is old.
        /// </summary>
        /// <param name="obj">The entity to save or update.</param>
        void SaveOrUpdate(TEntity obj);

        void Save(TEntity obj);
        void Update(TEntity obj);

        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to return.</param>
        /// <returns>The entity with the specified ID.</returns>
        TEntity GetById(TId id);

        /// <summary>
        /// Loads a proxy for an entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the proxy of the entity to return.</param>
        /// <returns>A proxy of the entity with the specified ID.</returns>
        TEntity LoadById(TId id);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>List of all entities.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets entities with specified Ids.
        /// </summary>
        /// <param name="ids">Ids of entities to get.</param>
        /// <returns>List of entities matching ids.</returns>
        IEnumerable<TEntity> GetByIds(IList<TId> ids);

        void Flush();
        void Initialize(object obj);
    }
}