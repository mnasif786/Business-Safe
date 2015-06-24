BusinessSafe.AccidentRecord.InjuredPerson = function() {
    var _formInputs = {
        isReadOnly: '#IsReadOnly',
        employeeInvolvedSelector: '#PersonInvolvedTypeEmployee',
        visitorInvolvedSelector: '#PersonInvolvedTypeVisitor',
        personAtWorkInvolvedSelector: '#PersonInvolvedTypePersonAtWork',
        otherPersonInvolvedSelector: '#PersonInvolvedTypeOther',
        employee: '#Employee',
        companyId: '#CompanyId',
        country: "#Country",
        countryId: "#CountryId",
        othersInvolved: "#PersonInvolvedOtherDescription",
        othersInvolvedId: "#PersonInvolvedOtherDescriptionId",
        saveAndNextButtons: '#nextBtn, a.riskassessment-tab-links',
        validationSummary: '.validation-summary-errors'
    };
    var elements = {
        employeeInvolvedSelect: '#employee-involved-select',
        otherPersonInvolvedType: '#other-person-involved-type',
        nonEmployeeInvolvedDetails: '#non-employee-details',
        otherPersonInvolvedOtherDescription: '#other-person-involved-other-description'
    };

    var _hideEmployeeInvolvedSelector = function() {
        $(elements.employeeInvolvedSelect).hide();
        // reset values before post?
    };

    var _hideOtherPersonTypeInput = function() {
        $(elements.otherPersonInvolvedType).hide();
        // reset values before post?
    };

    var _hideNonEmployeeDetails = function() {
        $(elements.nonEmployeeInvolvedDetails).hide();
        // reset values before post?
    };

    var _hideOtherPersonOtherDetails = function() {
        $(elements.otherPersonInvolvedOtherDescription).hide();
    };

    var _showEmployeeInvolvedSelector = function() {
        $(elements.employeeInvolvedSelect).fadeIn();
    };

    var _showOtherPersonTypeInput = function() {
        $(elements.otherPersonInvolvedType).fadeIn().find('input').focus();
    };

    var _showOtherPersonOtherDetails = function () {
        $(elements.otherPersonInvolvedOtherDescription).fadeIn();
    };

    var _showNonEmployeeInvolvedDetails = function() {
        $(elements.nonEmployeeInvolvedDetails).fadeIn();
    };

    var _focusNonEmployeeInvolvedDetails = function() {
        $(elements.nonEmployeeInvolvedDetails).find('input:first').focus();
    };

    var _setupDisplayingPersonInvolvedInputs = function() {
        _hideEmployeeInvolvedSelector();
        _hideOtherPersonTypeInput();
        _hideNonEmployeeDetails();

        if ($(_formInputs.employeeInvolvedSelector).attr('checked') === 'checked') {
            _showEmployeeInvolvedSelector();
            _hideOtherPersonTypeInput();
            _hideNonEmployeeDetails();
            _hideOtherPersonOtherDetails();
        }

        if ($(_formInputs.visitorInvolvedSelector).attr('checked') === 'checked') {
            _hideEmployeeInvolvedSelector();
            _hideOtherPersonTypeInput();
            _showNonEmployeeInvolvedDetails();
            _hideOtherPersonOtherDetails();
        }

        if ($(_formInputs.personAtWorkInvolvedSelector).attr('checked') === 'checked') {
            _hideEmployeeInvolvedSelector();
            _hideOtherPersonTypeInput();
            _showNonEmployeeInvolvedDetails();
            _hideOtherPersonOtherDetails();
        }

        if ($(_formInputs.otherPersonInvolvedSelector).attr('checked') === 'checked') {
            _hideEmployeeInvolvedSelector();
            _showOtherPersonTypeInput();
            _showNonEmployeeInvolvedDetails();
            _showOtherPersonOtherDetails();
        }
        
        $(_formInputs.employeeInvolvedSelector).click(function() {
            _showEmployeeInvolvedSelector();
            _hideOtherPersonTypeInput();
            _hideNonEmployeeDetails();
            _hideOtherPersonOtherDetails();
        });

        $(_formInputs.visitorInvolvedSelector).click(function() {
            _hideEmployeeInvolvedSelector();
            _hideOtherPersonTypeInput();
            _showNonEmployeeInvolvedDetails();
            _hideOtherPersonOtherDetails();
        });

        $(_formInputs.personAtWorkInvolvedSelector).click(function() {
            _hideEmployeeInvolvedSelector();
            _hideOtherPersonTypeInput();
            _showNonEmployeeInvolvedDetails();
            _hideOtherPersonOtherDetails();
        });

        $(_formInputs.otherPersonInvolvedSelector).click(function() {
            _hideEmployeeInvolvedSelector();
            _showOtherPersonTypeInput();
            _showNonEmployeeInvolvedDetails();

            if (displayOtherDescriptionTextBox()) {
                _showOtherPersonOtherDetails();
            } else {
                _hideOtherPersonOtherDetails();
            }
        });


    };

    var _setupEmployeesCombobox = function(employees) {
        $(_formInputs.employee).combobox({
            selectedId: $(_formInputs.employee + "Id").val(),
            initialValues: employees,
            url: window.globalajaxurls.getEmployees,
            afterSelect: function() {
            },
            data: {
                companyId: $(_formInputs.companyId).val(),
                pageLimit: 500
            }
        });
    };

    var setupCountriesComboBox = function(countries) {
        $(_formInputs.country).combobox({
            selectedId: $(_formInputs.countryId).val(),
            initialValues: countries,
            url: '',
            afterSelect: function() {
            },
            data: {
                companyId: $(_formInputs.companyId).val(),
                pageLimit: 100
            }
        });
    };

    var setupOthersInvolvedComboBox = function(othersInvolved) {
        $(_formInputs.othersInvolved).combobox({
            selectedId: $(_formInputs.othersInvolvedId).val(),
            initialValues: othersInvolved,
            url: '',
            afterSelect: function() {
                if (displayOtherDescriptionTextBox()) {
                    _showOtherPersonOtherDetails();
                } else {
                    _hideOtherPersonOtherDetails(); }
            },
            data: {
                companyId: $(_formInputs.companyId).val(),
                pageLimit: 100
            }
        });
    };

    var setupSaveAndNextButtons = function() {
        $(_formInputs.saveAndNextButtons).click(function(event) {
            var isReadOnly = $(_formInputs.isReadOnly);

            if (isReadOnly.length > 0) {
                return;
            }

            event.preventDefault();

            var destUrl = $(this).attr("href");
            var f = $('form:first');

            $.post('/AccidentReports/InjuredPerson/SaveAndNext', f.serialize(), function(result) {
                if (result.Success === true) {
                    window.location = destUrl;
                } else {
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

    var setInitialDisplay = function() {
        if (displayOtherDescriptionTextBox()) {
            _showOtherPersonOtherDetails();
        } else {
            _hideOtherPersonOtherDetails();
        }
    };

    var displayOtherDescriptionTextBox = function() {
        return ($(_formInputs.othersInvolvedId).val() == 9);
    };

    var _initialise = function (employees, countries, othersInvolved) {
        _setupDisplayingPersonInvolvedInputs();
        _setupEmployeesCombobox(employees);
        setupCountriesComboBox(countries);
        setupOthersInvolvedComboBox(othersInvolved);
        setupSaveAndNextButtons();
        setInitialDisplay();
    };

    return {
        initialise: _initialise
    };
}();