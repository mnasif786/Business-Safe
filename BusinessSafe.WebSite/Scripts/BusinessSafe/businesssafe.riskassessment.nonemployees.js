BusinessSafe.RiskAssessment.NonEmployees = function () {

    var _attachNonEmployee = function (nonEmployeeId, riskAssessmentId, formattedName, linkToCompanyId) {

        if (nonEmployeeId != "") {

            var url = window.globalajaxurls.attachNonEmployeeToGenerlRiskAssessment;
            var data = {
                nonEmployeeId: nonEmployeeId,
                companyId: linkToCompanyId,
                riskAssessmentId: riskAssessmentId
            };

            var successfulCallBack = function(data) {
                var selectors = {
                    nonEmployeeMultiSelect: "#nonEmployeesMultiSelect",
                    nonEmployeeSearch: "#nonEmployeesName",
                    newNonEmployeeMultiSelectOption: "#nonEmployeesMultiSelect option",
                    nonEmployeeRemoveBtn: "#removeNonEmployeesBtn"
                };

                $(selectors.nonEmployeeMultiSelect).append($("<option></option>").attr("value", nonEmployeeId).text(formattedName));
                $(selectors.nonEmployeeSearch).autocomplete("close").val('');
                $(selectors.newNonEmployeeMultiSelectOption).click(function() {
                    $(selectors.nonEmployeeRemoveBtn).removeClass("hide");
                });
            };

            AjaxCall.execute(url, successfulCallBack, data, "POST");
        }
    };

    var _detachNonEmployees = function (riskAssessmentId, companyId) {

        var getData = function () {
            var result = {};
            result.riskAssessmentId = riskAssessmentId;
            result.nonEmployeeIds = new Array();
            result.companyId = companyId;
            $('#nonEmployeesMultiSelect option:selected').each(function (idx, el) {
                result.nonEmployeeIds[idx] = String(el.value);
            });
            return result;
        };

        $.ajax({
            url: window.globalajaxurls.detachNonEmployeeFromGeneralRiskAssessment,
            type: "POST",
            dataType: "json",
            data: getData(),
            traditional: true,
            success: function (data) {
                $('#nonEmployeesMultiSelect option:selected').remove();
                $("#removeNonEmployeesBtn").addClass("hide");
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });
    };

    return {
        attachNonEmployee: _attachNonEmployee,
        detachNonEmployees: _detachNonEmployees
    };
} ();

BusinessSafe.RiskAssessment.NonEmployees.ViewModel = function (riskAssessment) {
    var self = this;
    var nonEmployees = '';
    var riskAssessmentId = $("#RiskAssessmentId").val();
    var companyId = $("#CompanyId").val();

    var successfulCallBack = function (data) {
        nonEmployees = data;
        $(".attachNonEmployeeAutoComplete").combobox({
            selectedId: '',
            initialValues: data,
            addDefaultOption: true,
            url: window.globalajaxurls.getNonEmployeesNotAttachedToGeneralRiskAssessment,
            data: {
                riskAssessmentId: riskAssessmentId,
                companyId: companyId,
                pageLimit: 100
            },
            afterSelect: function (event, ui) {

                var nonEmployeeName = ui.item.label;
                var nonEmployeeId = ui.item.value;

                BusinessSafe.RiskAssessment.NonEmployees.attachNonEmployee(nonEmployeeId, riskAssessment.Id, nonEmployeeName, riskAssessment.CompanyId);

                nonEmployees = jQuery.grep(nonEmployees, function (nonemployee) { return nonemployee.value != nonEmployeeId; });

                $('.attachNonEmployeeAutoComplete').combobox('RemoveItemFromInitialValues', nonEmployees);

                return false;
            },
            afterSearch: function (results) {
                if (results[0] === "No Results") {
                    $(".addNewNonEmployee").removeAttr("disabled", "disabled");
                    return;
                }

                if (results[0] !== "No Results") {
                    $(".addNewNonEmployee").attr("disabled", "disabled");
                    return;
                }
            }
        });
    };
    var url = window.globalajaxurls.getNonEmployeesNotAttachedToGeneralRiskAssessment;
    var data = {
        term: '',
        companyId: companyId,
        riskAssessmentId: riskAssessmentId,
        pageLimit: 100
    };
    AjaxCall.execute(url, successfulCallBack, data);



    $(".detachNonEmployee").click(function (event) {
        event.preventDefault();

        BusinessSafe.RiskAssessment.NonEmployees.detachNonEmployees(riskAssessment.Id, riskAssessment.CompanyId);
      
        var result = {};
        result.riskAssessmentId = riskAssessmentId;
        result.employeeIds = [];
        result.companyId = companyId;
        $('#nonEmployeesMultiSelect option:selected').each(function (idx, el) {

            var nonEmployee = {};
            nonEmployee.label = el.label;
            nonEmployee.value = el.value;
            nonEmployee.filterName = null;
            nonEmployee.fiterValue = null;

            nonEmployees.push(nonEmployee);

            result.employeeIds[idx] = String(el.value);
        });
    });

    $(".nonEmployeesMultiSelect option, .nonEmployeesMultiSelect").click(function () {
        if ($(".nonEmployeesMultiSelect option:selected").length === 0) {
            return;
        }
        $(".detachNonEmployee").removeClass("hide");
    });

    $(".addNewNonEmployee").click(function (event) {
        event.preventDefault();

        var nameToAdd = $("#nonEmployeesName").val();
        var successfullyCreatedNonEmployeeCallback = function (nonEmployeeId, formattedName) {

            BusinessSafe.RiskAssessment.NonEmployees.attachNonEmployee(nonEmployeeId, riskAssessment.Id, formattedName, riskAssessment.CompanyId);

            $("#dialogAddNonEmployee").dialog("close");

            return false;

        };

        addNonEmployeeManager.initialize(riskAssessment.Id, nameToAdd, successfullyCreatedNonEmployeeCallback);
    });

    return self;
};