var responsibilityTasksModule = function () {

    function load(responsibilityTaskId) {

        var selectors = {
            form: '#CreateResponsibilityTaskForm',
            dialogContainer: "#dialogAddEditResponsibilityTask",
            companyId: "#CompanyId",
            responsibilityId: "#ResponsibilityId",
            taskId: "#TaskId",
            saveButton: '#saveResponsibilityTaskButton',
            cancelButton: "#CancelAddResponsibilityTask",
            isReoccurring: "#IsRecurring",
            reoccurringDiv: "#recurringDiv",
            nonReoccurringDiv: "#nonReoccurringDiv",
            validationSummary: ".validation-summary-errors",
            titleId: "#TaskTitle",
            descriptionId: "#TaskDescription",
            isRecurringCheckboxId: "#IsRecurring",
            taskRecurringType: "#TaskReoccurringType",
            taskRecurringTypeId: "#TaskReoccurringTypeId",
            taskRecurringFirstDueDateId: "#FirstDueDate",
            taskRecurringLastDueDateId: "#TaskReoccurringEndDate",
            completionDueDateId: "#CompletionDueDate",
            taskAssignedTo: "#AssignedTo",
            taskAssignedToId: "#AssignedToId",
            doNotSendTaskAssignedNotificationId: "#DoNotSendTaskAssignedNotification",
            doNotSendTaskCompletedNotificationId: "#DoNotSendTaskCompletedNotification",
            doNotSendTaskOverdueNotificationId: "#DoNotSendTaskOverdueNotification",
            responsibilitySite: "#ResponsibilitySite",
            responsibilitySiteId: "#ResponsibilitySiteId",
            site: "#dialogAddEditResponsibilityTask #ResponsibilityTaskSite",
            siteId: "#dialogAddEditResponsibilityTask #ResponsibilityTaskSiteId",
            dialogHasMultipleFrequencyChangeToTrue: "#dialogHasMultipleFrequencyChangeToTrue",
            showCreateResponsibilityTaskDialogOnLoad: "#ShowCreateResponsibilityTaskDialogOnLoad",
            noLongerRequiredButton: "#ResponsibilityTaskNoLongerRequiredButton"
        };

        //load task assigned to
        var loadTaskAssignedDropDown = function () {

            var successfulCallBack = function (result) {

                result.splice(0, 0, { label: '--Select Option--', value: '' });

                $(selectors.taskAssignedTo).combobox({
                    selectedId: $(selectors.taskAssignedToId).val(),
                    initialValues: result,
                    afterSelect: function (event, ui) {
                        var taskAssignedToId = $(selectors.taskAssignedToId).val();
                        var showIsEmployeeAUser = new ShowIsSelectedAssignedToEmployeeABusinessSafeUserIndicator(taskAssignedToId);
                        showIsEmployeeAUser.execute();
                        return false;
                    }
                });
            };

            var requestData = {
                filter: '',
                companyId: $(selectors.companyId).val(),
                pageLimit: 500
            };

            AjaxCall.execute(window.globalajaxurls.getEmployees, successfulCallBack, requestData);
        };

        var loadTaskReoccuringDropDown = function () {

            var successfulCallBack = function (result) {

                $(selectors.taskRecurringType).combobox({
                    selectedId: $(selectors.taskRecurringTypeId).val(),
                    initialValues: result
                });

            };

            var requestData = {
                filter: '',
                companyId: $(selectors.companyId).val(),
                pageLimit: 100
            };

            AjaxCall.execute(window.globalajaxurls.getTaskReoccuringTypes, successfulCallBack, requestData);
        };

        var loadSiteDropDown = function () {
            var callback = function (result) {

                result.splice(0, 0, { label: '--Select Option--', value: '' });

                $(selectors.site).combobox({
                    selectedId: $(selectors.siteId).val(),
                    initialValues: result
                });

                if ($(selectors.taskId).val() == 0) {
                    $(selectors.siteId).val($(selectors.responsibilitySiteId).val());
                    $(selectors.site).val($(selectors.responsibilitySite).val());
                }
            };

            var requestData = {
                filter: '',
                companyId: $(selectors.companyId).val(),
                pageLimit: 100
            };

            AjaxCall.execute(window.globalajaxurls.getSites, callback, requestData);
        };


        var setReoccurring = function () {

            var reoccurringDiv = $(selectors.reoccurringDiv);
            var nonReoccurringDiv = $(selectors.nonReoccurringDiv);
            var checked = $(selectors.isRecurringCheckboxId).attr('checked');

            if (checked === "checked") {
                reoccurringDiv.removeClass("hide");
                nonReoccurringDiv.addClass("hide");
            } else {
                reoccurringDiv.addClass("hide");
                nonReoccurringDiv.removeClass("hide");
            }

        };

        var showDialog = function () {
            $(selectors.dialogContainer).dialog({
                modal: true,
                width: 900,
                resizable: false,
                draggable: false
            }).dialog('open');

        };

        var getResponsibiltyTaskData = {
            companyId: $(selectors.companyId).val(),
            responsibilityId: $(selectors.responsibilityId).val(),
            taskId: responsibilityTaskId,
            autoLaunchedAfterCreatingResponsibility: $(selectors.showCreateResponsibilityTaskDialogOnLoad).val() === 'True'
        };

        var successfulCallBack1 = function (returnedViewData) {
            $(selectors.dialogContainer).empty().append(returnedViewData);
            showDialog();
            loadTaskAssignedDropDown();
            loadTaskReoccuringDropDown();
            loadSiteDropDown();

            initialiseCalendar();

            //hook up cancel button
            $(selectors.cancelButton).on('click', function (event) {
                event.preventDefault();
                $(selectors.dialogContainer).dialog('close');
            });

            //hook up save button
            $(selectors.saveButton).on('click', function (event) {
                event.preventDefault();
                var saveButton = $(this).attr('disabled', 'disabled').addClass('disabled');
                var f = $(selectors.form);

                $.post(f[0].action, f.serialize(), function (result) {
                    if (result.Success === true) {

                        $(selectors.dialogContainer).dialog('close');

                        var reloadUrl = window.globalajaxurls.editRespobsibilityReassignResponsibilityTaskUrl +
                            "?responsibilityId=" + $(selectors.responsibilityId).val() +
                                "&companyId=" + $(selectors.companyId).val();

                        if (result.HasMultipleFrequencyChangeToTrue === true) {
                            $(selectors.dialogHasMultipleFrequencyChangeToTrue).dialog({
                                buttons: {
                                    "OK": function () {
                                        $(this).dialog("close");
                                        //location.reload();
                                        window.location.replace(reloadUrl);
                                    }
                                },
                                //modal: true,
                                //width: 900,
                                resizable: false,
                                draggable: false
                            }).dialog('open');
                        } else {
                            //location.reload();
                            window.location.replace(reloadUrl);
                        }
                    } else if (result.Errors !== undefined) {
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

            $(selectors.noLongerRequiredButton).click(function (event) {
                event.preventDefault();
                new TaskNoLongerRequiredModule(taskNoLongerRequiredCallback).initialize();
                return false;
            });

            var taskNoLongerRequiredCallback = function () {
                location.reload(true);
            };

            //hook up is recurring checkbox
            $(selectors.isRecurringCheckboxId).on('click', function (e) {
                if ($(this).attr('readonly') == 'readonly') {
                    e.preventDefault();
                    return false;
                }
                setReoccurring();
            });

            setReoccurring();
        };

        AjaxCall.execute(window.globalajaxurls.getResponsibiltyTask, successfulCallBack1, getResponsibiltyTaskData, "GET");

    }

    return {
        load: load
    };
} ();
