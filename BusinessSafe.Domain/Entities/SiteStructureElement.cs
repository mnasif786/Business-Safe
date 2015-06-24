using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;

namespace BusinessSafe.Domain.Entities
{   
    public abstract class SiteStructureElement : Entity<long>
    {
        public virtual long ClientId { get; set; }
        public virtual string Name { get; set; }
        public virtual SiteStructureElement Parent { get; set; }
        public virtual IList<SiteStructureElement> Children { get; set; }
        public virtual string SiteContact { get; set; }
        public virtual DateTime? SiteClosedDate { get; set; }

        protected IList<AccidentRecordNotificationMember> _accidentRecordNotificationMembers;//  { get; set; }

        public virtual IList<AccidentRecordNotificationMember> AccidentRecordNotificationMembers
        {
            get
            {
                return _accidentRecordNotificationMembers
                    .Where( x=> !x.Deleted && x.IsInitialised() )                  
                    .ToList();
            }
        }

        protected SiteStructureElement()
        {
            Children = new List<SiteStructureElement>();
            _accidentRecordNotificationMembers = new List<AccidentRecordNotificationMember>();
        }

        public override void MarkForDelete(UserForAuditing deletingUser)
        {
            if (Children.Any())
            {
                throw new MarkForDeleteSiteGroupException(Id);
            }

            base.MarkForDelete(deletingUser);
        }

        public virtual List<SiteStructureElement> GetThisAndAllDescendants()
        {
            var sites = new List<SiteStructureElement> {this};

            if (Children != null)
            {
                foreach (var childSite in Children)
                {
                    sites.AddRange(childSite.GetThisAndAllDescendants().Where(x => x.Deleted == false));
                }
            }

            return sites;
        }

        public virtual List<SiteStructureElement> GetThisAndAllAncestors()
        {
            var sites = new List<SiteStructureElement> { this };

            if (Parent != null)
            {
                sites.AddRange(Parent.GetThisAndAllAncestors().Where(x => x.Deleted == false));
            }

            return sites;

        }

        public virtual bool IsMainSite
        {
            get { return Parent == null; }
        }

        public virtual SiteStructureElement Self
        {
            get { return this; }
        }

        public virtual void AddAccidentRecordNotificationMember(Employee employee, UserForAuditing user)
        {
            if (!_accidentRecordNotificationMembers.Any(x => x.MatchesEmployee( employee ) ))
            {
                var accidentRecordNotificationMember = new AccidentRecordNotificationEmployeeMember
                {
                    CreatedBy = user,
                    CreatedOn = DateTime.Now,
                    Deleted = false,
                    LastModifiedBy = user,
                    LastModifiedOn = DateTime.Now,
                    Site = this,
                    Employee = employee,                   
                };

                _accidentRecordNotificationMembers.Add(accidentRecordNotificationMember);  
  
                SetLastModifiedDetails(user);                
            }

            if (_accidentRecordNotificationMembers.Any(x => x.MatchesEmployee( employee ) && x.Deleted))
            {
                _accidentRecordNotificationMembers.First(x => x.MatchesEmployee( employee ) && x.Deleted).ReinstateFromDelete(user);
                SetLastModifiedDetails(user);
            }
        }

        public virtual void AddNonEmployeeToAccidentRecordNotificationMembers(string nonEmployeeName, string nonEmployeeEmail, UserForAuditing user)
        {           
            var accidentRecordNotificationMember = new AccidentRecordNotificationNonEmployeeMember
            {
                CreatedBy = user,
                CreatedOn = DateTime.Now,
                Deleted = false,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now,
                Site = this,               
                NonEmployeeName = nonEmployeeName,
                NonEmployeeEmail = nonEmployeeEmail
            };

            _accidentRecordNotificationMembers.Add(accidentRecordNotificationMember);

            SetLastModifiedDetails(user);                      
        }

        public virtual void RemoveAccidentRecordNotificationMember(Employee employee, UserForAuditing user)
        {
            if (_accidentRecordNotificationMembers.Any(x => x.MatchesEmployee(employee) ))
            {
                _accidentRecordNotificationMembers.First(x => x.MatchesEmployee( employee )).MarkForDelete(user);
                SetLastModifiedDetails(user);                
            }
        }

        public virtual void RemoveNonEmployeeAccidentRecordNotificationMember(string nonEmployeeEmail, UserForAuditing user)
        {
            if (_accidentRecordNotificationMembers.Any( x => !x.Deleted
                                                        && x.GetType() == typeof(AccidentRecordNotificationNonEmployeeMember)  
                                                        && x.Email() == nonEmployeeEmail ))
            {
                _accidentRecordNotificationMembers.First(x => !x.Deleted
                                                            && x.GetType() == typeof(AccidentRecordNotificationNonEmployeeMember)  
                                                            && x.Email() == nonEmployeeEmail ).MarkForDelete(user);
                SetLastModifiedDetails(user);
            }
        }
    }
}