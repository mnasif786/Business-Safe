using System.Collections.Generic;

namespace BusinessSafe.AcceptanceTests.StepHelpers
{
    public static class ElementCssHelper
    {
        public static string ClassFor(Elements element)
        {
            var _elementCssClasses = new Dictionary<Elements, string>
                                     {
                                         { Elements.FcmReassignButton, "btn btn-reassign-further-action-task pull-right" },
                                         { Elements.EditFcmButton, "btn btn-edit-further-action-task" },
                                         { Elements.HazardousSubstanceRemoveButton, "deleteIcon icon-remove" },
                                         { Elements.HazardousSubstanceReinstateButton, "reinstateIcon icon-refresh" }
                                     };
            return _elementCssClasses[element];
        }
    }

    public enum Elements
    {
        FcmReassignButton,
        EditFcmButton,
        HazardousSubstanceRemoveButton,
        HazardousSubstanceReinstateButton
    }
}
