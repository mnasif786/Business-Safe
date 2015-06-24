using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Common
{
    public class BaseEntity<T> where T : struct
    {
        public virtual T Id { get; set; }

        public virtual UserForAuditing CreatedBy { get; set; }
        public virtual DateTime? CreatedOn { get; set; }
        public virtual UserForAuditing LastModifiedBy { get; set; }
        public virtual DateTime? LastModifiedOn { get; set; }
        public virtual bool Deleted { get; set; }
        private int? _hashCode;

        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity<T>;
            if (other == null) return false;

            if (other.Id.Equals(default(T?)) && Id.Equals(default(T?)))
                return other == this;

            if (other.Id.Equals(default(T?)) || Id.Equals(default(T?)))
                return false;

            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (_hashCode.HasValue)
                return _hashCode.Value;

            if (Id.Equals(default(T)))
            {
                _hashCode = base.GetHashCode();
                return _hashCode.Value;
            }

            return Id.GetHashCode();
        }

        protected internal virtual void SetLastModifiedDetails(UserForAuditing currentUser)
        {
            LastModifiedBy = currentUser;
            LastModifiedOn = DateTime.Now;
        }

        public virtual void MarkForDelete(UserForAuditing user)
        {
            if (!Deleted)
            {
                Deleted = true;
                SetLastModifiedDetails(user);
            }
        }

        public virtual void ReinstateFromDelete(UserForAuditing user)
        {
            if (Deleted)
            {
                Deleted = false;
                SetLastModifiedDetails(user);
            }
        }
    }
}