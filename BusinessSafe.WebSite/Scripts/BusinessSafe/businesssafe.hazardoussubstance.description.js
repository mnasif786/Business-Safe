BusinessSafe.HazardousSubstance.Description = function () {
    if (isReadOnly()) {
        return this;
    }

    var selectors = {
        companyId: "#CompanyId",
        riskAssessmentId: "#RiskAssessmentId",
        riskAssessor: "#RiskAssessorEmployee",
        hazardousSubstance: "#HazardousSubstance",
        saveAndNextButtons: '#nextBtn, a.riskassessment-tab-links'
    };

    var urls = {
        save: '/HazardousSubstanceRiskAssessments/Description/Save',
        saveAndNext: '/HazardousSubstanceRiskAssessments/Description/SaveAndNext'
    };

    var riskAssessment = {
        Id: $(selectors.riskAssessmentId).val(),
        CompanyId: $(selectors.companyId).val()
    };

    if (riskAssessment.Id !== undefined) {

        BusinessSafe.RiskAssessment.Employees.ViewModel(riskAssessment);
        BusinessSafe.RiskAssessment.NonEmployees.ViewModel(riskAssessment);
    }

    
    $(selectors.saveAndNextButtons).click(function (event) {
        var isReadOnly = $(selectors.isReadOnly);

        if (isReadOnly.length > 0) {
            return;
        }

        event.preventDefault();

        var destUrl = $(this).attr("href");
        var f = $('form:first');

        f.attr('action', urls.saveAndNext);
        $.post(f[0].action, f.serialize(), function (result) {
            if (result.Success === true) {
                window.location = destUrl;
            } else {
                if ($(selectors.validationSummary).size() === 0)
                    f.prepend('<div class="span12"><div class="validation-summary-errors alert alert-error"></div></div>');
                showValidationErrors(selectors.validationSummary, result.Errors);
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
    });

    return this;
};
