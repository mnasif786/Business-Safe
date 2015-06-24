using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class QaAdvisor : BaseEntity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
        public virtual bool InRotation { get; set; }

        public QaAdvisor()
        {
            Forename = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            InRotation = false;
        }

        public static QaAdvisor Create(string forename,string surname, string email)
        {
            return new QaAdvisor(){Forename = forename,Surname = surname, Email = email};
        }

        public virtual void Update(string forename,string surname, string email, bool inRotation)
        {
            Forename = forename;
            Surname = surname;
            Email = email;
            InRotation = inRotation;
        }

    }
}
