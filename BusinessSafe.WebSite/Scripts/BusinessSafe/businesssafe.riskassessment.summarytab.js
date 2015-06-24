var riskAssessorsLoader = function () {
    var _selectors = {
        companyId: "#CompanyId",
        riskAssessorsContainer: "#riskAssessorsContainer",
        riskAssessor: "#RiskAssessor",
        riskAssessorId: "#RiskAssessorId"
    };

    var _urls = {
        searchRiskAssessors: window.globalajaxurls.searchRiskAssessors,
        getRiskAssessors: window.globalajaxurls.getRiskAssessors
    };


    function load(riskAssessorsData) {

        if (riskAssessorsData.selectedSiteId === "") {
            $(_selectors.riskAssessorsContainer).fadeOut("slow");
            return;
        }

        var callback = function (callbackData) {

            $(_selectors.riskAssessor).combobox({
                selectedId: riskAssessorsData.selectedRiskAssessorId,
                initialValues: callbackData,
                url: _urls.getRiskAssessors,
                data: {
                    siteId: riskAssessorsData.selectedSiteId,
                    companyId: $(_selectors.companyId).val(),
                    pageLimit: 100
                }
            });

            // if new risk assessor list does not contain currently seelcted one, remove it
            var riskAssessorStillValid = false;
            for (var i = 0; i < callbackData.length; i++) {
                if (callbackData[i].value == riskAssessorsData.selectedRiskAssessorId) {
                    riskAssessorStillValid = true;
                    break;
                }
            }
            if (!riskAssessorStillValid) {
                $(_selectors.riskAssessor).val('--Select Option--');
                $(_selectors.riskAssessorId).val('');
            }

            $(_selectors.riskAssessorsContainer).fadeIn("slow");
        };

        var data = {
            siteId: riskAssessorsData.selectedSiteId,
            companyId: $(_selectors.companyId).val(),
            pageLimit: 100
        };

        AjaxCall.execute(_urls.getRiskAssessors, callback, data, "GET");
    }

    return {
        load: load
    };

} ();

// ******************************** FIRE RISK ASSESSMENT SUMMARY TAB ********************************************************//
BusinessSafe.FireRiskAssessment.SummaryTab = function () {
    var _selectors = {
    };

    var _urls = {
        save: '/FireRiskAssessments/Summary/Save',
        saveAndNext: '/FireRiskAssessments/Summary/SaveAndNext'
    };

    var _isReadOnlyInitialisation = function (initialisationModel) {
    };


    return {
        selectors: _selectors,
        urls: _urls,
        isReadOnlyInitialisation: function (model) {
            _isReadOnlyInitialisation(model);
        }
    };
} ();

// ******************************** PERSONAL RISK ASSESSMENT SUMMARY TAB ********************************************************//
BusinessSafe.PersonalRiskAssessment.SummaryTab = function () {
    var _selectors = {
        sensitiveCheckbox: 'input#Sensitive',
    };

    var _urls = {
        save: '/PersonalRiskAssessments/Summary/Save',
        saveAndNext: '/PersonalRiskAssessments/Summary/SaveAndNext'
    };

    var _isReadOnlyInitialisation = function (initialisationModel) {
    };

    var _handleSensitiveCheckbox = function () {
        $(_selectors.sensitiveCheckbox).on('click', function (event) {
            if($(this).attr('readonly') == 'readonly') {
                event.preventDefault();
                return;
            }
        });
    };

    _handleSensitiveCheckbox();

    return {
        selectors: _selectors,
        urls: _urls,
        isReadOnlyInitialisation: function (model) {
            _isReadOnlyInitialisation(model);
        }
    };
} ();

// ******************************** GENERAL RISK ASSESSMENT SUMMARY TAB ********************************************************//
BusinessSafe.GeneralRiskAssessment.SummaryTab = function () {
    var _selectors = {
    };

    var _urls = {
        save: '/GeneralRiskAssessments/Summary/Save',
        saveAndNext: '/GeneralRiskAssessments/Summary/SaveAndNext'
    };

    var _isReadOnlyInitialisation = function (initialisationModel) {
    };


    return {
        selectors: _selectors,
        urls: _urls,
        isReadOnlyInitialisation: function (model) {
            _isReadOnlyInitialisation(model);
        }
    };
} ();

