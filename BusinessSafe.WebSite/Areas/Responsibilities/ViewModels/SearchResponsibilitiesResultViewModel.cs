using System;

namespace BusinessSafe.WebSite.Areas.Responsibilities.ViewModels
{
    public class SearchResponsibilitiesResultViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Site { get; set; }
        public string Category { get; set; }
        public string Reason { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Frequency  { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsDeleted { get; set; }
       // public int PageSize { get; set; }
       // public int Total { get; set; }

        public string CreatedDateFormatted
        {
            get
            {
                return CreatedDate.ToShortDateString();
            }
        }

        public string DueDateFormatted
        {
            get
            {
                if (DueDate.HasValue)
                    return DueDate.Value.ToShortDateString();

                return string.Empty;
            }
        }

    }
}