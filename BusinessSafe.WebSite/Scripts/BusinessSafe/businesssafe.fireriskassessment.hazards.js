var addNewOption = function (optionAdding, options) {
            
    var newOptionAdding = $(optionAdding).val();
    if (newOptionAdding.length === 0) return;

    var data = {
        isNew: true,
        companyDefaultId: 0,
        companyDefaultValue: newOptionAdding,
        companyDefaultType: options.companyDefaultType,
        runMatchCheck: false,
        companyId: $("#CompanyId").val(),
        riskAssessmentId: $("#RiskAssessmentId").val()
    };

    var successfulCallBack = function (data) {

        if (data.Success === true) {
            $(options.selectedOptionsContainer).
                append($("<option></option>").
                attr("value", data.Id).
                text(newOptionAdding));
            $(optionAdding).val('');
        }
        else {
            window.location.replace(window.globalajaxurls.errorPage);
        }
    };

    var existingMatchesCallBack = function () {
        /// Should not be called because the run match check is set to false
    };

    var url = window.globalajaxurls.createRiskAssessmentDefaults;
    AjaxCall.execute(url, successfulCallBack, data, "POST");

};

var hazardsManager = function () {
    function initialize() {

        var _self = this;
        _self.selectors = {
            saveHazardsButton: "#SaveButton",
            dialogCannotRemoveHazard: "#dialogCannotRemoveRiskAssessmentHazard",
            errorSaving: "#errorSaving"
        };

        $(this.selectors.saveHazardsButton).click(function (event) {
            event.preventDefault();

            $("#PeopleAtRiskIds option, #FireSafetyControlMeasureIds option, #SourceOfFuelsIds option, #SourceOfIgnitionIds option ").prop('selected', true);

            $("form").submit();

        });

        $("#nextBtn, a.riskassessment-tab-links").click(function (event) {

            var isReadOnly = $("#IsReadOnly");
            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();
            disableTabs();
            var destUrl = $(this).attr("href");
          
            var f = $("form");
            f.attr('action', window.globalajaxurls.saveAndNextFireRiskAssessmentHazards);
            
            $("#PeopleAtRiskIds option, #FireSafetyControlMeasureIds option, #SourceOfFuelsIds option, #SourceOfIgnitionIds option ").prop('selected', true);
            
            $.post(f[0].action, f.serialize(), function (result) {
                if (result.Success === true) {
                    window.location = destUrl;
                }
            }).error(
                function (jqXhr, textStatus, errorThrown) {
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

        var disableTabs = function () {
            $("#nextBtn, a.riskassessment-tab-links").off();
            $("#nextBtn, a.riskassessment-tab-links").on('click', function (event) {
                event.preventDefault();
            });
        };

        // People at risk
        var peopleAtRiskOptions = {
            id: "people-at-risk-multi-select",
            companyDefaultType: "PeopleAtRisk",
            selectedOptionsContainer: "#PeopleAtRiskIds"
        };

        $("#AddNewPersonAtRisk").live("click", function (event) {
            event.preventDefault();
            var newPersonAtRisk = $(this).prev('input');
            addNewOption(newPersonAtRisk, peopleAtRiskOptions);
        });

        PBS.Plugins.MultiSelectAndSearch({}, peopleAtRiskOptions);


        // Fire safety control measures
        var fireSafetyControlMeasuresOptions = {
            id: "fire-safety-control-measures-multi-select",
            companyDefaultType: "FireSafetyControlMeasure",
            selectedOptionsContainer: "#FireSafetyControlMeasureIds"
        };

        $("#AddNewFireSafetyControlMeasure").live("click", function (event) {
            event.preventDefault();
            var newFireSafetyControlMeasure = $(this).prev('input');
            addNewOption(newFireSafetyControlMeasure, fireSafetyControlMeasuresOptions);
        });

        PBS.Plugins.MultiSelectAndSearch({}, fireSafetyControlMeasuresOptions);


        // Source of fuel
        var sourceOfFuelOptions = {
            id: "source-of-fuel-multi-select",
            companyDefaultType: "SourceOfFuel",
            selectedOptionsContainer: "#SourceOfFuelsIds"
        };

        $("#AddNewSourceOfFuel").live("click", function (event) {
            event.preventDefault();
            var newSourceOfFuel = $(this).prev('input');
            addNewOption(newSourceOfFuel, sourceOfFuelOptions);
        });

        PBS.Plugins.MultiSelectAndSearch({}, sourceOfFuelOptions);

        // Source of ignition                
        var sourceOfIgnitionOptions = {
            id: "source-of-ignition-multi-select",
            companyDefaultType: "SourceOfIgnition",
            selectedOptionsContainer: "#SourceOfIgnitionIds"
        };

        $("#AddNewSourceOfIgnition").live("click", function (event) {
            event.preventDefault();
            var newSourceOfIgnition = $(this).prev('input');
            addNewOption(newSourceOfIgnition, sourceOfIgnitionOptions);
        });

        PBS.Plugins.MultiSelectAndSearch({}, sourceOfIgnitionOptions);

    }

    return {
        initialize: initialize
    };
} ();