// ******************************** HAZARDOUS RISK ASSESSMENT SUMMARY TAB ******************************************************//
BusinessSafe.HazardousSubstanceRiskAssessment.SummaryTab = function () {
    var _selectors = {
        companyId: '#companyId',
        hazardousSubstance: '#HazardousSubstance',
        hazardousSubstanceId: '#HazardousSubstanceId'
    };

    var _urls = {
        save: '/HazardousSubstanceRiskAssessments/Summary/Save',
        saveAndNext: '/HazardousSubstanceRiskAssessments/Summary/SaveAndNext',
        getHazardousSubstances: window.globalajaxurls.getHazardousSubstances
    };

    var _isReadOnlyInitialisation = function (initialisationModel) {
        $(_selectors.hazardousSubstance).combobox({
            selectedId: $(_selectors.hazardousSubstanceId).val(),
            initialValues: initialisationModel.hazardousSubstances,
            url: _urls.getHazardousSubstances,
            data: {
                companyId: $(_selectors.companyId).val(),
                pageLimit: 100
            }
        });
    };

    return {
        selectors: _selectors,
        urls: _urls,
        isReadOnlyInitialisation: function (model) {
            _isReadOnlyInitialisation(model);
        }
    };
} ();

// ******************************** SUMMARY TAB VIEW MODEL *********************************************************************//
BusinessSafe.RiskAssessment.SummaryTab.ViewModel = function (summaryTab, initialisationModel) {
    var selectors = {
        isReadOnly: '#IsReadOnly',
        validationSummary: '.validation-summary-errors',
        saveAndNextButtons: '#nextBtn, a.riskassessment-tab-links',
        riskAssessor: "#RiskAssessor",
        riskAssessorId: "#RiskAssessorId",
        saveButton: '#saveButton',
        companyId: '#CompanyId',
        site: "#Site",
        siteId: "#SiteId"
    };

    $(selectors.saveAndNextButtons).click(function (event) {
        var isReadOnly = $(selectors.isReadOnly);

        if (isReadOnly.length > 0) {
            return;
        }

        event.preventDefault();

        var destUrl = $(this).attr("href");
        var f = $('form:first');

        f.attr('action', summaryTab.urls.saveAndNext);
        $.post(f[0].action, f.serialize(), function (result) {
            if (result.Success === true) {
                window.location = destUrl;
            }
            else {
                if ($(selectors.validationSummary).size() === 0) {
                    f.prepend('<div><div class="validation-summary-errors alert alert-error"></div></div>');
                }
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

    $(selectors.saveButton).click(function (event) {
        event.preventDefault();
        var f = $('form:first');
        f.attr('action', summaryTab.urls.save);
        f.submit();
    });

    $(selectors.site).combobox({
        selectedId: $(selectors.siteId).val(),
        initialValues: initialisationModel.sites,
        url: '',
        data: {
            companyId: $(selectors.companyId).val(),
            pageLimit: 100
        },
        afterSelect: function () {

            if ($(selectors.siteId).val() === "") {
                $(selectors.riskAssessorId).val('');
                return false;
            }

            var riskAssessorsData = {
                selectedSiteId: $("#SiteId").val(),
                companyId: $(selectors.companyId).val()
            };

            riskAssessorsLoader.load(riskAssessorsData);
            return false;
        }
    });

    var selectedRiskAssessorId = $(selectors.riskAssessorId).val();
    var selectedSiteId = $(selectors.siteId).val();
    if (selectedSiteId !== "") {
        var riskAssessorsData = {
            selectedRiskAssessorId: selectedRiskAssessorId,
            selectedSiteId: selectedSiteId,
            companyId: $(selectors.companyId).val()
        };
        riskAssessorsLoader.load(riskAssessorsData);
    }

    summaryTab.isReadOnlyInitialisation(initialisationModel);
};