using System;
using System.Collections;
using System.Data;
using System.Linq;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Common;
using NHibernate;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace BusinessSafe.Data.Common
{

    public class AuditUpdateListener : IPostUpdateEventListener, IPostInsertEventListener, IPostDeleteEventListener, IPostCollectionUpdateEventListener
    {
        private const string NoValueString = "";

        public void OnPostUpdate(PostUpdateEvent @event)
        {
            //try
            //{

            if (@event.Entity is Audit)
            {
                return;
            }

            if (@event.Entity is IAuditable)
            {
                var auditableEntity = @event.Entity as IAuditable;
                var entityFullName = @event.Entity.GetType().FullName;
                var entityId = @event.Id.ToString();

                if (@event.OldState == null)
                {
                    throw new AuditUpdateNotOldStatePresentExceptions(entityFullName, entityId);
                }

                var dirtyFieldIndexes = @event.Persister.FindDirty(@event.State, @event.OldState, @event.Entity,
                    @event.Session);
                var now = DateTime.Now;
                var updateId = Guid.NewGuid();

                foreach (var dirtyFieldIndex in dirtyFieldIndexes)
                {
                    var oldPropertyDetails = GetPropertyDetails(@event.OldState, dirtyFieldIndex, @event.Persister);
                    var newPropertyDetails = GetPropertyDetails(@event.State, dirtyFieldIndex, @event.Persister);

                    if (oldPropertyDetails.PropertyValue == newPropertyDetails.PropertyValue)
                    {
                        continue;
                    }

                    if (auditableEntity.LastModifiedBy == null)
                    {
                        throw new AuditUpdateLastModifiedByNotSetExceptions(entityFullName, entityId);
                    }
                    
                    AddRecord(
                        "U",
                        entityFullName,
                        entityId,
                        newPropertyDetails.PropertyName,
                        oldPropertyDetails.PropertyValue,
                        newPropertyDetails.PropertyValue,
                        now,
                        auditableEntity.LastModifiedBy.Id.ToString(),
                        updateId);
                    
                       
                }
            }
            //catch (Exception e)
            //    {
            //        string h = e.Message;
            //    }
        }

        public void OnPostInsert(PostInsertEvent @event)
        {
            if (@event.Entity is Audit)
            {
                return;
            }

            if (@event.Entity is IAuditable)
            {

                var auditableEntity = @event.Entity as IAuditable;
                var entityId = @event.Id.ToString();
                var entityFullName = @event.Entity.GetType().FullName;
                var propertyNames = @event.Persister.PropertyNames;
                var now = DateTime.Now;
                var updateId = Guid.NewGuid();

                for (int i = 0; i < propertyNames.Length; i++)
                {
                    var propertyDetails = GetPropertyDetails(@event, i);

                    if (auditableEntity.CreatedBy == null)
                    {
                        throw new AuditInsertCreatedByNotSetExceptions(entityFullName, entityId);
                    }

                    AddRecord(
                        "I",
                        entityFullName,
                        entityId,
                        propertyDetails.PropertyName,
                        NoValueString,
                        propertyDetails.PropertyValue,
                        now,
                        auditableEntity.CreatedBy.Id.ToString(),
                        updateId);

                }
            }
        }

        public void OnPostDelete(PostDeleteEvent @event)
        {
            if (@event.Entity is Audit)
            {
                return;
            }

            if (@event.Entity is IAuditable)
            {

                var auditableEntity = @event.Entity as IAuditable;
                var entityId = @event.Id.ToString();
                var entityFullName = @event.Entity.GetType().FullName;
                
                if (auditableEntity.LastModifiedBy == null)
                {
                    throw new AuditDeleteLastModifiedByNotSetExceptions(entityFullName, entityId);
                }

                AddRecord(
                    "D",
                    entityFullName,
                    entityId,
                    "Entity Deleted",
                    "Entity Deleted",
                    "Entity Deleted",
                    DateTime.Now,
                    auditableEntity.LastModifiedBy.Id.ToString(),
                    Guid.NewGuid());
            }
        }

        public void OnPostUpdateCollection(PostCollectionUpdateEvent @event)
        {
            if (@event.AffectedOwnerOrNull is Audit)
            {
                return;
            }

            if (@event.AffectedOwnerOrNull is IAuditable)
            {

                var auditableEntity = @event.AffectedOwnerOrNull as IAuditable;
                var entityId = auditableEntity.IdForAuditing;
                var entityFullName = @event.AffectedOwnerOrNull.GetType().FullName;

                var fieldName = GetItemTypeFromGenericType(@event.Collection.GetType()).Name;
                
                if (auditableEntity.LastModifiedBy == null)
                {
                    throw new AuditUpdateLastModifiedByNotSetExceptions(entityFullName, entityId);
                }

                var collectionItems = @event.Collection as IEnumerable;
                var now = DateTime.Now;
                var updateId = Guid.NewGuid();

                foreach (var item in collectionItems)
                {
                    if (item.GetType() == typeof(IAuditable))
                    {
                        var w = item as IAuditable;

                        AddRecord(
                            "U",
                            entityFullName,
                            entityId,
                            fieldName + " Collection",
                            "",
                            w.IdForAuditing,
                            now,
                            auditableEntity.LastModifiedBy.Id.ToString(),
                            updateId);
                    }
                  
                }
            }
        }

        private static PropertyDetails GetPropertyDetails(object[] stateArray, int propertyIndex, IEntityPersister persister)
        {
            var result = new PropertyDetails
            {
                PropertyName = persister.PropertyNames[propertyIndex]
            };


            var value = stateArray[propertyIndex];
            if (value is IAuditable)
            {
                result.PropertyName = result.PropertyName + "Id";
                result.PropertyValue = (value as IAuditable).IdForAuditing;
                return result;
            }

            if (value == null || value.ToString() == string.Empty)
            {
                result.PropertyValue = NoValueString;
                return result;
            }

            result.PropertyValue = value.ToString();
            return result;

        }

        private static PropertyDetails GetPropertyDetails(PostInsertEvent @event, int propertyIndex)
        {
            var result = new PropertyDetails
                             {
                                 PropertyName = @event.Persister.PropertyNames[propertyIndex]
                             };

            var newValue = @event.Persister.GetPropertyValue(@event.Entity, result.PropertyName, EntityMode.Poco);
            if (newValue == null || newValue.ToString() == string.Empty)
            {
                result.PropertyValue = NoValueString;
                return result;
            }

            if (newValue is IAuditable)
            {
                result.PropertyName = result.PropertyName + "Id";
                result.PropertyValue = (newValue as IAuditable).IdForAuditing;
                return result;
            }

            // Are we a collection if so then get all the values
            var collectionType = GetItemTypeFromGenericType(newValue.GetType());
            if (collectionType != null)
            {
                var isAuiditable =
                  collectionType.GetInterfaces().AsEnumerable().Count(x => x.Name == typeof(IAuditable).Name) == 1;
                
                if(isAuiditable)
                {
                    var collectionValues = newValue as IEnumerable;
                    foreach (var collectionValue in collectionValues)
                    {
                        var item = collectionValue as IAuditable;
                        if (result.PropertyValue != null && result.PropertyValue.Length > 0)
                        {
                            result.PropertyValue += ", ";
                        }
                        result.PropertyValue += item.IdForAuditing;
                    }
                    return result;    
                }
                
            }
            
            result.PropertyValue = newValue.ToString();
            return result;
        }

        private static Type GetItemTypeFromGenericType(Type type)
        {
            if (type.IsGenericType)
            {
                Type[] genericArguments = type.GetGenericArguments();
                if (genericArguments.GetUpperBound(0) >= 0)
                {
                    return genericArguments[genericArguments.GetUpperBound(0)];
                }
                return null;
            }
            if (type.BaseType != null)
            {
                return GetItemTypeFromGenericType(type.BaseType);
            }
            return null;
        }

        private void AddRecord(
            string type,
            string entityName,
            string entityId,
            string fieldName,
            string oldValue,
            string newValue,
            DateTime updateDate,
            string userName,
            Guid updateId)
        {
            var newConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BusinessSafe"].ConnectionString);
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO [Audit] ([Type], [EntityName], [EntityId], [FieldName], [OldValue], [NewValue], [UpdateDate], [UserName], [UpdateId]) ");
            sql.AppendLine("VALUES (@type, @entityName, @entityId, @fieldName, @oldValue, @newValue, @updateDate, @userName, @updateId) ");
            var command = new SqlCommand(sql.ToString(), newConnection);
            command.Parameters.Add("@type", SqlDbType.NVarChar, 1).Value = type;

            command.Parameters.Add("@entityName", SqlDbType.NVarChar, 200).Value = entityName;
            command.Parameters.Add("@entityId", SqlDbType.NVarChar, 200).Value = entityId;
            command.Parameters.Add("@fieldName", SqlDbType.NVarChar, 200).Value = fieldName;
            command.Parameters.Add("@oldValue", SqlDbType.NVarChar).Value = DBNull.Value;

            if (oldValue != null)
            {
                command.Parameters["@oldValue"].Value = oldValue;
            }

            command.Parameters.Add("@newValue", SqlDbType.NVarChar).Value = DBNull.Value;

            if (newValue != null)
            {
                command.Parameters["@newValue"].Value = newValue;
            }

            command.Parameters.Add("@updateDate", SqlDbType.DateTime).Value = updateDate;
            command.Parameters.Add("@userName", SqlDbType.NVarChar, 128).Value = userName;
            command.Parameters.Add("@updateId", SqlDbType.UniqueIdentifier).Value = updateId;

            try
            {
                newConnection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                newConnection.Close();
            }
        }

        private class PropertyDetails
        {
            public string PropertyName { get; set; }
            public string PropertyValue { get; set; }
        }
    }
}