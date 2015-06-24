BusinessSafe.RiskAssessment.Employees = function () {

    var _attachEmployee = function (employeeId, riskAssessmentId, formattedName, linkToCompanyId) {
        if (employeeId != "") {
            var url = window.globalajaxurls.attachEmployeeToGeneralRiskAssessment;
            var data = {
                employeeId: employeeId,
                companyId: linkToCompanyId,
                riskAssessmentId: riskAssessmentId
            };

            var successfulCallBack = function(data) {

                var selectors = {
                    employeeMultiSelect: "#employeesMultiSelect",
                    employeeSearch: "#employeesName",
                    newEmployeeMultiSelectOption: "#employeesMultiSelect option",
                    employeeRemoveBtn: "#removeEmployeesBtn"
                };

                $(selectors.employeeMultiSelect).append($("<option></option>").attr("value", employeeId).text(formattedName));
                $(selectors.employeeSearch).autocomplete("close").val('');
                $(selectors.newEmployeeMultiSelectOption).click(function() {
                    $(selectors.employeeRemoveBtn).removeClass("hide");
                });
            };

            AjaxCall.execute(url, successfulCallBack, data, "POST");
        }
    };

    var _detachEmployees = function (riskAssessmentId, companyId) {

        var getData = function () {
            var result = {};
            result.riskAssessmentId = riskAssessmentId;
            result.employeeIds = [];
            result.companyId = companyId;
            $('#employeesMultiSelect option:selected').each(function (idx, el) {
                result.employeeIds[idx] = String(el.value);
            });
            return result;
        };

        $.ajax({
            url: window.globalajaxurls.detachEmployeeFromGeneralRiskAssessment,
            type: "POST",
            dataType: "json",
            data: getData(),
            traditional: true,
            success: function (data) {
                $('#employeesMultiSelect option:selected').remove();
                $("#removeEmployeesBtn").addClass("hide");
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });

    };

    return {
        attachEmployee: _attachEmployee,
        detachEmployees: _detachEmployees
    };
} ();


BusinessSafe.RiskAssessment.Employees.ViewModel = function (riskAssessment) {
    var self = this;
    var employees = '';
    var riskAssessmentId = $("#RiskAssessmentId").val();
    var companyId = $("#CompanyId").val();
    var url = window.globalajaxurls.getEmployeesNotAttachedToRiskAssessment;
    var data = {
        term: '',
        companyId: companyId,
        riskAssessmentId: riskAssessmentId,
        pageLimit: 500
    };

    var successfulCallBack = function (data) {
        employees = data;

        $(".attachEmployeeAutoComplete").combobox({
            selectedId: '',
            initialValues: data,
            addDefaultOption: true,
            url: window.globalajaxurls.getEmployeesNotAttachedToRiskAssessment,
            data: {
                riskAssessmentId: riskAssessmentId,
                companyId: companyId,
                pageLimit: 500
            },
            afterSelect: function (event, ui) {

                var employeeName = ui.item.label;
                var employeeId = ui.item.value;

                BusinessSafe.RiskAssessment.Employees.attachEmployee(employeeId, riskAssessment.Id, employeeName, riskAssessment.CompanyId);

                employees = jQuery.grep(employees, function (employee) { return employee.value != employeeId; });

                $('.attachEmployeeAutoComplete').combobox('RemoveItemFromInitialValues', employees);

                return false;
            }
        });

    };

    AjaxCall.execute(url, successfulCallBack, data);



    $(".detachEmployee").click(function (event) {
        event.preventDefault();

        BusinessSafe.RiskAssessment.Employees.detachEmployees(riskAssessment.Id, riskAssessment.CompanyId);

        var result = {};
        result.riskAssessmentId = riskAssessmentId;
        result.employeeIds = [];
        result.companyId = companyId;
        $('#employeesMultiSelect option:selected').each(function (idx, el) {

            var employee = {};
            employee.label = el.label;
            employee.value = el.value;
            employee.filterName = null;
            employee.fiterValue = null;

            employees.push(employee);

            result.employeeIds[idx] = String(el.value);
        });


    });

    $(".employeesMultiSelect option, .employeesMultiSelect").click(function () {
        if ($(".employeesMultiSelect option:selected").length === 0) {
            return;
        }
        $(".detachEmployee").removeClass("hide");
    });

    return self;
};
