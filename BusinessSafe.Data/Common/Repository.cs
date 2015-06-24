using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Infrastructure.Attributes;
using NHibernate;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Common
{
    /// <summary>
    /// Repository: base repository for persisting an entity.
    /// </summary>
    [CoverageExclude]
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId> 
        where TId : struct
    {
        protected readonly IBusinessSafeSessionManager SessionManager;

        protected Repository(IBusinessSafeSessionManager sessionManager)
        {
            SessionManager = sessionManager;
        }
        
        public void Flush()
        {
            SessionManager.Session.Flush();
        }

        /// <summary>
        /// Saves an entity if it is new, or updates it if it is old.
        /// </summary>
        /// <param name="obj">The entity to save or update.</param>
        public virtual void SaveOrUpdate(TEntity obj)
        {
            SessionManager.Session.SaveOrUpdate(obj);
        }

        /// <summary>
        /// Saves an entity.
        /// </summary>
        /// <param name="obj">The entity to save.</param>
        public virtual void Save(TEntity obj)
        {
            SessionManager.Session.Save(obj);
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="obj">The entity to update.</param>
        public virtual void Update(TEntity obj)
        {
            SessionManager.Session.Update(obj);
        }

        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to return.</param>
        /// <returns>The entity with the specified ID.</returns>
        public virtual TEntity GetById(TId id)
        {
            return SessionManager.Session.Get<TEntity>(id);
        }

        /// <summary>
        /// Loads a proxy for an entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the proxy of the entity to return.</param>
        /// <returns>A proxy of the entity with the specified ID.</returns>
        public virtual TEntity LoadById(TId id)
        {
            return SessionManager.Session.Load<TEntity>(id);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>List of all entities.</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            ICriteria criteriaQuery = SessionManager.Session.CreateCriteria(typeof(TEntity));
            return criteriaQuery.List<TEntity>();
        }

        /// <summary>
        /// Gets entities with specified Ids.
        /// </summary>
        /// <param name="ids">Ids of entities to get.</param>
        /// <returns>List of entities matching ids.</returns>
        public IEnumerable<TEntity> GetByIds(IList<TId> ids)
        {
            return SessionManager.Session.CreateCriteria<TEntity>()
                .Add(Restrictions.In("Id", ids.ToArray()))
                .List<TEntity>()
                .ToList();               
        }

        public void Initialize(object obj)
        {
            NHibernateUtil.Initialize(obj);
        }
    }


    ///// <summary>
    ///// Repository: base repository for persisting an entity.
    ///// </summary>
    //[CoverageExclude]
    //public abstract class NonAuditableRepository<TNonAuditableEntity, TId> : INonAuditableRepository<TNonAuditableEntity, TId>
    //    where TNonAuditableEntity : NonAuditableEntity<TId>
    //    where TId : struct
    //{
    //    protected readonly IBusinessSafeSessionManager SessionManager;

    //    protected NonAuditableRepository(IBusinessSafeSessionManager sessionManager)
    //    {
    //        SessionManager = sessionManager;
    //    }

    //    public void Flush()
    //    {
    //        SessionManager.Session.Flush();
    //    }

    //    /// <summary>
    //    /// Saves an entity if it is new, or updates it if it is old.
    //    /// </summary>
    //    /// <param name="obj">The entity to save or update.</param>
    //    public virtual void SaveOrUpdate(TNonAuditableEntity obj)
    //    {
    //        SessionManager.Session.SaveOrUpdate(obj);
    //    }

    //    /// <summary>
    //    /// Saves an entity.
    //    /// </summary>
    //    /// <param name="obj">The entity to save.</param>
    //    public virtual void Save(TNonAuditableEntity obj)
    //    {
    //        SessionManager.Session.Save(obj);
    //    }

    //    /// <summary>
    //    /// Updates an entity.
    //    /// </summary>
    //    /// <param name="obj">The entity to update.</param>
    //    public virtual void Update(TNonAuditableEntity obj)
    //    {
    //        SessionManager.Session.Update(obj);
    //    }

    //    /// <summary>
    //    /// Gets the entity with the specified ID.
    //    /// </summary>
    //    /// <param name="id">The ID of the entity to return.</param>
    //    /// <returns>The entity with the specified ID.</returns>
    //    public virtual TNonAuditableEntity GetById(TId id)
    //    {
    //        return SessionManager.Session.Get<TNonAuditableEntity>(id);
    //    }

    //    /// <summary>
    //    /// Loads a proxy for an entity with the specified ID.
    //    /// </summary>
    //    /// <param name="id">The ID of the proxy of the entity to return.</param>
    //    /// <returns>A proxy of the entity with the specified ID.</returns>
    //    public virtual TNonAuditableEntity LoadById(TId id)
    //    {
    //        return SessionManager.Session.Load<TNonAuditableEntity>(id);
    //    }

    //    /// <summary>
    //    /// Gets all entities.
    //    /// </summary>
    //    /// <returns>List of all entities.</returns>
    //    public virtual IEnumerable<TNonAuditableEntity> GetAll()
    //    {
    //        ICriteria criteriaQuery = SessionManager.Session.CreateCriteria(typeof(TNonAuditableEntity));
    //        return criteriaQuery.List<TNonAuditableEntity>();
    //    }

    //    /// <summary>
    //    /// Gets entities with specified Ids.
    //    /// </summary>
    //    /// <param name="ids">Ids of entities to get.</param>
    //    /// <returns>List of entities matching ids.</returns>
    //    public IEnumerable<TNonAuditableEntity> GetByIds(IList<TId> ids)
    //    {
    //        return SessionManager.Session.CreateCriteria<TNonAuditableEntity>()
    //            .Add(Restrictions.In("Id", ids.ToArray()))
    //            .List<TNonAuditableEntity>()
    //            .ToList();
    //    }
    //}
}

