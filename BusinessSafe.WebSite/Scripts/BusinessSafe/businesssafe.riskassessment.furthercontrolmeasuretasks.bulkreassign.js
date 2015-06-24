var bulkReassignTasksManager = function () {
    var injectedElementClassName = 'bulk-reassign-injected-elem';
    var bulkReassignCheckboxClassName = 'bulk-reassign-checkbox';
    var notEmployeeAlertClassName = 'employee-not-user-alert-message';

    var selectors = {
        initButton: 'button#init-bulk-reassign-tasks',
        updateButton: 'button#update-bulk-reassign-tasks',
        cancelButton: 'button#cancel-bulk-reassign-tasks',
        companyId: '#CompanyId',
        riskAssessmentId: '#RiskAssessmentId',
        reassignTaskToDropdown: '#reassignTaskToDropdown',
        reassignTaskTo: "#bulkReassignTaskTo",
        reassignTaskToId: "#bulkReassignTaskToId",
        validationSummary: ".validation-summary-errors",
        bulkReassignCheckbox: "." + bulkReassignCheckboxClassName,
        injectedElements: "." + injectedElementClassName,
        notEmployeeAlert: "." + notEmployeeAlertClassName
    };

    var statuses = {
        Outstanding : "Outstanding",
        Overdue : "Overdue"
    };

    function initialise() {
        $(selectors.initButton).click(function () {
            setupBulkReassignTasks();
        });
        $(selectors.updateButton).click(function () {
            saveBulkReassignTasks();
        });
        $(selectors.cancelButton).click(function () {
            cancelBulkReassignTasks();
        });
        $(selectors.bulkReassignCheckbox).click(function () {
            $(this).attr('checked', 'checked');
        });

        $(selectors.initButton).show();
        $(selectors.updateButton).hide();
        $(selectors.cancelButton).hide();
        $(selectors.reassignTaskToDropdown).css('display', 'none');
        $(selectors.reassignTaskTo).val('');
        $(selectors.reassignTaskToId).val('');
    }

    var setupBulkReassignTasks = function () {
        $(selectors.initButton).hide();
        $(selectors.updateButton).show();
        $(selectors.cancelButton).show();
        $(selectors.reassignTaskToDropdown).css('display', 'inline-block');
        $(selectors.reassignTaskTo).focus();
        $(selectors.validationSummary).hide();
        $(selectors.reassignTaskTo).val('');
        $(selectors.reassignTaskToId).val('');

        var hazardTaskTables = $('table.further-action-task-table');
        hazardTaskTables.each(function () {
            $(this).find('th:nth-child(3)').after('<th class="' + injectedElementClassName + '">Re-assign Task</th>');
            $(this).find('tr').each(function () {
                var statusCellContents = $(this).find('td.fcm-status').html();
                $(this).find('td:nth-child(3)').after('<td class="' + injectedElementClassName + '"></td>');
                if (statusCellContents !== null && (statusCellContents.indexOf(statuses.Outstanding) != -1 || statusCellContents.indexOf(statuses.Overdue) != -1)) {
                    $(this).find('td.' + injectedElementClassName).html('<input type="checkbox" class="' + bulkReassignCheckboxClassName + '" />');
                }
            });
        });

        setupEmployeeDropdown();
    };

    var saveBulkReassignTasks = function () {

        var saveButton = $(selectors.updateButton);
        var checkedTasks = $('input.bulk-reassign-checkbox:checked');
        if (validate() === true) {

            saveButton.attr('disabled', 'disabled').addClass('disabled');
            $.ajax({
                type: "POST",
                url: window.globalajaxurls.saveFurtherControlMeasureReassignment,
                cache: false,
                data: JSON.stringify(collateReassignmentData()),
                contentType: "application/json",
                error: function () {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            }).done(function (data) {
                if (data.Success) {
                    document.location.reload(true);
                } else {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            });
        }
    };

    var validate = function () {
        var errors = [];
        $(".input-validation-error").removeClass("input-validation-error");

        var taskAssignedTo = $(selectors.reassignTaskTo);
        if (taskAssignedTo.val() === "") {
            taskAssignedTo.addClass("input-validation-error");
            errors.push("Please select who you wish to reassign the tasks to");
        }

        if ($(selectors.bulkReassignCheckbox + ':checked').size() === 0) {
            errors.push("Please tick the tasks you wish to reassign");
        }

        showValidationErrors(selectors.validationSummary, errors);

        if (errors.length > 0) {
            $(selectors.validationSummary).css('display', 'block');
        }
        return errors.length === 0;
    };

    var collateReassignmentData = function () {

        var companyId = $(selectors.companyId).val();
        var riskAssessmentId = $(selectors.riskAssessmentId).val();
        var reassignTaskToId = $(selectors.reassignTaskToId).val();

        var reassignments = new Array();
        $('.bulk-reassign-checkbox:checked').each(function () {

            var row = $(this).parent().parent();
            var riskAssessmentHazardId = row.attr('data-rah-id');
            var furtherControlMeasureTaskId = row.attr('data-id');
            var taskGuid = row.attr('data-task-guid');

            reassignments.push({
                CompanyId: companyId,
                RiskAssessmentId: riskAssessmentId,
                RiskAssessmentHazardId: riskAssessmentHazardId,
                FurtherControlMeasureTaskId: furtherControlMeasureTaskId,
                ReassignTaskToId: reassignTaskToId,
                TaskGuid: taskGuid
            });
        });
        return reassignments;
    };

    var tidyUpAfterSuccessfulReassign = function () {
        var furtherControlMeasureTable = $(selectors.bulkReassignCheckbox + ':checked').closest('table');
        var assignedToRowHeader = furtherControlMeasureTable.find("th:contains('Assignee')");
        var assignedToColumnIndex = assignedToRowHeader.parent("tr").children().index(assignedToRowHeader); //index starts from 0
        var reassignedToValue = $(selectors.reassignTaskTo).val();

        $(selectors.bulkReassignCheckbox + ':checked').each(function () {
            $(this).closest('tr').find('td').eq(assignedToColumnIndex).html(reassignedToValue);
        });

        cancelBulkReassignTasks();
        $(selectors.notEmployeeAlert).before('<div id="update-notification" class="row-fluid alert"><p>The tasks have been re-assigned!</div>').siblings('#update-notification').delay(3000).fadeOut();
    };

    var cancelBulkReassignTasks = function () {
        $(selectors.initButton).show();
        $(selectors.updateButton).hide();
        $(selectors.cancelButton).hide();
        $(selectors.reassignTaskToDropdown).hide();
        $(selectors.validationSummary).hide().find('ul').empty();
        $(selectors.injectedElements).remove();
        $(selectors.notEmployeeAlert).addClass('hide');
    };

    var setupEmployeeDropdown = function () {

        var successfulCallBack = function (result) {

            $(selectors.reassignTaskTo).combobox({
                initialValues: result,
                url: window.globalajaxurls.getEmployees,
                data: {
                    companyId: $(selectors.companyId).val(),
                    pageLimit: 500
                },
                afterSelect: function (event, ui) {

                    var reassignedToId = $(selectors.reassignTaskToId).val();
                    var showIsEmployeeAUser = new ShowIsSelectedAssignedToEmployeeABusinessSafeUserIndicator(reassignedToId);
                    showIsEmployeeAUser.execute();

                    return false;
                }
            });
        };

        var data = {
            filter: '',
            companyId: $(selectors.companyId).val(),
            pageLimit: 500
        };

        AjaxCall.execute(window.globalajaxurls.getEmployees, successfulCallBack, data);

    };

    return {
        initialise: initialise
    };
} ();

$(function () {
    bulkReassignTasksManager.initialise();
});