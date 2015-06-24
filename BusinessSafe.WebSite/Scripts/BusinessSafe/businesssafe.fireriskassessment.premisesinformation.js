BusinessSafe.FireRiskAssessment.PremisesInformation = function () {

    var selectors = {
        hidRiskAssessmentId: "#RiskAssessmentId",
        hidCompanyId: "#CompanyId",
        premisesProvidesSleepingAccommodationTrue: '#PremisesProvidesSleepingAccommodationTrue',
        premisesProvidesSleepingAccommodationFalse: '#PremisesProvidesSleepingAccommodationFalse',
        dialogSleepingAccommodation: "#dialogSleepingAccommodation",
        premisesProvidesSleepingAccommodationConfirmed: "#PremisesProvidesSleepingAccommodationConfirmed"
    };

    var urls = {
        index: "/FireRiskAssessments"
    };

    var initialize = function () {

        //if (!isReadOnly()) {
        //    
        //}


        $(selectors.premisesProvidesSleepingAccommodationTrue).click(function (event) {

            var riskAssessmentId = $(selectors.hidRiskAssessmentId).val();
            var companyId = $(selectors.hidCompanyId).val();

            $(selectors.dialogSleepingAccommodation).dialog({
                buttons: {
                    "Proceed": function () {
                        $(selectors.premisesProvidesSleepingAccommodationConfirmed).val(true);
                        $(this).dialog("close");
                    },
                    "Cancel": function () {
                        window.location.replace(urls.index + "?riskAssessmentId=" + riskAssessmentId + "&companyId=" + companyId);
                    }
                },
                modal: true,
                resizable: false,
                open: function () {
                    $(selectors.premisesProvidesSleepingAccommodationConfirmed).val(false);
                },
                close: function () {
                    if ($(selectors.premisesProvidesSleepingAccommodationConfirmed).val() != "true") {
                        window.location.replace(urls.index + "?riskAssessmentId=" + riskAssessmentId + "&companyId=" + companyId);
                    }
                }
            });
        });

        $(selectors.premisesProvidesSleepingAccommodationFalse).click(function (event) {
            $(selectors.premisesProvidesSleepingAccommodationConfirmed).val(null);
        });

        $("#nextBtn, a.riskassessment-tab-links").click(function (event) {
            var isReadOnly = $("#IsReadOnly");

            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();
            // prevent multiple submissions by removing the event handlers
            disableTabs(); 

            var destUrl = $(this).attr("href");
            var data = $('form:first');

            $.ajax({
                url: window.globalajaxurls.saveAndNextFireRiskAssessmentPremisesInformation,
                type: 'POST',
                data: data.serialize(),
                async: false,
                cache: false,
                timeout: 30000,
                error: function (jqXhr, textStatus, errorThrown) {
                    if (jqXhr.status !== 0) {
                        if (window.debugErrorHandler === undefined) {
                            window.location.replace(window.globalajaxurls.errorPage);
                        } else {
                            alert("DEBUG: AjaxCall.execute encountered a problem.");
                        }
                    } else {
                        window.location.reload();
                    }
                },
                success: function (msg) {
                    window.location = destUrl;
                    return;
                }
            });
        });


        var disableTabs = function () {
            $("a.riskassessment-tab-links").off();
            $("a.riskassessment-tab-links").on('click', function (event) {
                event.preventDefault();
            });
        };

    };

    return {
        initialize: initialize
    };
} ();