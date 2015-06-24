BusinessSafe.Responsibilities.Wizard.GenerateTasks = function () {

    var selectors = {
        frequencies: "div.frequency",
        owners: "div.owner",
        createTaskButton: "input.create-task",
        validationMessage: "#validation-message",
        saveAllButton: "a.generate-task-button",
        responsibilitiesIndexLink: "a.nav-responsibilities-index"
    };
    var _setupDescriptionPopover = function () {
        $('a.icon-question-sign').on('click', function (event) {
            event.preventDefault();
            $(this).
                popover({
                    container: '#responsibilities-wizard',
                    placement: 'right'
                });

            $(this).popover("show");
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

    var _disableTaskRow = function (row) {
        $(row).find('input[type="text"]').attr('disabled', 'disabled');
        $(row).find('.btn').attr('disabled', 'disabled');
        $(row).find('div.task-created').show();
        $(row).find('input[type="button"]').hide();
    };

    var _showIndexButton = function () {
        $(selectors.responsibilitiesIndexLink).removeClass('hide');
    };

    var setupCreateButtons = function () {
        $(selectors.createTaskButton).on('click', function () {
            var button = $(this);
            var successfulCallBack = function (data) {
                if (data.Success === true) {
                    $(selectors.validationMessage).hide();

                    _disableTaskRow(button.parent().parent());
                    _showIndexButton();
                } else {
                    $(selectors.validationMessage).show();
                    showValidationErrors(selectors.validationMessage, data.Errors);
                }
            };

            var data = getTaskData(this);

            var errors = getErrors(data);
            if (errors.length > 0) {
                showValidationErrors(selectors.validationMessage, errors);
            } else {
                var url = window.globalajaxurls.generateResponsibiltiesTasksFromSiteAndTemplateUrl;
                AjaxCall.execute(url, successfulCallBack, data, "POST");
            }
        });
    };

    var setupSaveAllButton = function () {
        $(selectors.saveAllButton).click(function (e) {

            var createButtonsToCheck = $(selectors.createTaskButton).not(':hidden');

            if (createButtonsToCheck.length == 0) {
                return;
            }

            e.preventDefault();

            var saveAllButton = $(this);
            saveAllButton.attr('disabled', 'disabled');

            var validTaskData = [];
            var createButtonsToCheck = $(selectors.createTaskButton).not(':hidden');
            createButtonsToCheck.each(function () { // get tasks that haven't been individually created
                var taskData = getTaskData($(this));

                var errors = getErrors(taskData);
                if (errors.length === 0) {
                    validTaskData.push(taskData);
                }
            });
            if (validTaskData.length > 0) {
                var successfulCallBack = function (data) {
                    if (data.Success === true) {
                        $(selectors.validationMessage).hide();
                        for (var i = 0; i < validTaskData.length; i++) {
                            _disableTaskRow($('tr[data-task-id="' + validTaskData[i].taskId + '"][data-rid="' + validTaskData[i].responsibilityId + '"][data-site-id="' + validTaskData[i].siteId + '"]'));
                        };
                        _showIndexButton();
                    } else {
                        showValidationErrors(selectors.validationMessage, data.Errors);
                    }
                    saveAllButton.removeAttr('disabled');
                };
                var url = window.globalajaxurls.bulkGenerateResponsibiltiesTasksFromSiteAndTemplateUrl;
                AjaxCall.execute(url, successfulCallBack, JSON.stringify(validTaskData), "POST", "application/json");
            } else {
                var errors = ['Please configure the tasks you wish to create.'];
                showValidationErrors(selectors.validationMessage, errors);
                saveAllButton.removeAttr('disabled');
            }
        });
    };

    var getTaskData = function (sender) {
        var taskData = {};
        var data = $(sender).parent().parent().data();
        taskData.siteId = data.siteId;
        taskData.responsibilityId = data.rid;
        taskData.taskId = data.taskId;
        taskData.owner = $(sender).parent().parent().children().find('.owner[type=hidden]').val();
        taskData.frequency = $(sender).parent().parent().children().find('.frequency [type=hidden]').val();
        taskData.startDate = $(sender).parent().parent().children().find('input[id^="start"]').val();
        taskData.endDate = $(sender).parent().parent().children().find('input[id^="end"]').val();
        return taskData;

    };

    var getErrors = function (taskData) {
        var errors = [];
        if (taskData.frequency == 0) errors.push('Please select a frequency for the selected task.');
        if (taskData.owner === '') errors.push('Please select an assignee for the selected task.');
        if (taskData.startDate === '') errors.push('Please select a valid start date for the selected task.');

        return errors;
    };

    var disableRowsForCreatedTasks = function () {
        var t = setTimeout(function () {
            $('tr[data-created="True"]').each(function() {
                _disableTaskRow(this);
            });
        }, 100);
    };
    
    var _initialise = function (employees, frequencyOptions) {
        _setupDescriptionPopover();
        setupFrequencyDropdown(frequencyOptions);
        setupEmployeeDropdown(employees);
        setupCreateButtons();
        setupSaveAllButton();
        disableRowsForCreatedTasks();
    };

    return { initialise: _initialise };
} ();
