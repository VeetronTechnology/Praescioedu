using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web;



namespace AOPM.Domain.Entities
{
   public class AuditTrailFactory
    {
        private readonly DbContext _context;
       private readonly string _userID;

       public AuditTrailFactory(DbContext context, string userId="admin")
        {
            _context = context;
            _userID = userId;
        }

       public AuditLog GetAudit(DbEntityEntry entry)
        {
            AuditLog audit = new AuditLog
            {
                UserId = _userID,
                TableName = GetTableName(entry),
                UpdateDate = DateTime.Now,
                TableIdValue = GetKeyValue(entry)
            };

            //entry is Added 
            if (entry.State == EntityState.Added)
            {
                var newValues = new StringBuilder();
                SetAddedProperties(entry, newValues);
                audit.NewData = newValues.ToString();
                audit.Actions = AuditActions.I.ToString();
            }
            //entry in deleted
            else if (entry.State == EntityState.Deleted)
            {
                var oldValues = new StringBuilder();
                SetDeletedProperties(entry, oldValues);
                audit.OldData = oldValues.ToString();
                audit.Actions = AuditActions.D.ToString();
            }
            //entry is modified
            else if (entry.State == EntityState.Modified)
            {
                var oldValues = new StringBuilder();
                var newValues = new StringBuilder();
                SetModifiedProperties(entry, oldValues, newValues);
                audit.OldData = oldValues.ToString();
                audit.NewData = newValues.ToString();
                audit.Actions = AuditActions.U.ToString();
            }

            return audit;
        }

        private void SetAddedProperties(DbEntityEntry entry, StringBuilder newData)
        {
            foreach (var propertyName in entry.CurrentValues.PropertyNames)
            {
                var newVal = entry.CurrentValues[propertyName];
                if (newVal != null)
                {
                    newData.AppendFormat("{0}={1} || ", propertyName, newVal);
                }
            }
            if (newData.Length > 0)
                newData.Remove(newData.Length - 3, 3);
        }

        private void SetDeletedProperties(DbEntityEntry entry, StringBuilder oldData)
        {
            DbPropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in dbValues.PropertyNames)
            {
                var oldVal = dbValues[propertyName];
                if (oldVal != null)
                {
                    oldData.AppendFormat("{0}={1} || ", propertyName, oldVal);
                }
            }
            if (oldData.Length > 0)
                oldData.Remove(oldData.Length - 3, 3);
        }

        private void SetModifiedProperties(DbEntityEntry entry, StringBuilder oldData, StringBuilder newData)
        {
            DbPropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in entry.OriginalValues.PropertyNames)
            {
                var oldVal = dbValues[propertyName];
                var newVal = entry.CurrentValues[propertyName];
                if (oldVal != null && newVal != null && !Equals(oldVal, newVal))
                {
                    newData.AppendFormat("{0}={1} || ", propertyName, newVal);
                    oldData.AppendFormat("{0}={1} || ", propertyName, oldVal);
                }
            }
            if (oldData.Length > 0)
                oldData.Remove(oldData.Length - 3, 3);
            if (newData.Length > 0)
                newData.Remove(newData.Length - 3, 3);
        }

        public long? GetKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            long id = 0;
            if (objectStateEntry.EntityKey.EntityKeyValues != null)
                id = Convert.ToInt64(objectStateEntry.EntityKey.EntityKeyValues[0].Value);

            return id;
        }

        private string GetTableName(DbEntityEntry dbEntry)
        {
            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
            return tableName;
        }
    }

   public enum AuditActions
   {
       I,
       U,
       D
   }
}
