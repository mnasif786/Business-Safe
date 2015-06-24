BusinessSafe.HazardousSubstanceRiskAssessment.Assessment = function () {
    var selectors = {
        selectedHazardGroupRadioButton: "[name='HazardGroup']:checked",
        substanceQunatityQuestion: ".substance-quantity-question",
        solidSubstanceQunatityQuestionLabel: ".solid-substance-quantity-question-label",
        liquidSubstanceQunatityQuestionLabel: ".liquid-substance-quantity-question-label",
        quantityRadioButton: "[name='Quantity']",
        selectedQuantityRadioButton: "[name='Quantity']:checked",
        matterStateRadioButton: "[name='MatterState']",
        selectedMatterStateRadioButton: "[name='MatterState']:checked",
        dustinessOrVolatility: "[name='DustinessOrVolatility']",
        selectedDustinessOrVolatility: "[name='DustinessOrVolatility']:checked",
        controlSystemLabel: "#WorkApproach",
        controlSystemGuidanceNotesLink: "#GuidanceNotes",
        saveAndNextButtons: '#nextBtn, a.riskassessment-tab-links',
        riskAssessmentId: "#RiskAssessmentId",
        controlSystemId: "#ControlSystemId",
        isReadOnly: "#IsReadOnly"
    };

    var urls = {
        saveAndNext: '/HazardousSubstanceRiskAssessments/Assessment/SaveAndNext'
    };

    function initialize() {
        $(selectors.matterStateRadioButton).on("change", function (event) {
            var substanceType = $(this).val();

            setSubstanceQuantityQuestionVisibility(substanceType);
            calculateControlSystem();
        });

        $(selectors.quantityRadioButton).on("change", function (event) {
            calculateControlSystem();
        });

        $(selectors.dustinessOrVolatility).on("change", function (event) {
            calculateControlSystem();
        });

        var setSubstanceQuantityQuestionVisibility = function (substanceType) {
            $(selectors.substanceQunatityQuestion).slideUp('slow', function () {
                if (substanceType === "Solid") {
                    $(selectors.solidSubstanceQunatityQuestionLabel).show();
                    $(selectors.liquidSubstanceQunatityQuestionLabel).hide();
                } else {
                    $(selectors.liquidSubstanceQunatityQuestionLabel).show();
                    $(selectors.solidSubstanceQunatityQuestionLabel).hide();
                }

                $(selectors.substanceQunatityQuestion).slideDown();
            });
        };

        var selectedSubstanceType = $(selectors.selectedMatterStateRadioButton);

        if (selectedSubstanceType.length > 0) {
            var substanceType = selectedSubstanceType.val();

            setSubstanceQuantityQuestionVisibility(substanceType);
        }

        var calculateControlSystem = function () {
            var hazardGroup = $(selectors.selectedHazardGroupRadioButton).val();
            var quantity = $(selectors.selectedQuantityRadioButton).val();
            var matterState = $(selectors.selectedMatterStateRadioButton).val();
            var dustinessOrVolatility = $(selectors.selectedDustinessOrVolatility).val();
            var riskAssessmentId = $(selectors.riskAssessmentId).val();

            var data = {
                hazardousSubstanceGroupCode: hazardGroup,
                matterState: matterState,
                quantity: quantity,
                dustinessOrVolatility: dustinessOrVolatility,
                riskAssessmentId: riskAssessmentId
            };

            if (data.matterState === undefined || data.dustinessOrVolatility === undefined || data.quantity === undefined) {
                return;
            }

            var successfulCallBack = function (result) {
                $(selectors.controlSystemLabel).fadeOut("fast").html('').html(result.ControlSystem).fadeIn('slow');
                $(selectors.controlSystemId).val(result.ControlSystemId);

                if (result.ControlSystem != "None") {
                    $(selectors.controlSystemGuidanceNotesLink).attr('href', result.Url).show();
                } else {
                    $(selectors.controlSystemGuidanceNotesLink).hide();
                }
            };

            AjaxCall.execute(window.globalajaxurls.getHazardousSubstanceRiskAssessmentControlSystem, successfulCallBack, data);
        };

        $(selectors.substanceQunatityQuestion).hide();

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

        calculateControlSystem();
    }

    return {
        initialize: initialize
    };
} ();