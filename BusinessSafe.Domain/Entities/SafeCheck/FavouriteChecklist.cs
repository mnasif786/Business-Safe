using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class FavouriteChecklist : BaseEntity<Guid>
    {
        public virtual string Title { get; set; }
        public virtual Checklist Checklist { get; set; }
        public virtual string MarkedByUser { get; set; }

        public static FavouriteChecklist Create(string title, Checklist checklist, string user)
        {
            return new FavouriteChecklist()
            {
                Id = Guid.NewGuid(),
                Checklist = checklist,
                MarkedByUser = user,
                Title = title
            };
        }
    }

    
}
