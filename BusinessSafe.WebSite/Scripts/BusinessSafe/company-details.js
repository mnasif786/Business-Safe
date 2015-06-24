var companyDetails = (function ($) {
    var _selectors = {
        companyId: '#Id',
        employeeId: '#BusinessSafeContactId',
        employeeName: '#BusinessSafeContact',
        form: '#company-details-form',
        modifyCompanyDetailsButton: '#modifyCompanyDetails',
        cancelButton: '#cancelButton',
        saveButton: '#notifyAdminButton'
    };

    var _initialise = function () {
        $(_selectors.modifyCompanyDetailsButton).click(function (e) {
            _modifyCompanyDetails(e);
        });

        $(_selectors.cancelButton).click(function (e) {
            _cancelModification(e);
        });
    };

    var _createEmployeeDropdown = function () {
        var url = window.globalajaxurls.getEmployeesWithNoJobTitle;
        var data = {
            filter: '',
            companyId: $(_selectors.companyId).val(),
            pageLimit: 500
        };
        var callback = function (employeeData) {

            employeeData.splice(0, 0, { label: '--Select Option--', value: '' });
            
            $(_selectors.employeeName).combobox({
                selectedId: $(_selectors.employeeId).val(),
                initialValues: employeeData,
                url: url,
                afterSelect: function () {
                },
                data: {
                    filter: '',
                    companyId: $(_selectors.companyId).val(),
                    pageLimit: 100
                }
            });
        };

        AjaxCall.execute(url, callback, data);
    };

    var _modifyCompanyDetails = function (e) {
        e.preventDefault();

        $('strong', _selectors.form).not(':hidden, #CanLabel').hide();
        $('input.hide').removeClass("hide").show();
        $('.alert').hide();

        $(_selectors.saveButton).removeAttr("disabled");
        $(_selectors.cancelButton).removeAttr("disabled");

        _createEmployeeDropdown();
    };

    var _cancelModification = function (e) {
        e.preventDefault();

        $(_selectors.saveButton).attr("disabled", "disabled");
        $(_selectors.cancelButton).attr("disabled", "disabled");

        $('strong:hidden', _selectors.form).show();
        $('input').addClass("hide");

        $('input').not('#CAN, #Id').each(function () {
            var originalValue = $(this).siblings('strong').text();
            $(this).val(originalValue);
        });
    };

    return {
        initialise: _initialise
    };
})($);