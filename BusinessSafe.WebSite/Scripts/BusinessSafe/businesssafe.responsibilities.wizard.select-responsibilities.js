BusinessSafe.Responsibilities.Wizard.SelectResponsibilities = function () {
    var _initialise = function () {

        var selectors = {
            selectAllResponsbilityInCategoryButton: ".responsibilityCategorySelector",
            responsibilityTemplateRow: ".responsibilityTemplateRow",
            generateResponsibilitiesTab: '#nav-generate-responsibilities'
        };

        $(selectors.selectAllResponsbilityInCategoryButton).click(function (e) {
            var checked = $(this).prop("checked");
            var table = $(this).closest('table');
            if (checked) {
                selectAllCheckboxesInTableColumn(table, 1);
            }
            else {
                deSelectAllCheckboxesInTableColumn(table, 1);
            }
        });

        $(selectors.generateResponsibilitiesTab).click(function (e) {
            e.preventDefault();
            var selectedResponsibilityTemplateIds = [];
            $(selectors.checkbox = ":checked").not('.responsibilityCategorySelector').each(function () {
                selectedResponsibilityTemplateIds.push($(this).val());
            });
            var selectedResponsibilityTemplateIdsAsString = selectedResponsibilityTemplateIds.join(',');
            window.location ='/Responsibilities/Wizard/GenerateResponsibilities?selectedResponsibilityTemplateIds=' + selectedResponsibilityTemplateIdsAsString;
        });
    };

    return { initialise: _initialise };
} ();