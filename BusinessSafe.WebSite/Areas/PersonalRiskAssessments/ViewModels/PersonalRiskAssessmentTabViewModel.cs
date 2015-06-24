using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels
{
    public class PersonalRiskAssessmentTabViewModel : TabViewModel<PersonalRiskAssessmentTabs>
    {
        public bool ChecklistSent { get; set; }

        public PersonalRiskAssessmentTabViewModel()
        {
            // TODO: load correctly from the risk assessment
            ChecklistSent = true;
        }

    }
}

