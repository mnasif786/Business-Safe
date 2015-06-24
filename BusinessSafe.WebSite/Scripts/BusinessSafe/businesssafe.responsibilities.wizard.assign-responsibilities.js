BusinessSafe.Responsibilities.Wizard.AssignResponsibilities = function () {

    var _multiSelectAndSearch;

    var selectors = {
        responsibilitiesList: '#select-responsibilities',
        checkbox: 'td input[type="checkbox"]',
        selectedSites: '#SiteIds',
        form: 'form',
        submitButton: 'input[type=submit]',
        responsibilityTemplateRow: '.responsibilityTemplateRow',
        owners: "div.owner",
        frequencies: "div.frequency",
        frequency: 'input[type="hidden"].frequency',
        responsiblePerson: 'input[type="hidden"].owner',
        generateTasksTab: '#nav-generate-tasks',
        goToGenerateTasksButton: '#nextBtn',
        validationMessage: '#validation-message',
        resetSitesButon: '#reset',
        selectAllResponsbilitiesButton: '.responsibilitySelector'
    };

    var _setupResponsibilitySelector = function () {
        $(selectors.checkbox, selectors.responsibilitiesList).on('click', function () {
            var checkbox = $(this);
            var tableRow = checkbox.closest('tr');
            if (checkbox.is(':checked')) {
                _enableResponsibilty(tableRow);
            } else {
                _disableResponsibilty(tableRow);
            }
        });
    };

    var _enableAllResponsibilties = function (table) {
        $(table).find('tr').each(function (index) {
            _enableResponsibilty($(this));
        });
    };

    var _enableResponsibilty = function (tableRow) {
        tableRow.children().each(function () {
            $(this).find('input[type="text"]').removeAttr('disabled');
            $(this).find('.btn').removeAttr('disabled');
        });
    };

    var _disableAllResponsibilties = function (table) {
        $(table).find('tr').each(function (index) {
            _disableResponsibilty($(this));
        });
    };

    var _disableResponsibilty = function (tableRow) {
        tableRow.children().each(function () {
            $(this).find('input[type="text"]').attr('disabled', 'disabled');
            $(this).find('.btn').attr('disabled', 'disabled');
        });
    };

    var _setupSitesMultiSelector = function () {
        var eventBindingObject = {};
        var options = {
            id: "sites-multi-select"
        };
        _multiSelectAndSearch = new PBS.Plugins.MultiSelectAndSearch(eventBindingObject, options);
    };

    var _getSelections = function () {

        // set options moved to selected sites as selected, so we can use val()
        $(selectors.selectedSites + ' option').each(function () {
            $(this).attr('selected', 'selected');
        });
        var sites = $(selectors.selectedSites).val();

        var responsibilities = [];
        $(selectors.checkbox + ':checked').each(function () {
            var currCheckbox = $(this);
            responsibilities.push({
                ResponsibilityTemplateId: currCheckbox.val(),
                FrequencyId: currCheckbox.parent().siblings().find(selectors.frequency).val(),
                ResponsiblePersonEmployeeId: currCheckbox.parent().siblings().find(selectors.responsiblePerson).val()
            });
        });

        return {
            SiteIds: sites,
            Responsibilities: responsibilities
        };
    };

    var _generateResponsibilities = function (selections) {
        $(selectors.submitButton).attr('disabled', 'disabled');

        var url = window.globalajaxurls.generateResponsibiltiesFromSiteAndTemplateUrl;
        var successCallBack = function (data) {
            if (data.Success === true) 
            {
                _resetForm();
                $(selectors.form).delay(500).before('<div class="alert alert-success">Your tasks have been generated.</div>');
                $('.alert-success').delay(3000).fadeOut('slow', function () 
                {
                    $(this.remove());
                });

                $(selectors.goToGenerateTasksButton).removeClass('hide');
                $(selectors.goToGenerateTasksButton).addClass('btn btn-primary pull-right');
                $(selectors.generateTasksTab).removeClass('hide');


                window.scrollTo($(selectors.goToGenerateTasksButton).position().left, $(selectors.goToGenerateTasksButton).position().top);
                
                
            } else {
                $(selectors.validationMessage).show();
                showValidationErrors(selectors.validationMessage, data.Errors);
                _enableSubmitButton();
            }
        };
        AjaxCall.execute(url, successCallBack, JSON.stringify(selections), "POST", 'application/json; charset=utf-8');
    };

    var _enableSubmitButton = function () {
        $(selectors.submitButton).removeAttr('disabled');
    };

    var _setupGenerateResponsibilityButton = function () {
        $(selectors.form).submit(function (event) {
            event.preventDefault();

            var selections = _getSelections();

            _generateResponsibilities(selections);
        });
    };

    var _resetComboBox = function (hiddenField) {
        var initialValue = hiddenField.data('initial-value');
        hiddenField.val(initialValue);

        var displayTextField = hiddenField.siblings('input[type="text"]');
        var displayTextFieldInitialValue = displayTextField.data('initial-value');
        displayTextField.val(displayTextFieldInitialValue);
    };

    var _resetForm = function () {
        _enableSubmitButton();
        $(selectors.validationMessage).hide();
        $('select', selectors.responsibilitiesList).attr('disabled', 'disabled');
        $('input[type="checkbox"]', selectors.responsibilitiesList).removeAttr('checked');

        // save options from selected sites and dis
        var ids = _multiSelectAndSearch.getSelectedOptions();
        $(selectors.selectedSites).children().remove();
        _multiSelectAndSearch.setDisabledOptions(ids);

        // to disable after combo callbacks have completed
        $('div.responsibilities-auto-select-container input[type="text"]').attr('disabled', 'disabled');
        var t = setTimeout(function () {
            $('div.responsibilities-auto-select-container .btn').attr('disabled', 'disabled');
        }, 100);

        $(selectors.frequency).each(function () {
            _resetComboBox($(this));
        });

        $(selectors.responsiblePerson).each(function () {
            _resetComboBox($(this));
        });

        // reset Go To Generate Tasks button
        $(selectors.goToGenerateTasksButton).removeClass();
        $(selectors.goToGenerateTasksButton).addClass('hide');
    };

    var setupEmployeeDropdown = function (employees) {
        $(selectors.owners).each(function (idx, val) {
            var owner = $(this).find('input[type="text"]');
            var ownerId = $(this).find(':hidden');

            $(owner).combobox({
                selectedId: $(ownerId).val(),
                initialValues: employees
            });
        });
    };

    var setupFrequencyDropdown = function (frequencyOptions) {
        $(selectors.frequencies).each(function (idx, val) {
            var frequency = $(this).find('input[type="text"]');
            var frequencyId = $(this).find(':hidden');

            $(frequency).combobox({
                selectedId: $(frequencyId).val(),
                initialValues: frequencyOptions
            });
        });
    };

    var setupResetSitesButton = function () {
        $(selectors.resetSitesButon).on('click', function () {
            var ids = _multiSelectAndSearch.getDisabledOptions();
            _multiSelectAndSearch.setEnabledOptions(ids);
        });
    };

    var _initialise = function (employees, frequencyOptions) {
        setupEmployeeDropdown(employees);
        setupFrequencyDropdown(frequencyOptions);
        _setupSitesMultiSelector();
        setupResetSitesButton();
        _setupResponsibilitySelector();
        _setupGenerateResponsibilityButton();
        _resetForm();
    };

    $(selectors.selectAllResponsbilitiesButton).click(function (e) {
        var checked = $(this).prop("checked");
        var table = $(this).closest('table');
        if (checked) {
            selectAllCheckboxesInTableColumn(table, 1);
            _enableAllResponsibilties(table);
        }
        else {
            deSelectAllCheckboxesInTableColumn(table, 1);
            _disableAllResponsibilties(table);
        }
    });

    return {
        initialise: _initialise
    };
} ();
