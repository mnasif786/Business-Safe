using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class Consultant : BaseEntity<Guid>
    {
        public virtual string Username { get; set; }
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
        public virtual int PercentageOfChecklistsToSendToQualityControl { get; set; }
        public virtual Guid? QaAdvisorAssigned { get; set; }

        public virtual string FullName
        {
            get
            {
                string fullName = Forename;
                if (!String.IsNullOrEmpty(Forename) && !String.IsNullOrEmpty(Surname))
                    fullName += " ";

                fullName += Surname;
                return fullName;
            }
        }
        
        public static Consultant Create(string forename, string surname)
        {
            var username = forename + '.' + surname;
            var email = forename + ',' + surname + "@peninsula-uk.com";
            return Create(username, forename, surname, email);
        
        }

        public static Consultant Create(string username,string forename, string surname, string email)
        {
            return new Consultant()
            {
                Username = username
                ,
                Forename = forename
                ,
                Surname = surname
                ,
                Email = email,
                PercentageOfChecklistsToSendToQualityControl = 20
            };
        }

        public virtual void AddToBlacklist()
        {
            PercentageOfChecklistsToSendToQualityControl = 100;
        }

        public virtual void RemoveFromBlacklist()
        {
            PercentageOfChecklistsToSendToQualityControl = 20;
        }
    }
}
