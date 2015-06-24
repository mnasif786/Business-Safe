BusinessSafe.PersonalRiskAssessment.Checklists.Summary = function () {
    var viewPersonalRiskAssessment = '/PersonalRiskAssessments/Summary';

    var _selectors = {
        generateRislAssessmentLink: "input#save-and-generate",
        checklistId: "#checklistId",
        validationMessage: "#checklist-validation-message",
        isFurtherActionRequired: 'input[name="IsFurtherActionRequired"]'
    };

    function _UpdateEmployeeChecklistFurtherRequiredValue(sender) {
        var url = window.globalajaxurls.updateEmployeeChecklistFurtherRequiredUrl;
        var postData = {};
        postData.employeeChecklistId = $(_selectors.checklistId).val();
        postData.isFurtherActionRequired = $(_selectors.isFurtherActionRequired).val();

        $.post(url, postData, function (data) {
            if (data.Success === true) {
                if (data.riskAssessmentId > 0) {
                    window.location = viewPersonalRiskAssessment + "?riskAssessmentId=" + data.riskAssessmentId + '&' + 'companyId=' + data.companyId
                } else {
                    window.location.reload();
                }
            } else {
                $(_selectors.validationMessage).show();
                showValidationErrors(_selectors.validationMessage, data.Errors);
            }
        }).error(function (jqXhr, textStatus, errorThrown) {
            if (jqXhr.status !== 0) {
                if (window.debugErrorHandler === undefined) {
                    window.location.replace(window.globalajaxurls.errorPage);
                } else {
                    alert("DEBUG: AjaxCall.execute encountered a problem.");
                }
            } else {
                window.location.reload();
            }
        });
    }

    function _setupGernateRiskAssessmentLink() {
        $(_selectors.generateRislAssessmentLink).click(function (event) {
            event.preventDefault();
            _UpdateEmployeeChecklistFurtherRequiredValue(this);
        });
    }

    function _initialize() {
        _setupGernateRiskAssessmentLink();
    }

    return {
        initialize: _initialize
    };
} ();

