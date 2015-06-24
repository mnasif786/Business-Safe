var tasksCompleteViewModelLoader = function () {

    var getUrl = function (data) {

        var url = '';
        if (data.taskType === 'gra') {
            url = window.globalajaxurls.getGeneralRiskAssessmentCompleteFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'hsra') {
            url = window.globalajaxurls.getHazardousSubstanceRiskAssessmentCompleteFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'pra') {
            url = window.globalajaxurls.getPersonalRiskAssessmentCompleteFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'fra') {
            url = window.globalajaxurls.getFireRiskAssessmentCompleteFurtherControlMeasureTaskUrl;
        }

        if (data.taskType === 'responsibility') {
            url = window.globalajaxurls.getResponsibilityCompleteTaskUrl;
        }

        if (data.taskType === 'action') {
            url = window.globalajaxurls.getActionsCompleteTaskUrl;
                                        
        }


        return url;
    };

    var getTitle = function (data) {

        var title = '';
        if (data.taskType === 'gra') {
            title = 'Further Control Measure Task';
        }

        if (data.taskType === 'hsra') {
            title = 'Further Control Measure Task';
        }

        if (data.taskType === 'pra') {
            title = 'Further Control Measure Task';
        }

        if (data.taskType === 'fra') {
            title = 'Further Control Measure Task';
        }

        if (data.taskType === 'responsibility') {
            title = 'Responsibility Task';
        }

        if (data.taskType === 'action') {
            title = 'Action Task';
        }
        
        return title;
    };

    var load = function (data) {

        var url = getUrl(data);
        var title = getTitle(data);
        var callback = function (callbackData) {
            new TaskCompleteViewModel().initialize(callbackData, title);
        };

        AjaxCall.execute(url, callback, data, "GET");
    };

    return {
        load: load
    };

} ();

function TaskCompleteViewModel() {
    var selectors = {
        dialog: "#TaskDialog",
        taskCompleteCheckbox: "#TaskComplete",
        saveButton: "#TaskSaveButton",
        cancelButton: "#TaskCancelButton",
        form: "#ActionTask",
        isCompleted: '#TaskComplete',
        completedComments: '#CompletedComments'
    };

    var showDialog = function (callbackData,title) {

        $(selectors.dialog).empty().dialog({
            title:title,
            autoOpen: false,
            width: 820,
            modal: true,
            resizable: false,
            draggable: false
        }).append(callbackData).dialog('open');
    };
    
    var setSaveButtonEnabledState = function (completeCheckbox) {
        var completeButton = $(selectors.saveButton);
        var checked = $(completeCheckbox).attr('checked');

        if (checked === "checked") {
            completeButton.removeAttr("disabled");
            completeButton.removeClass("disabled");
        } else {
            completeButton.attr("disabled", "disabled");
            completeButton.addClass("disabled");
        }
    };

    var getErrors = function () {
        var errors = new Array();

        if ($(selectors.isCompleted).is(':checked') === false) {
            var isCompletedErrorMessages = new Array();
            isCompletedErrorMessages.push({ ErrorMessage: "Please ensure Completed is ticked" });
            errors.push({
                PropertyName: selectors.isCompleted,
                Errors: isCompletedErrorMessages
            });
        };

        return errors;
    };

    var saveComplete = function () {
        var form = $(selectors.form);

        $.post(form[0].action, form.serialize(), function (result) {
            if (result.Success === true) {
                $(selectors.dialog).dialog('close');
                location.reload(true);
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
    };

    var hookEvents = function () {

        $(selectors.taskCompleteCheckbox).click(function () {
            setSaveButtonEnabledState(this);
        });

        $(selectors.cancelButton).click(function (event) {
            event.preventDefault();

            $(selectors.dialog).dialog('close');
        });

        $(selectors.saveButton).on('click', function (event) {
            event.preventDefault();

            if ($(this).hasClass('disabled')) return;

            var errors = getErrors();

            if (errors.length > 0) {
                showValidationErrorsWithHighlightedFields('.validation-summary-errors', errors);
                return;
            }

            $(this).attr('disabled', 'disabled');
            saveComplete();
        });

        $(selectors.completedComments).focus();
    };

    this.initialize = function (callbackData,title) {
        showDialog(callbackData,title);
        hookEvents();
    };

}



