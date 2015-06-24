// General Risk Assessment Options
BusinessSafe.Tasks.GeneralRiskAssessment.Options = function () {

    var selectors = {
        companyId: "#CompanyId",
        hazardousSubstanceRiskAssessmentId: "#HazardousSubstanceRiskAssessmentId"
    };

    var getDataForNewFurtherControlMeasure = function (button) {
        return {
            companyId: $(selectors.companyId).val(),
            riskAssessmentHazardId: button.attr('data-rah-id')
        };
    };

    return {
        editFurtherControlMeasureTaskUrl: window.globalajaxurls.getGeneralRiskAssessmentEditFurtherControlMeasureTaskUrl,
        newFurtherControlMeasureTaskUrl: window.globalajaxurls.getGeneralRiskAssessmentNewFurtherControlMeasureTaskUrl,
        viewFurtherControlMeasureTaskUrl: window.globalajaxurls.getGeneralRiskAssessmentViewFurtherControlMeasureTaskUrl,
        reassignFurtherControlMeasureTaskUrl: window.globalajaxurls.getGeneralRiskAssessmentReassignFurtherControlMeasureTaskUrl,
        getDataForNewFurtherControlMeasure: getDataForNewFurtherControlMeasure,
        taskType: "gra"
    };
} ();

// Personal Risk Assessment Options
BusinessSafe.Tasks.PersonalRiskAssessment.Options = function () {

    var selectors = {
        companyId: "#CompanyId",
        hazardousSubstanceRiskAssessmentId: "#HazardousSubstanceRiskAssessmentId"
    };

    var getDataForNewFurtherControlMeasure = function (button) {
        return {
            companyId: $(selectors.companyId).val(),
            riskAssessmentHazardId: button.attr('data-rah-id')
        };
    };


    return {
        editFurtherControlMeasureTaskUrl: window.globalajaxurls.getPersonalRiskAssessmentEditFurtherControlMeasureTaskUrl,
        newFurtherControlMeasureTaskUrl: window.globalajaxurls.getPersonalRiskAssessmentNewFurtherControlMeasureTaskUrl,
        viewFurtherControlMeasureTaskUrl: window.globalajaxurls.getPersonalRiskAssessmentViewFurtherControlMeasureTaskUrl,
        reassignFurtherControlMeasureTaskUrl: window.globalajaxurls.getPersonalRiskAssessmentReassignFurtherControlMeasureTaskUrl,
        getDataForNewFurtherControlMeasure: getDataForNewFurtherControlMeasure,
        taskType: "pra"
    };
} ();

// Hazardous Substances Risk Assessment Options
BusinessSafe.Tasks.HazardousSubstanceRiskAssessment.Options = function () {

    var selectors = {
        companyId: "#CompanyId",
        hazardousSubstanceRiskAssessmentId: "#RiskAssessmentId",
        furtherControlMeasureTasksTable: "#FurtherControlMeasureTasksTable"
    };

    var getDataForNewFurtherControlMeasure = function (button) {
        return {
            companyId: $(selectors.companyId).val(),
            riskAssessmentId: $(selectors.hazardousSubstanceRiskAssessmentId).val()
        };
    };

    return {
        editFurtherControlMeasureTaskUrl: window.globalajaxurls.getHazardousSubstanceRiskAssessmentEditFurtherControlMeasureTaskUrl,
        newFurtherControlMeasureTaskUrl: window.globalajaxurls.getHazardousSubstanceRiskAssessmentNewFurtherControlMeasureTaskUrl,
        viewFurtherControlMeasureTaskUrl: window.globalajaxurls.getHazardousSubstanceRiskAssessmentViewFurtherControlMeasureTaskUrl,
        reassignFurtherControlMeasureTaskUrl: window.globalajaxurls.getHazardousSubstanceRiskAssessmentReassignFurtherControlMeasureTaskUrl,
        getDataForNewFurtherControlMeasure: getDataForNewFurtherControlMeasure,
        taskType: "hsra"
    };
} ();

// Fire Risk Assessment Options
BusinessSafe.Tasks.FireRiskAssessment.Options = function () {

    var selectors = {
        companyId: "#CompanyId",
        riskAssessmentId: "#RiskAssessmentId",
        furtherControlMeasureTasksTable: "#FurtherControlMeasureTasksTable"
    };

    var getDataForNewFurtherControlMeasure = function (button) {
        return {
            companyId: $(selectors.companyId).val(),
            riskAssessmentId: $(selectors.riskAssessmentId).val(),
            significantFindingId: $(button).data('id')
        };
    };

    return {
        editFurtherControlMeasureTaskUrl: window.globalajaxurls.getFireRiskAssessmentEditFurtherControlMeasureTaskUrl,
        newFurtherControlMeasureTaskUrl: window.globalajaxurls.getFireRiskAssessmentNewFurtherActionTaskUrl,
        viewFurtherControlMeasureTaskUrl: window.globalajaxurls.getFireRiskAssessmentViewFurtherControlMeasureTaskUrl,
        reassignFurtherControlMeasureTaskUrl: window.globalajaxurls.getFireRiskAssessmentReassignFurtherControlMeasureTaskUrl,
        getDataForNewFurtherControlMeasure: getDataForNewFurtherControlMeasure,
        taskType: "fra"
    };
} ();
