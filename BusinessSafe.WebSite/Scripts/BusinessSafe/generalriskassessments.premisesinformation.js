var premisesInformationManager = function () {
    var selectors = {
        isReadOnly: '#IsReadOnly',
        premisesInformationTabContentHolder: "#PremisesInformationTabContentHolder",
        dateOfAssessment: "#DateOfAssessment",
        locationAreaDepartment: "#LocationAreaDepartment",
        taskProcessDescription: "#TaskProcessDescription",
        savedSuccessfully: "#savedSuccessfully",
        errorSaving: "#errorSaving",
        loadNextTabPremisesInformation: "#loadNextTabPremisesInformation"
    };

    function initialize() {
        $(".active").removeClass("active");
        $(".premisesinformationlink").parent().addClass("active");

        $("#nextBtn, a.riskassessment-tab-links").click(function (event) {
            var isReadOnly = $("#IsReadOnly");

            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();

            var destUrl = $(this).attr("href");
            var f = $('form:first');

            f.attr('action', window.globalajaxurls.saveAndNextGeneralRiskAssessmentPremisesInformation);
            $.post(f[0].action, f.serialize(), function (result) {

                if (result.Success === true) {
                    window.location = destUrl;
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

        $("#RiskAssessorEmployeeId-button").css("width", "450px");

        var riskAssessment = {
            Id: $("#RiskAssessmentId").val(),
            CompanyId: $("#CompanyId").val()
        };

        if (riskAssessment.Id !== '') {
            $("#myTab li").find("a.hazardspeoplelink").removeClass("disable");
        }

        BusinessSafe.RiskAssessment.Employees.ViewModel(riskAssessment);
        BusinessSafe.RiskAssessment.NonEmployees.ViewModel(riskAssessment);
    }

    return {
        initialize: initialize
    };
} ();