BusinessSafe.AccidentRecord.AccidentDetails = function () {
    var _formInputs = {
        isReadOnly: '#IsReadOnly',
        companyId: '#CompanyId',
        isOtherAccidentType: '#IsOtherAccidentType',
        isOtherAccidentCause: '#IsOtherAccidentCause',
        firstAidAdministered: 'input#FirstAidAdministered',
        unableToPerformDuties: 'input[name="UnableToPerformDuties"]',
        firstAider: '#FirstAiderEmployee',
        site: '#Site',
        accidentType: '#AccidentType',
        accidentCause: '#AccidentCause',
        saveAndNextButtons: '#nextBtn, a.riskassessment-tab-links',
        validationSummary: '.validation-summary-errors',
        recordStatus: "#IsClosed"
    };
    var elements = {
        offSiteName: '#offsite-name',
        otherAccidentType: '#other-accident-type',
        otherAccidentCause: '#other-accident-cause',
        firstAidInformation: '#details-of-first-aid',
        nonEmployeeFirstAiderName: '#non-employee-firstaider-name',
        showNonEmployeeFirstAidInputs: '#ShowNonEmployeeFirstAidInputs'
    };

    var setElementDisplay = function (show, element) {
        if (show) {
            $(element).fadeIn();
        } else {
            $(element).hide();
        }
    };

    var nonEmployeeFirstAiderId = '00000000-0000-0000-0000-000000000000';
    var nonEmployeeFirstAiderName = 'Non-employee';

    _setInitialDisplay = function () {
        setElementDisplay($(_formInputs.site + "Id").val() == -1, elements.offSiteName);
        setElementDisplay($(_formInputs.accidentType + "Id").val() == 16, elements.otherAccidentType);
        setElementDisplay($(_formInputs.accidentCause + "Id").val() == 14, elements.otherAccidentCause);
        setElementDisplay($(_formInputs.firstAider + "Id").val() == nonEmployeeFirstAiderId, elements.nonEmployeeFirstAiderName);
        setElementDisplay($(_formInputs.firstAidAdministered + ':checked').val() == 'true', elements.firstAidInformation);
    };

    var _setupTogglableInputs = function () {

        $(_formInputs.firstAidAdministered).change(function () {

            if ($(this).val() == "true") {
                $(elements.firstAidInformation).fadeIn();
            } else {
                $(elements.firstAidInformation).hide();
            }
        });
    };

    var _setupComboboxes = function (employees, sites, types, causes) {
        $(_formInputs.firstAider).combobox({
            selectedId: $(_formInputs.firstAider + "Id").val(),
            initialValues: employees,
            afterSelect: function () {
                var isNonEmployee = $(_formInputs.firstAider + "Id").val() == nonEmployeeFirstAiderId;
                setElementDisplay(isNonEmployee, elements.nonEmployeeFirstAiderName);
            }
        });
        $(_formInputs.site).combobox({
            selectedId: $(_formInputs.site + "Id").val(),
            initialValues: sites,
            afterSelect: function () {
                var isOffsite = $(_formInputs.site + "Id").val() == -1;
                setElementDisplay(isOffsite, elements.offSiteName);
            }
        });
        $(_formInputs.accidentType).combobox({
            selectedId: $(_formInputs.accidentType + "Id").val(),
            initialValues: types,
            afterSelect: function () {
                var isOtherAccidentType = $(_formInputs.accidentType + "Id").val() == 16;
                setElementDisplay(isOtherAccidentType, elements.otherAccidentType);
            }
        });
        $(_formInputs.accidentCause).combobox({
            selectedId: $(_formInputs.accidentCause + "Id").val(),
            initialValues: causes,
            afterSelect: function () {
                var isOtherAccidentCause = $(_formInputs.accidentCause + "Id").val() == 14;
                setElementDisplay(isOtherAccidentCause, elements.otherAccidentCause);
            }
        });
    };

    var _setupSaveAndNextButtons = function () {
        $(_formInputs.saveAndNextButtons).click(function (event) {
            var isReadOnly = $(_formInputs.isReadOnly);

            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();

            var destUrl = $(this).attr("href");
            var f = $('form:first');

            $.post('/AccidentReports/AccidentDetails/SaveAndNext', f.serialize(), function (result) {
                if (result.Success === true) {
                    window.location = destUrl;
                }
                else {
                    if ($(_formInputs.validationSummary).size() === 0) {
                        f.prepend('<div><div class="validation-summary-errors alert alert-error"></div></div>');
                    }
                    showValidationErrors(_formInputs.validationSummary, result.Errors);
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
    };

    var _setupFirstAidSection = function () {
        if ($(elements.showNonEmployeeFirstAidInputs).val() == 'True') {
            $(_formInputs.firstAider + 'Id').val(nonEmployeeFirstAiderId);
            $(_formInputs.firstAider).val(nonEmployeeFirstAiderName);
            $(elements.nonEmployeeFirstAiderName).fadeIn();
        }
    };
    var _initialise = function (employees, sites, types, causes) {
        _setInitialDisplay();
        _setupTogglableInputs();
        _setupComboboxes(employees, sites, types, causes);
        _setupSaveAndNextButtons();
        _setupFirstAidSection();

    };

    return {
        initialise: _initialise
    };
} ();