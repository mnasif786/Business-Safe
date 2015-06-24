BusinessSafe.ImmediateRiskNotificationAndActions.AssignedTo = function () {

    var selectors = {
        companyId: '#CompanyId',
        owners: "div.owner",
        assignButton: "input.assign-btn",
        assignAllButton: "input.assign-all-btn",
        actionTaskRown: "tr.actionTaskRow",
        reassignActionTaskButtons: ".reassign-action-task-link",
        taskAssignedTo: "#ActionTaskAssignedTo",
        taskAssignedToId: "#ActionTaskAssignedToId",
        form: "#ActionPlanTasks",
        dialog: "#dialog",
        cancelButton: "#cancelButton",
        saveButton: "#saveButton",
        validationSummary: ".validation-summary-errors"
    };


    var _initialise = function (actionEmployee) {
        setupActionEmployeeDropdown(actionEmployee);
        setupAssignButtons();
        setupAssignAllButton();
        setupReassignButton();
        disableRowsForGeneratedTasks();
        
    };

    var disableRowsForGeneratedTasks = function () {
        var t = setTimeout(function () {
            $(selectors.actionTaskRown).each(function (idx, el) {
                var data = $(el).data();
                if (data.hasTask == 'True') {
                    disableTaskRow(el);
                }
            });
        }, 100);
    };

    var _disableAllTasksForNonEditPermissions = function () {
        var t = setTimeout(function() {
            $(selectors.actionTaskRown).each(function(idx, el) {
                disableTaskRow(el);});
        }, 100);
    };

    var setupActionEmployeeDropdown = function (actionEmployee) {
        $(selectors.owners).each(function (idx, val) {
            var owner = $(this).find('input[type="text"].owner');
            var ownerId = $(this).find(':hidden');

            $(owner).combobox({
                selectedId: $(ownerId).val(),
                initialValues: actionEmployee
            });
        });
    };

    var getTaskData = function (link) {
        var data = {};

        var taskData = $(link).data();
        var assignedTo = $(link).parent().parent().children().find('.owner[type=hidden]').val();
        var dueDate = $(link).parent().parent().children().find('input[id^="dueDate"]').val();

        data.actionId = taskData.id;
        data.assignedTo = assignedTo;
        data.dueDate = dueDate;

        data.error = (data.actionId == undefined || data.assignedTo == undefined || data.dueDate == undefined) ||
            (data.actionId == "" || data.assignedTo == "" || data.dueDate == "");

        return data;
    };


    function showValidationErrors(container, errors) {
        if (errors.length === 0) {
            return;
        }
        $('.alert-success').hide();

        var validationSummary = $(container);

        validationSummary.removeClass("hide");

        var errorList = validationSummary.find("ul");
        if (errorList.size() === 0) {
            validationSummary.append('<ul></ul>');
            errorList = validationSummary.find("ul");
        }

        errorList.empty();
        jQuery.each(errors, function (index, value) {
            $("<li>" + value + "</li>").appendTo(errorList);
        });

        var validationSummaryPosition = validationSummary.position();

        window.scrollTo(validationSummaryPosition.left, validationSummaryPosition.top);
    }

    var getErrors = function (taskData) {
        var errors = [];
        if (taskData.owner === '') errors.push('Please select an assignee for the selected task.');
        if (taskData.dueDate === '') errors.push('Please select a valid due date for the selected task.');

        return errors;
    };

    var disableTaskRow = function (row) {
        $(row).find('input[type="text"]').attr('disabled', 'disabled');
        $(row).find('.btn').attr('disabled', 'disabled');
        $(row).find('div.task-created').show();
        $(row).find('input[type="button"]').hide();
    };

    var setupAssignButtons = function () {
        $(selectors.assignButton + ':enabled').on('click', function () {
            var button = $(this);

            var validationmsg = $(this).data().validationmessage;

            var successfulCallBack = function (data) {
                if (data.Success === true) {
                    $(validationmsg).hide();

                    disableTaskRow(button.parent().parent());
                }
                else {
                    $(validationmsg).show();
                    showValidationErrors(validationmsg, data.Errors);
                }
            };

            var data = getTaskData(this);
            var errors = getErrors(data);

            if (errors.length > 0) {
                showValidationErrors(validationmsg, errors);
            }
            else {
                var url = window.globalajaxurls.assignTask;
                AjaxCall.execute(url, successfulCallBack, data, "POST");
            }
        });
    };

    var setupAssignAllButton = function () {
        $(selectors.assignAllButton).on('click', function () {

            var taskData = [];
            var rows = [];

            $(selectors.assignButton).each(function (idx, el) {

                var button = $(el);
                var data = getTaskData(button);
                if (data.error == false) {
                    taskData.push(data);
                    rows.push($(button).parent().parent());
                }

            });

            var successfulCallBack = function (data) {
                if (data.Success === true) {
                    //$(validationmsg).hide();
                    $(rows).each(function (idx, row) {
                        disableTaskRow(row);
                    });
                }
                else {
                    //$(validationmsg).show();
                    //showValidationErrors(validationmsg, data.Errors);
                }
            };

            if (taskData.length > 0) {
                var url = window.globalajaxurls.bulkAssignActionTasks;
                AjaxCall.execute(url, successfulCallBack, JSON.stringify(taskData), "POST", "application/json");
            }

        });

    };

    var setupReassignButton = function () {

        $(selectors.reassignActionTaskButtons).on('click', function (e) {
            e.preventDefault();

            var actionId = $(this).data().id;
            var actionplanId = $(this).data().actionplanId;
            var mode = $(this).data().mode;
            var title = $(this).data().title;

            var success = function (response) {
                $(selectors.dialog).empty().dialog({
                    title: title,
                    autoOpen: false,
                    width: 820,
                    modal: true,
                    resizable: false
                }).append(response).dialog('open'); ;

                initialiseCalendar();
                loadEmployees();

                var f = $(selectors.form);

                $(selectors.cancelButton).on('click', function (e) {
                    e.preventDefault();
                    $(selectors.dialog).dialog('close');
                });

                $(selectors.saveButton).on('click', function (e) {
                    e.preventDefault();
                    var saveButton = $(this).attr('disabled', 'disabled').addClass('disabled');

                    $.post(f[0].action, f.serialize(), function (result) {
                        if (result.Success === true) {

                            $(selectors.dialog).dialog('close');
                            location.reload();
                        }
                        else if (result.Errors !== undefined) {
                            saveButton.removeAttr('disabled').removeClass('disabled');
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
            };

            if (mode == 'Edit') {
                AjaxCall.execute(window.globalajaxurls.editActionTask, success, { actionId: actionId, actionplanId: actionplanId });
            } else if (mode == 'View') {
                AjaxCall.execute(window.globalajaxurls.viewIrnActionTask, success, { actionId: actionId, actionplanId: actionplanId });
            } else if (mode == 'Reassign') {
                AjaxCall.execute(window.globalajaxurls.reAssignActionTask, success, { actionId: actionId, actionplanId: actionplanId });
            }
            
        });
    };

    var loadEmployees = function () {

        var success = function (result) {

            result.splice(0, 0, { label: '--Select Option--', value: '' });

            $(selectors.taskAssignedTo).combobox({
                selectedId: $(selectors.taskAssignedToId).val(),
                initialValues: result
            });
        };

        var requestData = {
            filter: '',
            companyId: $(selectors.companyId).val(),
            pageLimit: 500
        };

        AjaxCall.execute(window.globalajaxurls.getEmployees, success, requestData);

    };

    return {
        initialise: _initialise,
        disableAllTasksForNonEditPermissions: _disableAllTasksForNonEditPermissions
    };
} ();